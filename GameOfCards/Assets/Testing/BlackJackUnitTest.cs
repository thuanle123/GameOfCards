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
    public void blackjack_a_setup_created()
    {
        var blackJack = GetBlackJackMock();
        Assert.That(blackJack.dealer.CardCount, Is.GreaterThan(0));
        Assert.That(blackJack.player.CardCount, Is.GreaterThan(0));
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