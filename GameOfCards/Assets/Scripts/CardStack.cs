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

    public bool HasCards
    {
        get
        {
            return cards != null && cards.Count > 0;
        }
    }

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

    // Get card count from card stack
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

    // Draw method draws the card to the player and dealer
    // It also remove the card from the List
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

    // This function helps with the rendering problem when pushing the card
    // Before, push will add it to position 0, making the old card renders on top of a new one
    public void InsertCard(int position, int card)
    {
        cards.Insert(position, card);
    }

    // Calculate the hand value
    // The value for 10 needs to be 0
    // Otherwise it won't distuingish between a 10 and face cards
    // Ten + Jack + Queen = 0, lowest hand
    // Any 3 three cards sum value = 30, highest hand
    public int ChanceSumValue()
    {
        int sum = 0;
        foreach(int card in GetCards())
        {
            // give the remainder
            // 0         Ace
            // 1         2
            // 2         3
            // get the value of the deck when you add 1
            // cardRank less than 9 represents Ace to Nine
            // cardRank equals 9 represents Ten
            // cardRank greater than or equal to 10 represents Jack, Queen, King
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

    // Calculate Blackjack hand value
    // Ace is worth either 1 or 11 points
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
            // get the value of the deck when you add 1
            // cardRank equals 0 represents Ace
            // cardRank less than 10 represents Two to Ten
            // cardRank greater than or equal to 10 represents Jack, Queen, King
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

    // Clear the List of cards
    public void Clear()
    {
        cards.Clear();
    }

    public void Start()
    {
        cards = new List<int>();
        if (isGameDeck)
        {
            Shuffle();
        }
    }
}