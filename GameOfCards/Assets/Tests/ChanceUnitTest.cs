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
        var chance = GetChanceMock();
        Assert.That(chance.player.CardCount, Is.EqualTo(3));
        Assert.That(chance.dealer.CardCount, Is.EqualTo(3));
    }

    [Test]
    public void test_b_chance_swap()//Checks to see if the swap function in chance is working correctly
    {
        var chance = GetChanceMock();
        int sumBeforeSwap = chance.player.ChanceSumValue() + chance.dealer.ChanceSumValue();

        chance.swapCard();

        Assert.That(chance.player.ChanceSumValue() + chance.dealer.ChanceSumValue(), Is.EqualTo(sumBeforeSwap));
    }

    [Test]
    public void test_c_player_hand_loses() //Make sure the card value are being properly added together to determine a loser.
    {
        var player = CardStackFunctions.GetCardStackMock();
        var dealer = CardStackFunctions.GetCardStackMock();

        //Player has a score of 3
        player.push(0);
        player.push(13);
        player.push(26);

        //Dealer has all face cards
        dealer.push(10);
        dealer.push(11);
        dealer.push(12);

        Assert.That(player.ChanceSumValue(), Is.LessThan(dealer.ChanceSumValue())); // Assert  that player will lose
        Assert.That(player.ChanceHandValue(), Is.LessThan(dealer.ChanceHandValue()));
    }

    [Test]
    public void test_d_player_hand_wins() //Makes sure the card values are being properly added together to determine a winner
    {
        var player = CardStackFunctions.GetCardStackMock();
        var dealer = CardStackFunctions.GetCardStackMock();

        //Player has all face cards
        player.push(10);
        player.push(11);
        player.push(12);

        //Dealer has score of 3
        dealer.push(0);
        dealer.push(13);
        dealer.push(26);

        Assert.That(player.ChanceSumValue(), Is.GreaterThan(dealer.ChanceSumValue())); //Assert that player will win
        Assert.That(player.ChanceHandValue(), Is.GreaterThan(dealer.ChanceHandValue()));

    }

    [Test]
    public void test_e_player_dealer_draw() //Makes sure the card value are being added together to determine a draw
    {
        var player = CardStackFunctions.GetCardStackMock();
        var dealer = CardStackFunctions.GetCardStackMock();

        player.push(10);
        player.push(11);
        player.push(12);

        dealer.push(23);
        dealer.push(24);
        dealer.push(25);

        Assert.That(player.ChanceSumValue(), Is.EqualTo(dealer.ChanceSumValue())); //Assert that player and dealer will draw
        Assert.That(player.ChanceHandValue(), Is.EqualTo(dealer.ChanceHandValue()));


    }
  
    private Chance GetChanceMock()
    {
        var chance = Substitute.For<Chance>();
        chance.player = CardStackFunctions.GetCardStackMock();
        chance.dealer = CardStackFunctions.GetCardStackMock();
        chance.deck = CardStackFunctions.GetCardStackMock();

        chance.playAgainButton = Substitute.For<Button>();
        chance.winnerText =Substitute.For<Text>();
        chance.playerScore =Substitute.For<Text>();
        chance.dealerScore = Substitute.For<Text>();
        chance.playerHandScore = Substitute.For<Text>();
        chance.dealerHandScore = Substitute.For<Text>();
        chance.endTurnButton = Substitute.For<Button>();
        chance.swapCardButton = Substitute.For<Button>();
        chance.nextRoundButton = Substitute.For<Button>();

        chance.StartGame();

        return chance;
    }
}
