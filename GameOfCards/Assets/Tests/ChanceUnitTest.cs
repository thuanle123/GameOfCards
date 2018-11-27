using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class ChanceUnitTest {

    [Test]
    public void test_a_chance_setup_created()//Checks that players have the same amount of cards in there hand. 
    {
        var chance = GetChanceMock();
        Assert.That(chance.player.CardCount, Is.EqualTo(3));
        Assert.That(chance.dealer.CardCount, Is.EqualTo(3));
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
        //var chanceGame = GetCardStackMock();

    }


    private Chance GetChanceMock()
    {
        var chance = Substitute.For<Chance>();
        chance.player = CardStackFunctions.GetCardStackMock();
        chance.dealer = CardStackFunctions.GetCardStackMock();
        chance.deck = CardStackFunctions.GetCardStackMock();
        chance.playerScore = Substitute.For<Text>();
        chance.winnerText = Substitute.For<Text>();
        chance.dealerScore = Substitute.For<Text>();
        chance.playerHandScore = Substitute.For<Text>();
        chance.dealerHandScore = Substitute.For<Text>();
        chance.endTurnButton = Substitute.For<Button>();
        chance.swapCardButton = Substitute.For<Button>();
        chance.nextRoundButton = Substitute.For<Button>();

        CardStackFunctions.FillWithCards(chance.deck, 52);

        chance.Start();
        return chance;
    }
}
