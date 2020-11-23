using System.Collections.Generic;

[System.Serializable] public class CharacterShopData
{
    public List<int> purchasedCharactersIndexes = new List<int>();
}

[System.Serializable] public class PlayerData
{
    public int coins = 0;
    public int selectedCharacterIndex = 0;
}

public static class GameDataManager
{
    static PlayerData playerData = new PlayerData();
    static CharacterShopData characterShopData = new CharacterShopData();

    static Character selectedCharacter;

    public static Character GetSelectedCharacter()
    {
        return selectedCharacter;
    }

    public static void SetSelectedCharacter(Character character, int index)
    {
        selectedCharacter = character;
        playerData.selectedCharacterIndex = index;
        SavePlayerData();
    }

    public static int GetSelectedCharacterIndex()
    {
        return playerData.selectedCharacterIndex;
    }

    static GameDataManager()
    {
        LoadPlayerData();
        LoadCharacterShopData();
    }

    public static int GetCoins()
    {
        return playerData.coins;
    }

    public static void AddCoins(int Amount)
    {
        playerData.coins += Amount;
        SavePlayerData();
    }

    public static bool CanSpendCoins(int Amount)
    {
        return (playerData.coins >= Amount);
    }

    public static void SpendCoins(int Amount)
    {
        playerData.coins -= Amount;
        SavePlayerData();
    }

    static void LoadPlayerData()
    {
        playerData = BinarySerializer.Load<PlayerData>("player-data.txt");
        UnityEngine.Debug.Log("<color=green>[PlayerData] Loaded.</color>");
    }

    static void SavePlayerData()
    {
        BinarySerializer.Save(playerData, "player-data.txt");
        UnityEngine.Debug.Log("<color=magenta>[PlayerData] Saved.</color>");
    }

    public static void AddPurchasedCharacter(int characterIndex)
    {
        characterShopData.purchasedCharactersIndexes.Add(characterIndex);
        SaveCharacterShopData();
    }

    public static List<int> GetallPurchasedCharacter()
    {
        return characterShopData.purchasedCharactersIndexes;
    }

    public static int GetPurchasedCharacter(int index)
    {
        return characterShopData.purchasedCharactersIndexes[index];
    }

    static void LoadCharacterShopData()
    {
        characterShopData = BinarySerializer.Load<CharacterShopData>("character-shop-data.txt");
        UnityEngine.Debug.Log("<color=green>[CharacterShopData] Loaded.</color>");
    }

    static void SaveCharacterShopData()
    {
        BinarySerializer.Save(characterShopData, "character-shop-data.txt");
        UnityEngine.Debug.Log("<color=magenta>[CharacterShopData] Saved.</color>");
    }
}
