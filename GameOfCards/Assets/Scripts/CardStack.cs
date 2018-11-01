using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Deck class:
 * Utilizes an interface list (IList) to generate a stack of 52 cards.
 * Shuffling implemented with a technique called the Fisher-Yates shuffle.
 *  Note: one instance of Deck also contains the discard pile.
*/ 

public class CardStack : MonoBehaviour
{

    //IList<Card> deck;
    //IList<Card> discard_pile;

    // this list is private
    List<int> cards;

    public bool isGameDeck;

    public bool HasCards
    {
        get { return cards != null && cards.Count > 0; }
    }
    // need a public method to numerate through them all
    // need to use yield
    public IEnumerable<int> GetCards()
    {
        foreach (int i in cards)
        {
            yield return i;
        }
    }

    //Get card count from card stack
    public int CardCount
    {
        get
        {
            if (cards != null)
            {
                return cards.Count;
            }
            else
            {
                return 0;
            }
        }
    }

    public event RemovedEventHandler CardRemoved; //delegate

    //This is your Card Draw method(), i just move it up here
    public int Draw()
    {
        int temp = cards[0];
        cards.RemoveAt(0);

        //to remove card from the stack
        if (CardRemoved != null)
        {
            CardRemoved(this, new CardRemoved(temp));
        }
        return temp;
    }

    public void push(int card)
    {
        cards.Add(card);
    }

    //calculate the hand value
    public int ChanceHandValue()
    {
        int sum = 0;
        foreach(int card in GetCards())
        {
            // give the remainder
            // 0         Ace
            // 1         2
            // 2         3
            //get the value of the deck when you add 1
            int cardRank = (card % 13) +1;
            sum = sum + cardRank;
        }
        return sum;
    }
    /*
    public int Value()
    {
        int sum = ChanceHandValue();
        if (sum >= 33)
        {
            sum = sum + 0;
        }
        else
        {
            sum = sum % 10;
        }
        return sum;
    }*/
    public void Shuffle()
    {
        cards.Clear();

        // Unshuffle card deck, go from 0 to 51
        // Shuffling implemented with a technique called the Fisher-Yates shuffle.
        for (int i = 0; i < 52; i++)
        {
            cards.Add(i);
        }
        int len = cards.Count;
        for (int k = 0; k < 5; ++k)
        {
            for (int i = len - 1; i >= 1; --i)
            {
                int j = new System.Random().Next(i);
                int temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
        }
    }

    void Start()
    {
        cards = new List<int>();
        if (isGameDeck)
        {
            Shuffle();
        }
    }
}