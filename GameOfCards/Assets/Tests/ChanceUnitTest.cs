using UnityEngine.UI;
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
    public void test_b_chance_cards_added()//Checks to see if cards in chance are added to the hand
    {
        
        var chanceGame = GetChanceMock();
        var dealer = chanceGame.dealer;
        FillWithCards(dealer, 3);
        Assert.That(dealer.CardCount, Is.EqualTo(3));
    }

    [Test]
    public void test_c_chance_cards_have_value()//Checks to see if the card holds a value
    {
        var chanceGame = GetChanceMock();
        var dealer = chanceGame.dealer;
        System.Random r = new System.Random();
        var card = r.Next(0, 52); //picks a random card out of the deck 
        dealer.push(card);
        Assert.That(dealer.Draw, Is.GreaterThan(0));

    }

    [Test]
    public void test_d_chance_swap()//Checks to see if the swap function in chance is working correctly
    {
        var chanceGame = GetChanceMock();
        //chanceGame.StartGame();
        System.Random r = new System.Random();
        var dealer = chanceGame.dealer;
        var player = chanceGame.player;
        var playerCard = r.Next(0,52); //picks a random card out of the deck
        var dealerCard = r.Next(0,52); //picks a random card out of the deck
        dealer.push(dealerCard);
        player.push(playerCard);
        var unswappedDealer = dealer; //Hold the value of dealer for comparison
        chanceGame.swapCard();
        Assert.That(player, Is.EqualTo(unswappedDealer));
        
    }

    [Test]
    public void test_e_chance_cards_value_added() //Make sure the card value are being properly added together.
    {
        System.Random r = new System.Random();
        var chanceGame = GetChanceMock();
        var dealer = chanceGame.dealer;
        var cardOne = r.Next(0, 52);
        var cardTwo = r.Next(0, 52);
        var value = cardOne + cardTwo; //Adds cards together
        dealer.push(value);
        Assert.That(value, Is.EqualTo(cardOne + cardTwo)); //compare to see if card are being pushed and added properly
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
        chance.player = GetCardStackMock();
        chance.dealer = GetCardStackMock();
        
        chance.playAgainButton = Substitute.For<Button>();
        chance.winnerText =Substitute.For<Text>();
        chance.playerScore =Substitute.For<Text>();
        chance.dealerScore = Substitute.For<Text>();
        chance.playerHandScore = Substitute.For<Text>();
        chance.dealerHandScore = Substitute.For<Text>();
        chance.endTurnButton = Substitute.For<Button>();
        chance.swapCardButton = Substitute.For<Button>();
        chance.nextRoundButton = Substitute.For<Button>();

        return chance;
    }


    private void  FillWithCards(CardStack c,  int numCards)
    {
        for (var i= 0  ; i < numCards; i++)
        {
            c.push(i);
        }
    }

    

}
