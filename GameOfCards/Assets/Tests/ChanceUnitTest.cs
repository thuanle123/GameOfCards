using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;
using System.Collections.Generic;
using System;
// READ THIS!!!
// When you check for the sum
// Any Face Cards * 3 (Jack + Jack + Queen, King + Queen + Queen, etc..)
// If value = 30, it is correct
// If the value is less than 30, you take the (sum mod 10) of it
// which mean 10, Queen, Jack, King can be treat as a 0 if you don't have all three
// I made the value of King,Queen,Jack value of 11 because
// I couldn't distuingish between 10 and those 3
// but all i have to do is make 10 = 0 then everything solve

// to recap, if a player/dealer have 3 face cards, sum = 30
// otherwise sum mod 10
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
