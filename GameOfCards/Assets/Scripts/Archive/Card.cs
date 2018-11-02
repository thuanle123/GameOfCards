using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* https://forum.unity.com/threads/best-way-to-program-a-deck-of-cards.160014/
 * 
 * 
 */

public class Card : ScriptableObject
{
    public enum SuitEnum
    {
        Clubs = 1,
        Diamonds = 2,
        Hearts = 3,
        Spades = 4
    }

    private SuitEnum suit;
    private int rank;
    private int assetIndex;
    
    public SuitEnum Suit { get { return suit; } }
    public int Rank { get { return rank; } }

    private GameObject card;

    public Card(SuitEnum suit, int rank, int assetIndex, Vector3 position, Quaternion rotation) //Need to add position X and position Y
    {
        string assetName = "StandardDeck_" + assetIndex; // Example:  "StandardDeck_0" would be the Ace of Clubs.
        GameObject asset = GameObject.Find(assetName);
        if (asset == null)
        {
            Debug.LogError("Asset '" + assetName + "' could not be found.");
        }
        else
        {
            this.card = (GameObject)Instantiate(asset, position, rotation);
            this.suit = suit;
            this.rank = rank;
        }
    }
}
