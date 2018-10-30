using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand {
    private List<Card> hand;
    private int limit; //How many cards MAXIMUM can you hold?

    public Hand(int limit)
    {
        this.limit = limit;
        hand = new List<Card>(limit); //Create a list that is the length of limit
    }

    public void addCard(Card c)
    {
        hand.Add(c);
    }
}
