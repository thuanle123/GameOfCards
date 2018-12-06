using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blackjack : MonoBehaviour
{
    // Variables
    public CardStack dealer;
    public CardStack player;
    public CardStack deck;

    public Text winnerText;
    public Text gameOverText;
    public Text playerScore;
    public Text dealerScore;
    public Text playerHandScore;
    public Text dealerHandScore;

    public Button hitButton;
    public Button nextRoundButton;
    public Button playAgainButton;
    public Button standButton;

    public int roundWonByPlayer = 0;
    public int roundWonByDealer = 0;

    public GameObject HandCover;


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

    public void Start() {
        roundWonByPlayer = roundWonByDealer = 0;
        playerScore.text = roundWonByPlayer.ToString();
        dealerScore.text = roundWonByDealer.ToString();
        winnerText.text = "";

        StartRound();
        FindObjectOfType<AudioManager>().Play("cardShuffle");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Open panel function
    public void CoverHand()
    {
        //Checks to see if there is a HandCover.
        // If the HandCover is being displayed, close it. 
        //If the button has not been pressed, show HandCover.
        if (HandCover != null)
        {
            if (HandCover.activeSelf)
            {
                HandCover.SetActive(false);
            }
            else
            {
                HandCover.SetActive(true);
            }
        }
    }

    public void Hit()
    {
        FindObjectOfType<AudioManager>().Play("cardSlide6");
        player.push(deck.Draw(0));
        Debug.Log("Player score = " + player.BlackjackSumValue());
        playerHandScore.text = player.BlackjackSumValue().ToString();
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
        hitButton.interactable = false;
        standButton.interactable = false;
        StartCoroutine(DealersTurn());
    }

    public void PlayAgain()
    {
        playAgainButton.interactable = true;
        hitButton.interactable = true;
        standButton.interactable = true;
        CoverHand();

        StartRound();
        FindObjectOfType<AudioManager>().Play("cardSlide6");
    }

    public void NewGame()
    {
        deck.Shuffle();
        HandCover.SetActive(true);
        hitButton.interactable = true;
        standButton.interactable = true;
        Start();
    }

    void StartRound()
    {
        // Empty the hands.
        while (player.HasCards)
        {
            player.Draw(0);
        }
        while (dealer.HasCards)
        {
            dealer.Draw(0);
        }
        // Draw the hands
        for (int i = 0; i < 2; i++)
        {
            player.push(deck.Draw(0));
            dealer.push(deck.Draw(0));
        }

        // Update text.
        winnerText.text = "";
        dealerHandScore.text = "";
        playerHandScore.text = player.BlackjackSumValue().ToString();
        nextRoundButton.interactable = false;

        if(dealer.BlackjackSumValue() == 21)
        {
            winnerText.text = "Dealer has a blackjack!";
            Stand();
        }

        if(player.BlackjackSumValue() == 21)
        {
            winnerText.text = "Player has a blackjack!";
            Stand();
        }
    }

    void DealerHit ()
    {
        int card = deck.Draw(0);
        dealer.push(card);
        FindObjectOfType<AudioManager>().Play("cardSlide6");
    }

    IEnumerator DealersTurn()
    {
        // Uncover Dealer's hand and reveal current score.
        yield return new WaitForSeconds(1f);
        CoverHand();
        dealerHandScore.text = dealer.BlackjackSumValue().ToString();
        yield return new WaitForSeconds(1f);

        // If the player has not yet bust, then the dealer draws until his hand is higher than 17
        bool playerBlackjack = (player.BlackjackSumValue() == 21 && player.CardCount == 2);

        if (!playerBlackjack)
        {
            while (dealer.BlackjackSumValue() < 17 && player.BlackjackSumValue() <= 21)
            {
                FindObjectOfType<AudioManager>().Play("cardSlide6");
                DealerHit();
                dealerHandScore.text = dealer.BlackjackSumValue().ToString();
                if(dealer.BlackjackSumValue() > 21)
                {
                    winnerText.text = "The dealer is bust!";
                }
                yield return new WaitForSeconds(1f);
                winnerText.text = "";
            }
        }
        

        // If you get a 21, game should be over TODO:

        // If the player is bust, or the dealer has a better score, than the player loses.
        if(player.BlackjackSumValue() > 21 || (dealer.BlackjackSumValue() > player.BlackjackSumValue() && dealer.BlackjackSumValue() <= 21))
        {
            winnerText.text = "You lose.";
            roundWonByDealer++;
            dealerScore.text = roundWonByDealer.ToString();
        } 
        // If the dealer is bust or the player has a better score than the dealer, than the player wins.
        else if (dealer.BlackjackSumValue() > 21 || (player.BlackjackSumValue() <= 21 && player.BlackjackSumValue() > dealer.BlackjackSumValue()))
        {
            winnerText.text = "You win!";
            roundWonByPlayer++;
            playerScore.text = roundWonByPlayer.ToString();
        } 
        // Else it is a draw, and "the house wins".
        else
        {
            winnerText.text = "Draw";
        }

        // If there is less than 10 cards left in the deck, the game is over.
        if(deck.CardCount <= 10) 
        {
            yield return new WaitForSeconds(1f);
            winnerText.text = "Game over!";
            nextRoundButton.interactable = false;
        }
        else 
        {
            nextRoundButton.interactable = true;
        }
    }
}
