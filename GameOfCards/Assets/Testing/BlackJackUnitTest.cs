using UnityEngine.UI;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;
using System.Collections.Generic;
using System;

public class BlackJackUnitTest  
{
    [Test]
    public void blackjack_a_setup_created() //Checks that players have the same amount of cards in there hand.
    {
        var blackJack = GetBlackJackMock();
        Assert.That(blackJack.dealer.CardCount, Is.GreaterThan(0));
        Assert.That(blackJack.player.CardCount, Is.GreaterThan(0));
    }

    [Test]
    public void blackjack_b_player_win()
    {
        var blackJack = GetBlackJackMock();

        blackJack.dealer.push(1);
        blackJack.dealer.push(2);

        blackJack.player.push(13);
        blackJack.player.push(14);

        Assert.That(blackJack.player.BlackjackSumValue(), Is.GreaterThan(blackJack.dealer.BlackjackSumValue())); // Assert  that player will win

    }

    [Test]
    public void blackjack_c_player_lose()
    {
        var blackJack = GetBlackJackMock();

        blackJack.dealer.push(13);
        blackJack.dealer.push(14);

        blackJack.player.push(1);
        blackJack.player.push(2);

        Assert.That(blackJack.player.BlackjackSumValue(), Is.LessThan(blackJack.dealer.BlackjackSumValue())); // Assert  that player will lose

    }

    [Test]
    public void blackjack_d_draw_determined()
    {
        var blackJack = GetBlackJackMock();

        blackJack.dealer.push(10);
        blackJack.dealer.push(11);

        blackJack.player.push(23);
        blackJack.player.push(24);

        Assert.That(blackJack.player.BlackjackSumValue(), Is.EqualTo(blackJack.dealer.BlackjackSumValue())); // Assert  that player will tie with dealer

    }

    private Blackjack GetBlackJackMock() //Simulates BlackJack
   {
        var blackJack = Substitute.For<Blackjack>();
        blackJack.dealer = CardStackFunctions.GetCardStackMock();
        blackJack.player = CardStackFunctions.GetCardStackMock();
        blackJack.deck = CardStackFunctions.GetCardStackMock();

        blackJack.winnerText = Substitute.For<Text>(); 
        blackJack.playerScore = Substitute.For<Text>(); 
        blackJack.dealerScore = Substitute.For<Text>(); 
        blackJack.playerHandScore = Substitute.For<Text>(); 
        blackJack.dealerHandScore = Substitute.For<Text>();

        blackJack.endTurnButton = Substitute.For<Button>();
        blackJack.hitButton = Substitute.For<Button>();
        blackJack.nextRoundButton = Substitute.For<Button>();
        blackJack.playAgainButton  = Substitute.For<Button>();
        blackJack.standButton = Substitute.For<Button>();

        blackJack.StartGame();

        return blackJack; 
    }
}