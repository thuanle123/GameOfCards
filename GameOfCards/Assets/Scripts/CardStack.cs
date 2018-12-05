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

    // Public method to numerate through cards.
    public IEnumerable<int> GetCards()
    {
        foreach (int i in cards)
        {
            yield return i;
        }
    }

    // Gets number of cards in the stack.
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

    // Draws a card from the stack.
    // To draw from the top, call Draw(0);
    public int Draw(int position)
    {
        int temp = cards[position];
        cards.RemoveAt(position);

        // Actually removes card from the stack.
        if (CardRemoved != null)
        {
            CardRemoved(this, new CardRemoved(temp));
        }
        return temp;
    }

    // Pushes card to top of the stack.
    public void push(int card)
    {
        cards.Add(card);
    }

    // Inserts a card to a specific position in the list.
    public void InsertCard(int position, int card)
    {
        cards.Insert(position, card);
    }

    // Calculate the hand value for Chance.
    // Face cards have a value of 10, and tens have a value of 0.
    // This allows us to check for a CHANCE hand (All face cards.)
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

    // Calculate the hand value for blackjack.
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