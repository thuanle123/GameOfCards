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
    public void test_a_setup_created() //Checks that players have the same amount of cards in there hand.
    {
        var blackJack = GetBlackJackMock();
        Assert.That(blackJack.dealer.CardCount, Is.GreaterThan(0));
        Assert.That(blackJack.player.CardCount, Is.GreaterThan(0));
    }

    [Test]
    public void test_b_player_win()
    {
        var dealer = CardStackFunctions.GetCardStackMock();
        var player = CardStackFunctions.GetCardStackMock();

        dealer.push(1); // 2 of Clubs
        dealer.push(2); // 3 of Clubs

        player.push(13); // Ace of Diamonds
        player.push(14); // 2 of Diamonds

        Assert.That(player.BlackjackSumValue(), Is.GreaterThan(dealer.BlackjackSumValue())); // Assert  that player will win

    }

    [Test]
    public void test_c_player_lose()
    {
        var dealer = CardStackFunctions.GetCardStackMock();
        var player = CardStackFunctions.GetCardStackMock();

        dealer.push(13); // Ace of Diamonds
        dealer.push(14); // 2 of Diamonds

        player.push(1); // 2 of Clubs
        player.push(2); // 3 of Clubs

        Assert.That(player.BlackjackSumValue(), Is.LessThan(dealer.BlackjackSumValue())); // Assert  that player will lose

    }

    [Test]
    public void test_d_draw_determined()
    {
        var dealer = CardStackFunctions.GetCardStackMock();
        var player = CardStackFunctions.GetCardStackMock();

        dealer.push(10); // Jack of Clubs
        dealer.push(11); // Queen of Clubs

        player.push(23); // Jack of Diamonds
        player.push(24); // Queen of Diamonds

        Assert.That(player.BlackjackSumValue(), Is.EqualTo(dealer.BlackjackSumValue())); // Assert  that player will tie with dealer

    }

    private Blackjack GetBlackJackMock() //Simulates BlackJack
   {
        var blackJack = Substitute.For<Blackjack>();
        blackJack.dealer = CardStackFunctions.GetCardStackMock();
        blackJack.player = CardStackFunctions.GetCardStackMock();
        blackJack.deck = CardStackFunctions.GetCardStackMock();
        CardStackFunctions.FillWithCards(blackJack.deck, 52);


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

        //blackJack.HandCover = Substitute.For<GameObject>();
        //blackJack.HandCover.AddComponent<SpriteRenderer>();

        blackJack.StartGame();

        return blackJack; 
    }
}