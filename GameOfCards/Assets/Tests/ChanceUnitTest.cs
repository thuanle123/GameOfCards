using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;
using System.Collections.Generic;
using System;

public class ChanceUnitTest {

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
    
    void test_b_chance_is_swapped()//Checks that cards are swapped();
    {
        var chanceGame = GetChanceMock();
        var player;
        var dealer;
        var swapPlayer;
        var swapDealer;
        
        for(int i=0; i<3; i++)
        {
            Debug.Log(player.cards[i]);
            Debug.Log(dealer.cards[i]);
        }

        chanceGame.swapCard(); //Swapping the value of the cards. 

        for (int i=0; i<3; i++)
        {
            Debug.Log(swapPlayer.cards[i]);
            Debug.Log(swapDealer.cards[i]);
        }

        //compare the swapped value and unswapped value to see if changed
    }

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


    private Chance GetChanceMock()
    {
        var chance = Substitute.For<Chance>();
        var player = GetCardStackMock();
        var dealer = GetCardStackMock();
        chance.Start();
        return <Chance>
    }


    private void  FillWithCards(CardStack c,  int numCards)
    {
        for (int i= 0  ; i < numCards; i++)
        {
            c.push(i);
        }
    }

}
