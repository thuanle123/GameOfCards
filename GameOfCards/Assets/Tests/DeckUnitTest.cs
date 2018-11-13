using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;
using System.Collections.Generic;
using System;

/*  ================= Unit test for CardStack.cs =================
 *  -Uses NSubstitute library: https://github.com/nsubstitute/nsubstitute
 *  -References:
 *      -https://blogs.unity3d.com/2014/06/03/unit-testing-part-2-unit-testing-monobehaviours/
 *      -How to use NSubstitute in Unity: https://www.youtube.com/watch?v=xSa2S-W7x48
 */

public class DeckUnitTest {

    [Test]
    public void EmptyDeckCreated()  // Check if empty deck with 0 cards can be created
    {
        var deck = GetCardStackMock();
        Assert.That(deck.CardCount, Is.EqualTo(0));
    }

    [Test]
    public void DeckHoldsOneCard()  // Check if it can store 1 card
    {
        var deck = GetCardStackMock();
        deck.push(0);
        Assert.That(deck.CardCount, Is.EqualTo(1));
    }

    [Test]
    public void DeckHoldsDrawsCardCorrectly()   // Check if it can hold and draw 1 random card
    {
        var deck = GetCardStackMock();
        System.Random r = new System.Random();  // Create a random number generator
        var randomCard = r.Next(0, 52);         // Generate a random card between 0 to 51
        deck.push(randomCard);                  // Push it onto the deck
        Assert.That(deck.Draw, Is.EqualTo(randomCard));
    }

    [Test]
    public void DeckHolds52Cards()   // Check if it can hold a maximum of 52 cards
    {
        var deck = GetCardStackMock();
        for(int i = 0; i < 52; i++)
        {
            deck.push(i);   // Add 52 cards, all in sequence, into the deck
        }
        Assert.That(deck.CardCount, Is.EqualTo(52));
    }

    // Generates a stub for CardStack that simulates the deck
    private CardStack GetCardStackMock()
    {
        var deck = Substitute.For<CardStack>();
        deck = new CardStack();
        deck.isGameDeck = true;
        deck.cards = new List<int>();   // Initialize fake List to simulate putting in and drawing cards
        return deck;
    }
}
