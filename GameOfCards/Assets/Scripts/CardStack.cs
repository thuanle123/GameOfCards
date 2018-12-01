using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStack : MonoBehaviour
{
    // Public list to access it for other function
    // RemovedEventHandler is a delegate, a reference pointer to a method
    // It serves as a intermediate scripting, in this case to remove the card
    public List<int> cards;
    public bool isGameDeck;
    public event RemovedEventHandler CardRemoved;

    // GetCards starts a coroutine
    // The yield statement is there so that it can be paused at any moment
    // We need a public method to numerate through them all
    public IEnumerable<int> GetCards()
    {
        foreach (int i in cards)
        {
            yield return i;
        }
    }

    public bool HasCards
    {
        get
        {
            return cards.Count > 0 && cards != null;
        }
    }

    //Get card count from the list
    public int CardCount
    {
        get
        {
            if (cards == null)
            {
                return 0;
            }
            return cards.Count;
        }
    }

    // Draw method draws the card to the player and dealer
    // It also remove the card from the List
    public int Draw()
    {
        int temp = cards[0];
        cards.RemoveAt(0);

        //to remove card from the list
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
    // The value for 10 needs to be 0
    // Otherwise it won't distuingish between a 10 and face cards
    // 10 + Jack + Queen = 0, lowest hand
    // Any 3 three cards = 30, highest hand
    public int ChanceSumValue()
    {
        // give the remainder
        // 0         Ace
        // 1         2
        // 2         3
        //get the value of the deck when you add 1
        int sum = 0;
        foreach(int card in GetCards())
        {
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

    // Problem before: the value always calculate on each card drawn
    // We want to calculate the sum after 3 cards have been drawn
    // ChanceHandValue fixes that problem
    // If the hand does not contain 3 face cards (sum of 30)
    // the sum will be sum mod 10
    public int ChanceHandValue()
    {
        int sum = ChanceSumValue();
        if (sum < 30)
        {
            sum = sum % 10;
        }
        return sum;
    }

    // Similar to ChanceHandValue
    // Except Ace can be worth 1 or 11 points
    public int BlackjackSumValue()
    {
        // give the remainder
        // 0         Ace
        // 1         2
        // 2         3
        //get the value of the deck when you add 1
        int sum = 0;
        int aces = 0;
        int i = 0;
        foreach (int card in GetCards())
        {
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
        while (i < aces)
        {
            if (sum + 11 <= 21)
            {
                sum += 11;
            }
            else
            {
                sum += 1;
            }
            i++;
        }
        return sum;
    }

    // Unshuffle card deck, go from 0 to 51
    // Shuffling implemented with a technique called the Fisher-Yates shuffle.
    // The deck will be shuffle 5 times
    public void Shuffle()
    {
        cards.Clear();
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

    // Clear function clear the List of cards
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