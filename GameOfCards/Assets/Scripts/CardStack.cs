using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStack : MonoBehaviour
{
    // this list is private
    public List<int> cards;

    public bool isGameDeck;
    public event RemovedEventHandler CardRemoved; //delegate

    public bool HasCards{ get { return cards != null && cards.Count > 0; } }

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

    //This is your Card Draw method(), i just move it up here
    public int Draw(int position)
    {
        int temp = cards[position];
        cards.RemoveAt(position);

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

    public void InsertCard(int position, int card)
    {
        cards.Insert(position, card);
    }

    //calculate the hand value
    public int ChanceSumValue()
    {
        int sum = 0;
        foreach(int card in GetCards())
        {
            // give the remainder
            // 0         Ace
            // 1         2
            // 2         3
            //get the value of the deck when you add 1
            int cardRank = (card % 13);
            
            if (cardRank < 9)
            {
                cardRank += 1;
            }
            else if (cardRank == 9)
            {
                cardRank = 0;
            }
            else if (cardRank >= 10)
            {
                cardRank = 10;
            }
            sum = sum + cardRank;
        }
        return sum;
    }

    // same as ChanceSumValue() but face cards are worth 10.
    public int BlackjackSumValue()
    {
        int sum = 0;
        int aces = 0;
        foreach (int card in GetCards())
        {
            // give the remainder
            // 0         Ace
            // 1         2
            // 2         3
            //get the value of the deck when you add 1
            int cardRank = (card % 13);

            if (cardRank == 0)
            {
                aces++;
            }
            else if (cardRank < 10)
            {
                cardRank += 1;
                sum = sum + cardRank;
            }
            else if (cardRank >= 10)
            {
                cardRank = 10;
                sum = sum + cardRank;
            }  
        }
        for (int i = 0; i < aces; i++)
        {
            if (sum + 11 <= 21)
            {
                sum = sum + 11;
            }
            else
            {
                sum = sum + 1;
            }
        }
        return sum;
    }
    
    public int ChanceHandValue()
    {
        int sum = ChanceSumValue();
        if (sum < 30)
        {
            sum = sum % 10;
        }
        return sum;
    }

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

        Debug.Log("Number of Cards = " + CardCount);
    }

    public void Clear()
    {
        cards.Clear();
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