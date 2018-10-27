using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Card class:
 * Uses a enumeration to keep track of the suit of the card.
 * TODO: Link individual cards to respective asset.
*/

public class Card
{
    public enum SuitEnum
    {
        Hearts = 1,
        Clubs = 2,
        Diamonds = 3,
        Spades = 4
    }

    private SuitEnum suit;
    private int rank;

    public SuitEnum Suit { get { return suit; } }
    public int Rank { get { return rank; } }

    private GameObject card;

    public Card(SuitEnum suit, int rank) //Need to add position X and position Y
    {
        //string assetName = string.Format("Card_{0}_{1}", suit, rank); // Example:  "Card_1_10" would be the Jack of Hearts.
        //this.card = Instantiate(asset, position);
        this.suit = suit;
        this.rank = rank;
    }
}
