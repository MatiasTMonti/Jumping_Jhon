using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CharacterShopUI : MonoBehaviour
{
    [Header("Layout Settings")]
    [SerializeField] private float itemSpacing = 0.5f;
    float itemHeight;

    [Header("UI elements")]
    [SerializeField] private Image selectedCharacterIcon = null;
    [SerializeField] private Transform shopMenu = null;
    [SerializeField] private Transform shopItemsContainer = null;
    [SerializeField] private GameObject itemPrefab = null;
    [Space(20)]
    [SerializeField] private CharacterShopDatabase characterDB = null;

    [Space(20)]
    [Header("Shop Events")]
    [SerializeField] private GameObject shopUi = null;
    [SerializeField] private Button openShopButton = null;
    [SerializeField] private Button closeShopButton = null;
    [SerializeField] private Button scrollUpButton = null;

    [Space(20)]
    [Header("Main Menu")]
    [SerializeField] private Image mainMenuCharacterImage = null;

    [Space(20)]
    [Header("Scroll View")]
    [SerializeField] private ScrollRect scrollRect = null;

    [Space(20)]
    [Header("Purchase Fx and Error messages")]
    [SerializeField] private ParticleSystem purchaseFx = null;
    [SerializeField] private Transform purchaseFxPos = null;
    [SerializeField] private TMP_Text noEnoughCoinsText = null;

    int newSelectedItemIndex = 0;
    int previousSelectedItemIndex = 0;

    private void Start()
    {
        purchaseFx.transform.position = purchaseFxPos.position;

        AddShopEvents();

        GenerateShopItemsUI();

        SetSelectedCharacter();

        SelectedItemUI(GameDataManager.GetSelectedCharacterIndex());

        ChangePlayerSkin();

        AutoScrollShopList(GameDataManager.GetSelectedCharacterIndex());
    }

    void AutoScrollShopList(int itemIndex)
    {
        scrollRect.verticalNormalizedPosition = Mathf.Clamp01(1f-(itemIndex/(float)(characterDB.CharacterCount-1)));
    }

    void SetSelectedCharacter()
    {
        int index = GameDataManager.GetSelectedCharacterIndex();

        GameDataManager.SetSelectedCharacter(characterDB.GetPurchased(index), index);
    }

    private void GenerateShopItemsUI()
    {
        for (int i = 0; i < GameDataManager.GetallPurchasedCharacter().Count; i++)
        {
            int purchasedCharacterIndex = GameDataManager.GetPurchasedCharacter(i);
            characterDB.PurchasedCharacter(purchasedCharacterIndex);
        }

        itemHeight = shopItemsContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
        Destroy(shopItemsContainer.GetChild(0).gameObject);
        shopItemsContainer.DetachChildren();

        for (int i = 0; i < characterDB.CharacterCount; i++)
        {
            Character character = characterDB.GetPurchased(i);
            CharacterItemUI uiItem = Instantiate(itemPrefab, shopItemsContainer).GetComponent<CharacterItemUI>();

            uiItem.SetItemPosition(Vector2.down * i * (itemHeight + itemSpacing));

            uiItem.SetCharacterImage(character.image);
            uiItem.SetCharacterPrice(character.price);

            if (character.isPurchased)
            {
                uiItem.SetCharacterAsPurchased();
                uiItem.OnItemSelect(i, OnItemSelect);
            }
            else
            {
                uiItem.SetCharacterPrice(character.price);
                uiItem.OnItemPurchased(i, OnItemPurchased);
            }

            shopItemsContainer.GetComponent<RectTransform>().sizeDelta = Vector2.up * ((itemHeight + itemSpacing) * characterDB.CharacterCount + itemSpacing);
        }
    }

    void ChangePlayerSkin()
    {
        Character character = GameDataManager.GetSelectedCharacter();
        if (character.image != null)
        {
            mainMenuCharacterImage.sprite = character.image;

            selectedCharacterIcon.sprite = GameDataManager.GetSelectedCharacter().image;
        }
    }

    private void OnItemSelect(int index)
    {
        SelectedItemUI(index);

        GameDataManager.SetSelectedCharacter(characterDB.GetPurchased(index), index);

        ChangePlayerSkin();
    }

    void SelectedItemUI(int itemIndex)
    {
        previousSelectedItemIndex = newSelectedItemIndex;
        newSelectedItemIndex = itemIndex;

        CharacterItemUI previousUIItem = GetItemUI(previousSelectedItemIndex);
        CharacterItemUI newUIItem = GetItemUI(newSelectedItemIndex);

        previousUIItem.DeselectItem();
        newUIItem.SelectItem();
    }
    CharacterItemUI GetItemUI(int index)
    {
        return shopItemsContainer.GetChild(index).GetComponent<CharacterItemUI>();
    }

    private void OnItemPurchased(int index)
    {
        Character character = characterDB.GetPurchased(index);
        CharacterItemUI uiItem = GetItemUI(index);

        if (GameDataManager.CanSpendCoins(character.price))
        {
            GameDataManager.SpendCoins(character.price);

            purchaseFx.Play();

            GameSharedUI.Instance.UpdateCoinsUIText();

            characterDB.PurchasedCharacter(index);

            uiItem.SetCharacterAsPurchased();
            uiItem.OnItemSelect(index, OnItemSelect);

            GameDataManager.AddPurchasedCharacter(index);
        }
        else
        {
            AnimateNoMoreCoins();
            uiItem.AnimateShakeItem();
        }
    }

    void AnimateNoMoreCoins()
    {
        noEnoughCoinsText.transform.DOComplete();
        noEnoughCoinsText.DOComplete();

        noEnoughCoinsText.transform.DOShakePosition(3f, new Vector3(5f, 0f, 0f), 10, 0);
        noEnoughCoinsText.DOFade(1f, 3f).From(0f).OnComplete(() =>
        {
            noEnoughCoinsText.DOFade(0f, 1f);
        });
    }

    private void AddShopEvents()
    {
        openShopButton.onClick.RemoveAllListeners();
        openShopButton.onClick.AddListener(OpenShop);

        closeShopButton.onClick.RemoveAllListeners();
        closeShopButton.onClick.AddListener(CloseShop);

        scrollRect.onValueChanged.RemoveAllListeners();
        scrollRect.onValueChanged.AddListener(OnShopListScroll);

        scrollUpButton.onClick.RemoveAllListeners();
        scrollUpButton.onClick.AddListener(OnScrollUpClicked);
    }

    void OnScrollUpClicked()
    {
        scrollRect.DOVerticalNormalizedPos(1f, .5f).SetEase(Ease.OutBack);
    }

    void OnShopListScroll(Vector2 value)
    {
        float scrollY = value.y;

        if (scrollY<.7f)
        {
            scrollUpButton.gameObject.SetActive(true);
        }
        else
        {
            scrollUpButton.gameObject.SetActive(false);
        }
    }

    private void OpenShop()
    {
        shopUi.SetActive(true);
    }

    private void CloseShop()
    {
        shopUi.SetActive(false);
    }
}
