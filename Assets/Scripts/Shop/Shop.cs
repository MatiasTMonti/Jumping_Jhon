using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    #region Singleton:Shop

    public static Shop Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    [System.Serializable] public class ShopItem
    {
        public Sprite image;
        public int Price;
        public bool IsPurchased = false;
    }

    [SerializeField] private Animator NoCoinsAnim;

    [SerializeField] private GameObject ItemTemplate = null;
    [SerializeField] private Transform ShopScrollView = null;
    [SerializeField] private GameObject shopPanel;

    Button buyBtn;
    public List<ShopItem> ShopItemList;
    private GameObject g = null;
    private void Start()
    {
        for (int i = 0; i < ShopItemList.Count; i++)
        {
            g = Instantiate(ItemTemplate, ShopScrollView);
            g.transform.GetChild (0).GetComponent<Image>().sprite = ShopItemList[i].image;
            g.transform.GetChild (1).GetChild (0).GetComponent<TextMeshProUGUI>().text = ShopItemList[i].Price.ToString();
            buyBtn = g.transform.GetChild(2).GetComponent<Button>();
            if (ShopItemList[i].IsPurchased)
            {
                DisableBuyButton();
            }
            buyBtn.AddEventListener(i, OnShopItemBtnClicked);
        }
    }

    void OnShopItemBtnClicked(int ItemIndex)
    {
        if (Game.Instance.HasEnoughCoins(ShopItemList[ItemIndex].Price))
        {
            Game.Instance.UseCoins(ShopItemList[ItemIndex].Price);
            ShopItemList[ItemIndex].IsPurchased = true;

            buyBtn = ShopScrollView.GetChild(ItemIndex).GetChild(2).GetComponent<Button>();
            DisableBuyButton();

            Game.Instance.UpdateAllCoinsUIText();

            Profile.Instance.AddAvatar(ShopItemList[ItemIndex].image);
        }
        else
        {
            NoCoinsAnim.SetTrigger("NoCoins");
            Debug.Log("No tienes monedas suficientes!!");
        }
    }
    private void DisableBuyButton()
    {
        buyBtn.interactable = false;
        buyBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "PURCHASED";
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }
}
