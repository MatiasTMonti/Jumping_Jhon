using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    #region Singleton:Profile

    public static Profile Instance;

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

    public class Avatar
    {
        public Sprite image;
    }

    public List<Avatar> AvatarsList;

    [SerializeField] GameObject AvatarUITemplate;
    [SerializeField] Transform AvatarsScrollView;

    GameObject g;

    int newSelectedIndex, previousSelectedIndex;

    [SerializeField] Color ActiveAvatarColor;
    [SerializeField] Color DefaultAvatarColor;

    [SerializeField] Image CurrentAvatar;

    private void Start()
    {
        GetAvailableAvatars();
        newSelectedIndex = previousSelectedIndex = 0;
    }

    private void GetAvailableAvatars()
    {
        for (int i = 0; i < Shop.Instance.ShopItemList.Count; i++)
        {
            if (Shop.Instance.ShopItemList[i].IsPurchased)
            {
                AddAvatar(Shop.Instance.ShopItemList[i].image);
            }
        }

        SelectedAvatar(newSelectedIndex);
    }

    public void AddAvatar(Sprite img)
    {
        if (AvatarsList == null)
        {
            AvatarsList = new List<Avatar>();
        }
        Avatar av = new Avatar() { image = img };

        AvatarsList.Add(av);

        g = Instantiate(AvatarUITemplate, AvatarsScrollView);

        g.transform.GetChild(0).GetComponent<Image>().sprite = av.image;

        g.transform.GetComponent<Button>().AddEventListener(AvatarsList.Count - 1, OnAvatarClick);
    }

    private void OnAvatarClick(int AvatarIndex)
    {
        SelectedAvatar(AvatarIndex);
    }


    private void SelectedAvatar(int AvatarIndex)
    {
        previousSelectedIndex = newSelectedIndex;
        newSelectedIndex = AvatarIndex;
        AvatarsScrollView.GetChild(previousSelectedIndex).GetComponent<Image>().color = DefaultAvatarColor;
        AvatarsScrollView.GetChild(newSelectedIndex).GetComponent<Image>().color = ActiveAvatarColor;

        CurrentAvatar.sprite = AvatarsList[newSelectedIndex].image;
    }
}
