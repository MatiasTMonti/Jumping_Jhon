using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;

public class CharacterItemUI : MonoBehaviour
{
    [SerializeField] Color noItemSelectedColor;
    [SerializeField] Color itemSelectedColor;

    [Space(20.0f)]
    [SerializeField] Image characterImage;
    [SerializeField] TMP_Text characterPriceText;
    [SerializeField] Button characterPurchasedButton;

    [Space(20.0f)]
    [SerializeField] Button itemButton;
    [SerializeField] Image itemImage;
    [SerializeField] Outline itemOutline;

    public void SetItemPosition(Vector2 pos)
    {
        GetComponent<RectTransform>().anchoredPosition += pos;
    }

    public void SetCharacterImage(Sprite sprite)
    {
        characterImage.sprite = sprite;
    }

    public void SetCharacterPrice(int price)
    {
        characterPriceText.text = price.ToString();
    }

    public void SetCharacterAsPurchased()
    {
        characterPurchasedButton.gameObject.SetActive(false);
        itemButton.interactable = true;

        itemImage.color = noItemSelectedColor;
    }

    public void OnItemPurchased(int itemIndex, UnityAction<int> action)
    {
        characterPurchasedButton.onClick.RemoveAllListeners();
        characterPurchasedButton.onClick.AddListener(() => action.Invoke(itemIndex));
    }

    public void OnItemSelect(int itemIndex, UnityAction<int> action)
    {
        itemButton.interactable = true;
        itemButton.onClick.RemoveAllListeners();
        itemButton.onClick.AddListener(() => action.Invoke(itemIndex));
    }

    public void SelectItem()
    {
        itemOutline.enabled = true;
        itemImage.color = itemSelectedColor;
        itemButton.interactable = false;
    }

    public void DeselectItem()
    {
        itemOutline.enabled = false;
        itemImage.color = noItemSelectedColor;
        itemButton.interactable = true;
    }

    public void AnimateShakeItem()
    {
        transform.DOComplete();
        transform.DOShakePosition(1f, new Vector3(8f, 0, 0), 10, 0).SetEase(Ease.Linear);
    }
}
