using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterShopDatabase", menuName ="Shopping/Characters shop database")]
public class CharacterShopDatabase : ScriptableObject
{
    public Character[] character;

    public int CharacterCount
    {
        get { return character.Length; }
    }

    public Character GetPurchased(int index)
    {
        return character[index];
    }

    public void PurchasedCharacter(int index)
    {
        character[index].isPurchased = true;
    }
}
