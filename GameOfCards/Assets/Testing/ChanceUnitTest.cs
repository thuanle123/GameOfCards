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
    

        chance.SwapCard();
        Assert.That(chance.player.ChanceSumValue() + chance.dealer.ChanceSumValue(), Is.EqualTo(sumBeforeSwap));
    }

    [Test]
    public void test_c_player_hand_loses() //Make sure the card value are being properly added together to determine a loser.
    {

        var chance = GetChanceMock();

        //Player has a score of 3
        chance.player.push(0);
        chance.player.push(13);
        chance.player.push(26);

        //Dealer has all face cards
        chance.dealer.push(10);
        chance.dealer.push(11);
        chance.dealer.push(12);

        Assert.That(chance.player.ChanceSumValue(), Is.LessThan(chance.dealer.ChanceSumValue())); // Assert  that player will lose
        Assert.That(chance.player.ChanceHandValue(), Is.LessThan(chance.dealer.ChanceHandValue()));
    }

    [Test]
    public void test_d_player_hand_wins() //Makes sure the card values are being properly added together to determine a winner
    {
        var chance = GetChanceMock();

        //Player has all face cards
        chance.player.push(10);
        chance.player.push(11);
        chance.player.push(12);

        //Dealer has score of 3
        chance.dealer.push(0);
        chance.dealer.push(13);
        chance.dealer.push(26);

        Assert.That(chance.player.ChanceSumValue(), Is.GreaterThan(chance.dealer.ChanceSumValue())); //Assert that player will win
        Assert.That(chance.player.ChanceHandValue(), Is.GreaterThan(chance.dealer.ChanceHandValue()));

    }

    [Test]
    public void test_e_player_dealer_draw() //Makes sure the card value are being added together to determine a draw
    {
        var chance = GetChanceMock();

        chance.player.push(10);
        chance.player.push(11);
        chance.player.push(12);

        chance.dealer.push(23);
        chance.dealer.push(24);
        chance.dealer.push(25);

        Assert.That(chance.player.ChanceSumValue(), Is.EqualTo(chance.dealer.ChanceSumValue())); //Assert that player and dealer will draw
        Assert.That(chance.player.ChanceHandValue(), Is.EqualTo(chance.dealer.ChanceHandValue()));


    }
  
    private Chance GetChanceMock()
    {
        var chance = Substitute.For<Chance>();
        chance.player = CardStackFunctions.GetCardStackMock();
        chance.dealer = CardStackFunctions.GetCardStackMock();
        chance.deck = CardStackFunctions.GetCardStackMock();
        chance.deck.Start();

        //chance.soundClips = Substitute.For<AudioManager>();
        //chance.soundClips.sounds = new Sounds[3];
        //chance.soundClips.sounds = 

        chance.playAgainButton = Substitute.For<Button>();
        chance.winnerText =Substitute.For<Text>();
        chance.playerScore =Substitute.For<Text>();
        chance.dealerScore = Substitute.For<Text>();
        chance.playerHandScore = Substitute.For<Text>();
        chance.dealerHandScore = Substitute.For<Text>();
        chance.endTurnButton = Substitute.For<Button>();
        chance.swapCardButton = Substitute.For<Button>();
        chance.nextRoundButton = Substitute.For<Button>();

        chance.Start();

        return chance;
    }
}
