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
    [Test]
    void test_b_chance_is_dealer_swapped()//Checks that cards are swapped() for dealer
    {
        var chanceGame = GetChanceMock();
        var swapDealer= Random.Range(0, 3); //Picks random cards from deck
        var player = Random.Range(0, 3); //Picks random cards from deck
        var dealer = swapDealer;

        for(int i=0; i<3; i++)
        {
           dealer.cards[i]=+dealer.cards[i]; //Adds the value of all cards together
           player.cards[i] = +player.cards[i]; //cards being swapped with 
        }

        chanceGame.swapCard(); //Swapping the value of the cards. 
        for (int i = 0; i < 3; i++)
        {
            swapDealer.cards[i] =+swapDealer.cards[i]; //Adds value of swapped cards together
        }

        Assert.That(swapDealer.value, Is.NotEqualTo(dealer.value)); //compare the swapped value and unswapped value to see if swapped value is differnt from unswapped
    }

    [Test]
    void test_c_chance_is_player_swapped()//Checks that cards are swapped() for dealer;
    {
        var chanceGame = GetChanceMock();
        var swapPlayer= Random.Range(0, 3); //Picks random cards from deck
        var dealer = Random.Range(0, 3); //Picks random cards from deck
        var player = swapPlayer;
        for (int i = 0; i < 3; i++)
        {
            player.cards[i] =+player.cards[i]; //Adds the value of all cards together
            dealer.cards[i] = +dealer.cards[i]; //Cards being swapped
        }

        chanceGame.swapCard(); //Swapping the value of the cards. 
       
        for (int i = 0; i < 3; i++)
        {
            swapPlayer.cards[i] =+swapPlayer.cards[i]; //Adds value of swapped cards together
        }
        Assert.That(swapPlayer.value, Is.NotEqualTo(player.value)); //compare the swapped value and unswapped value to see if swapped value is differnt from unswapped
    }

    [Test]
    void test_d_chance_is_winner()//Checks to see if there is a winner of the game. 
    {
        var chanceGame = GetCardStackMock();

    }

    [Test]
    void test_e_chance_cards_hold_value()//Checks to see if cards have a value
    {
        var chanceGame = GetCardStackMock();
        var cardHand = GetCardStackMock();
        FillWithCards(cardHand, 3);
        for (int i=0; i<3; i++)
        {
            cardHand.cards[i] =+cardHand.cards[i]; //Adds the value of the cards
        }
        Assert.That(cardHand.value, Is.GreaterThan(30));


    }

    [Test]
    void test_f_chance_game_is_done()//Checks to see if game resets when game is done
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
