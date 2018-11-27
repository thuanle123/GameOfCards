using System.Collections.Generic;
using NSubstitute;


public class CardStackFunctions
{
    public static CardStack GetCardStackMock()     // Generates a stub for CardStack that simulates the deck
    {
        var deck = Substitute.For<CardStack>();
        deck.isGameDeck = true;
        List<int> l = new List<int>();
        deck.cards = l;   // Initialize fake List to simulate putting in and drawing cards
        return deck;
    }


    public static void FillWithCards(CardStack c, int numCards) // Fills a given CardStack with 52 cards
    {
        for (int i = 0; i < numCards; i++)
        {
            c.push(i);   // Add 52 cards, all in sequence, into the both
        }
    }
}

