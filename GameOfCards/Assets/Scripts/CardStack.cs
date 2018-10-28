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

    //calculate the hand
    //get mod
    public int ChanceHandValue()
    {
        int sum = 0;
        foreach(int card in GetCards())
        {
            // give the remainder
            // 0         Ace
            // 1         2
            // 2         3
            int cardRank = card % 13; 
            if (cardRank <=9) // bigger than 10
            {
                cardRank += 1;
            }
            else
            {
                cardRank = 10;
            }
            sum = sum + cardRank;

            if (sum > 10)
            {
                sum = sum % 10;
            }
        }
        return sum;
    }
    public void Shuffle()
    {
        cards.Clear();

        // Unshuffle card deck, go from 0 to 51
        // I bring the Fisher-Yates method up here
        // Art Fisher-Yates method
        for (int i = 0; i < 52; i++)
        {
            cards.Add(i);
        }
        int len = cards.Count;
        for (int k = 0; k < 3; ++k)
        {
            for (int i = len - 1; i >= 1; --i)
            {
                int j = new System.Random().Next(i);
                int temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
        }
        // Given a list, use Fisher-Yates shuffle
        //public IList<Card> Shuffle(IList<Card> list)
        //{
           // var len = list.Count;
            //for (var k = 0; k < 3; ++k) //use Fisher-Yates multiple times
           // {
               // for (var i = len - 1; i >= 1; --i)
                //{
                   // var j = new System.Random().Next(i);
                    //var tmp = list[i];
                   // list[i] = list[j];
                    //list[j] = tmp;
               // }
            //}

            //return list;
        //}
    }

    void Start()
    {
        cards = new List<int>();
        if (isGameDeck)
        {
            Shuffle();
        }
    }
    /*public Deck()
    {
        System.Random rng = new System.Random();
        this.deck = new List<Card>();
        this.discard_pile = new List<Card>();

        for(var i = 1; i <= 4; ++i) // Loop through all suits (Hearts, Clubs, Diamonds,Spades)
        {
            for(var j = 1; j <= 13; ++j) // Loop through all ranks (1 to 13)
            {
                deck.Add(new Card((Card.SuitEnum)i, j));
            }
        }

        this.deck = Shuffle(this.deck);
    }

    public Card Draw()
    {
        if(this.deck.Count == 0)
        {
            Debug.Log("Deck out of cards.");
            return null;
        }
        else
        {
            Card c = deck[0];
            deck.RemoveAt(0); //Remove card after being drawn
            return c;
        }
        }
    }

    }*/
}