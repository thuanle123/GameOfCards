using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;
using System.Collections.Generic;
using System;

public class Chance {

    [Test]
    public void test_a_chance_setup_created()//Checks that players have the same amount of cards in there hand. 
    { 
        
        var player = GetCardStackMock();
        var opponent = GetCardStackMock();
        FillWithCards(player, 3);
        FillWithCards(opponent, 3);
        Assert.That(player.CardCount, Is.EqualTo(3));
        Assert.That(opponent.CardCount, Is.EqualTo(3));

    }
    /*
    void test_b_chance_is_swapped()//Checks that cards are swapped();
    {
        var chanceGame = GetChanceMock();
        for()
        {
        //add card value
        }

        for()
        {
        //add swapped value
        }

        compare the swapped value and unswapped value to see if changed
    }
    */

    void test_c_is_winner()//
    {
        var chanceGame = GetCardStackMock();

    }


    private CardStack GetCardStackMock()
    {
        var deck = Substitute.For<CardStack>();
        deck.isGameDeck = true;
        deck.cards = new List<int>();   // Initialize fake List to simulate putting in and drawing cards
        return deck;
    }


    private void GetChanceMock()
    {
        var chance = Substitute.For<Chance>();
        var player = GetCardStackMock();
        var dealer = GetCardStackMock();

        FillWithCards(player, 3);
        FillWithCards(dealer, 3);
    }


    private void  FillWithCards(CardStack c,  int numCards)
    {
        for (int i= 0  ; i < numCards; i++)
        {
            c.push(i);
        }
    }

}
