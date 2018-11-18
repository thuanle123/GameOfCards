using NUnit.Framework;

/*  ================= Unit test for CardStack.cs =================
 *  -Uses NSubstitute library: https://github.com/nsubstitute/nsubstitute
 *  -References:
 *      -https://blogs.unity3d.com/2014/06/03/unit-testing-part-2-unit-testing-monobehaviours/
 *      -How to use NSubstitute in Unity: https://www.youtube.com/watch?v=xSa2S-W7x48
 */

public class DeckUnitTest
{
    // TESTS:
    [Test]
    public void test_a_empty_deck_created()  // Check if empty deck with 0 cards can be created
    {
        var deck = CardStackFunctions.GetCardStackMock();
        Assert.That(deck.CardCount, Is.EqualTo(0));
    }

    [Test]
    public void test_b_deck_holds_one_card()  // Check if it can store 1 card
    {
        var deck = CardStackFunctions.GetCardStackMock();
        deck.push(0);
        Assert.That(deck.CardCount, Is.EqualTo(1));
    }

    [Test]
    public void test_c_deck_holds_draws_card_correctly()   // Check if it can hold and draw 1 random card
    {
        var deck = CardStackFunctions.GetCardStackMock();
        System.Random r = new System.Random();  // Create a random number generator
        var randomCard = r.Next(0, 52);         // Generate a random card between 0 to 51
        deck.push(randomCard);                  // Push it onto the deck
        Assert.That(deck.Draw, Is.EqualTo(randomCard));
    }

    [Test]
    public void test_d_deck_holds_52_cards()   // Check if it can hold a maximum of 52 cards
    {
        var deck = CardStackFunctions.GetCardStackMock();
        CardStackFunctions.FillWithCards(deck, 52);    // Fill it with 52 cards
        Assert.That(deck.CardCount, Is.EqualTo(52));
    }

    [Test]
    public void test_e_is_shuffled() // Checks to see if cards are shuffled
    {

        //Creates 2 decks old and new, will be the same cards one will be shuffled and one will not

        // This will not change 
        var unshuffledDeck = CardStackFunctions.GetCardStackMock();
        // This will be shuffled
        var shuffledDeck = CardStackFunctions.GetCardStackMock();

        CardStackFunctions.FillWithCards(unshuffledDeck, 52);
        CardStackFunctions.FillWithCards(shuffledDeck, 52);

        shuffledDeck.Shuffle(); // Shuffles only one deck

        int notEqualCount = 0;
        for (int i = 0; i < 52; i++)
        {
            int unshuffledCard = unshuffledDeck.Draw();
            int shuffledCard = shuffledDeck.Draw();
            if (unshuffledCard != shuffledCard)
            {
                notEqualCount++;
            }
        }
        Assert.That(notEqualCount, Is.GreaterThan(30));
    }


}
