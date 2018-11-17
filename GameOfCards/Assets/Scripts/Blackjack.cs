using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blackjack : MonoBehaviour {

    // Variables
    public CardStack dealer;
    public CardStack player;
    public CardStack deck;

    public Text winnerText;
    public Text playerScore;
    public Text dealerScore;
    public Text playerHandScore;
    public Text dealerHandScore;

    public Button endTurnButton;
    public Button hitButton;
    public Button nextRoundButton;
    public Button playAgainButton;
    public Button standButton;

    public int dealersFirstCard = -1;

    /* Blackjack Rules:
     * The player attemps to beat the dealer by getting a count as 
     * close to 21 as possible, without going over 21.
     * 
     * Card Values / Scoring:
     * It is up to each individial player if an ace is worth 1 or 11. 
     * Face cards are 10 and any other card is its face value.
     * 
     * The Deal:
     * The dealer gives one card face up to each player, and then one card face up to himself.
     * Another round of cards is then dealt face up to each plauer, 
     * but the dealer takes his second card face down.
     * 
     * The Play:
     * The player must decide whether to "stand" (not ask for another card) or 
     * "hit" (ask for nother cad in an attempt to get closer to a count of 21, 
     * or even hit 21 exactly. Thus, a player may stand on the two cards originally dealt him, 
     * or he may ask the dealer for additional cards, one at a time, until he either decides to stand
     * on the total (if it is 21 or under) or goes "bust" (if it is over 21). In the latter case,
     * the player loses.
     * 
     * The Dealer's Play:
     * When the dealer has served every okayer, his face down card is turned up. 
     * If the total is 17 or more, he must stand. If the total is 16 or under,
     * he must take a card. He must continue to take cards until the total is 17
     * or more, at which point the dealer must stand.
     * 
     */

    // Use this for initialization
    void Start () {
        StartGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Hit()
    {
        player.push(deck.Draw());
        Debug.Log("Player score = " + player.BlackjackSumValue());
        if (player.BlackjackSumValue() > 21)
        {
            // Player is bust.
            hitButton.interactable = false;
            standButton.interactable = false;
            StartCoroutine(DealersTurn());
        }
    }

    public void Stand()
    {
        // dealer
        hitButton.interactable = false;
        standButton.interactable = false;


        StartCoroutine(DealersTurn());
    }

    public void PlayAgain()
    {
        playAgainButton.interactable = false;

        //player.GetComponent<CardStackView>().Clear();
        //dealer.GetComponent<cardStackView>().Clear();
        //deck.GetComponent<cardStackView>().Clear();
        //deck.CreateDeck();

        dealersFirstCard = -1;

        hitButton.interactable = true;
        standButton.interactable = true;

        StartGame();
    }

    void StartGame ()
    {
        // Empty the hands.
        while (player.HasCards)
        {
            player.Draw();
        }
        while (dealer.HasCards)
        {
            dealer.Draw();
        }

        // Draw the hands
        for (int i = 0; i < 2; i++)
        {
            player.push(deck.Draw());
            dealer.push(deck.Draw());
        }
    }

    void dealerHit ()
    {
        // Draw card -> check for bust?
        int card = deck.Draw();

        if (dealersFirstCard < 0)
        {
            dealersFirstCard = card;
        }

        dealer.push(card);
        if (dealer.CardCount >= 2)
        {
            //CardStackView view = dealer.GetComponent<CardStackView>();
            //view.toggle(card, true);
        }
    }

    IEnumerator DealersTurn()
    {

        //CardStackView view = dealer.GetComponent<CardStackView>();
        //view.toggle(dealersfirstcard, true);
        //view.ShowCards();
        //yield return new WaitForSeconds(1f);

        while (dealer.BlackjackSumValue() < 17)
        {
            dealerHit();
            yield return new WaitForSeconds(1f);
        }

        int dealerScore = dealer.BlackjackSumValue();
        int playerScore = player.BlackjackSumValue();

        if(player.BlackjackSumValue() > 21 || (dealer.BlackjackSumValue() >= player.BlackjackSumValue() && dealer.BlackjackSumValue() <= 21))
        {
            winnerText.text = "You lose.";
        } else if (dealer.BlackjackSumValue() > 21 || (player.BlackjackSumValue() <= 21 && player.BlackjackSumValue() > dealer.BlackjackSumValue()))
        {
            winnerText.text = "You win!";
        } else
        {
            winnerText.text = "The house wins.";
        }

        yield return new WaitForSeconds(1f);
        playAgainButton.interactable = true;
    }
}
