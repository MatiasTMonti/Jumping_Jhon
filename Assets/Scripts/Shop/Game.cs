using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    #region Singleton:Game
    public static Game Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] TextMeshProUGUI[] allCoinsUiText = null;

    public int Coins;

    private void Start()
    {
        UpdateAllCoinsUIText();
    }

    public void UseCoins(int amount)
    {
        Coins -= amount;
    }

    public bool HasEnoughCoins(int amount)
    {
        return (Coins >= amount);
    }

    public void UpdateAllCoinsUIText()
    {
        for (int i = 0; i < allCoinsUiText.Length; i++)
        {
            allCoinsUiText[i].text = Coins.ToString();
        }
    }
}
