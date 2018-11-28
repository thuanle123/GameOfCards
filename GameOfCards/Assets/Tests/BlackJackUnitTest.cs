using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class BlackJack {

    [Test]
    public void test_a_blackjack_setup_created()//Checks that players have the same amount of cards in there hand. 
    {

        var player = GetCardStackMock();
        var opponent = GetCardStackMock();
        FillWithCards(player, 3);
        FillWithCards(opponent, 3);
        Assert.That(player.CardCount, Is.EqualTo(3));
        Assert.That(opponent.CardCount, Is.EqualTo(3));

    }

    [Test]
    public void test_b_blackjack_21_is_winner()//Checks to see that 21 will always be the highest score.
    {

    }

    public void test_c_blackjack_cards_counted()//Checks to see that cards are counted in when hitting. 
    {

    }
}
