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
    //public AudioManager soundClips;

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

    // update UI
    // update naming
    // move scores above cards to avoid overlap
    public void Start ()
    {
        //CoverHand();
        roundWonByPlayer = roundWonByDealer = 0;
        playerScore.text = roundWonByPlayer.ToString();
        dealerScore.text = roundWonByDealer.ToString();
        winnerText.text = "";
        gameOverText.text = "";
        StartGame();
        FindObjectOfType<AudioManager>().Play("cardShuffle");
        //soundClips.Play("cardShuffle");
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
        //soundClips.Play("cardSlide6");
        player.push(deck.Draw(0));
        Debug.Log("Player score = " + player.BlackjackSumValue());
        playerHandScore.text = player.BlackjackSumValue().ToString();
        if (player.BlackjackSumValue() > 21)
        {
            // Player is bust.
            hitButton.interactable = false;
            standButton.interactable = false;
            StartCoroutine(DealersTurn());
            //soundClips.Play("cardSlide6");
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
        hitButton.interactable = true;
        standButton.interactable = true;
        CoverHand();
        StartGame();
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

    void StartGame ()
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
        winnerText.text =  "";
        dealerHandScore.text = "";
        playerHandScore.text = player.BlackjackSumValue().ToString();
        nextRoundButton.interactable = false;
    }

    void DealerHit ()
    {
        int card = deck.Draw(0);
        dealer.push(card);
    }

    IEnumerator DealersTurn()
    {
        CoverHand();
        dealerHandScore.text = dealer.BlackjackSumValue().ToString();
        yield return new WaitForSeconds(1f);
        while (dealer.BlackjackSumValue() < 17 && player.BlackjackSumValue() <= 21)
        {
            FindObjectOfType<AudioManager>().Play("cardSlide6");
            DealerHit();
            dealerHandScore.text = dealer.BlackjackSumValue().ToString();
            yield return new WaitForSeconds(1f);
        }
        /* I don't know where to put this for now
        // If you’re dealt an ace and 10 as your first two cards, that’s blackjack. 
        //This is an automatic win for you unless the dealer gets the same. 
        //If this happens, it’s called a push and no one wins.
        // Basically, if this happen i want it to go to the next round
        if (player.BlackjackSumValue() == 21 && dealer.BlackjackSumValue() != 21)
        {
            playerHandScore.text = "Blackjack!!";
            roundWonByPlayer++;
            playerScore.text = roundWonByPlayer.ToString();
        }
        else if ((player.BlackjackSumValue() != 21 && dealer.BlackjackSumValue() == 21))
        {
            playerHandScore.text = "Blackjack!!";
            roundWonByDealer++;
            dealerScore.text = roundWonByDealer.ToString();
        }*/

        // Problem with this code for now
        // if you get a 21 and the dealer draws until he gets a 21
        // the round will count as draw, instead of you win
        if (player.BlackjackSumValue() > 21 || (dealer.BlackjackSumValue() > player.BlackjackSumValue() && dealer.BlackjackSumValue() <= 21))
        {
            winnerText.text = "You lose.";
            roundWonByDealer++;
            dealerScore.text = roundWonByDealer.ToString();
        }
        else if (dealer.BlackjackSumValue() > 21 || (player.BlackjackSumValue() <= 21 && player.BlackjackSumValue() > dealer.BlackjackSumValue()))
        {
            winnerText.text = "You win!";
            roundWonByPlayer++;
            playerScore.text = roundWonByPlayer.ToString();
        }
        else
        {
            winnerText.text = "Draw";
        }

        if(deck.CardCount <= 10) 
        {
            gameOverText.text = "Deck is running out of cards.\r\nGame over.";
            if (roundWonByDealer > roundWonByPlayer)
            {
                winnerText.text = "Dealer wins the game!";
            }
            else if (roundWonByPlayer > roundWonByDealer)
            {
                winnerText.text = "You win the game!";
            }
            else
            {
                winnerText.text = "Tie game!";
            }
            playAgainButton.interactable = false;
        }
        else 
        {
            playAgainButton.interactable = true;
        }
    }
}
