using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;
using System.Collections.Generic;
using System;

//Will add to deck test to prevent duplication. 

/*  ================= Unit test for CardStack.cs =================
 *  -Uses NSubstitute library: https://github.com/nsubstitute/nsubstitute
 *  -References:
 *      -https://blogs.unity3d.com/2014/06/03/unit-testing-part-2-unit-testing-monobehaviours/
 *      -How to use NSubstitute in Unity: https://www.youtube.com/watch?v=xSa2S-W7x48
 */


public class ShuffleUnitTest{

    [Test]
    public void IsShuffled() { //Checks to see if cards are shuffled

        //Creates 2 decks old and new, will be the same cards one will be shuffled and one will not
    
        //This will not change 
        var oldDeck = GetCardStackMock();
        //This will be shuffled
        var newDeck = GetCardStackMock();
        newDeck.Shuffle(); //Shuffles the new deck

        for(int i=0; i == 52; i++)
        {
            
        }
        Assert.That(newDeck, Is.EqualTo(oldDeck));
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
