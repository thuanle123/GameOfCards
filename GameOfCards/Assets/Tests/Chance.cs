using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;
using System.Collections.Generic;
using System;

public class Chance {

    [Test]
    public void test_a_chance_setup_created() { //Checks that players have the same amount of cards in there hand. 
        
        var player = GetCardStackMock();
        var opponent = GetCardStackMock();
        FillWithCards(player, 3);
        FillWithCards(opponent, 3);
        Assert.That(player.CardCount, Is.EqualTo(3));
        Assert.That(opponent.CardCount, Is.EqualTo(3));

    }

    //void test_b_chance_


    private CardStack GetCardStackMock()
    {
        var deck = Substitute.For<CardStack>();
        deck = new CardStack();
        deck.isGameDeck = true;
        deck.cards = new List<int>();   // Initialize fake List to simulate putting in and drawing cards
        return deck;
    }

    private void  FillWithCards(CardStack c,  int numCards)
    {
        for (int i= 0  ; i < numCards; i++)
        {
            c.push(i);
        }
    }

}
