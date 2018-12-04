using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Chance : MonoBehaviour
{
    
    // These are game objects
    public CardStack dealer;
    public CardStack player;
    public CardStack deck;

    public CardStack tempStack;

    // Create these buttons in Chance scene
    public Button playAgainButton;
    public Button endTurnButton;
    public Button swapCardButton;
    public Button nextRoundButton;

    public Text winnerText;
    public Text playerScore;
    public Text dealerScore;
    public Text playerHandScore;
    public Text dealerHandScore;

    public GameObject HandCover1;
    public GameObject HandCover2;
    public GameObject HandCover3;

    //public AudioManager soundClips;

    int roundWonByPlayer = 0;
    int roundWonByDealer = 0;

    // Make PNG to cover up hands.

    /*
     * 3 Cards dealt to each player
     * The score of your hand is calculated by adding up all three cards and taking the value of the digit in the ones place.
     * You have the option to make one random "swap" with your opponent. You give up a random card from your hand 
     * in exchange for a random one of your opponent's.
     * At the end of the turn, the hand with the higher score wins the round.
     * 3 face cards (Jack/Jack/Jack) or Queen/Jack/King have the highest value
     * You continue playing until you reach the end of the deck. The player who won the most amount of rounds wins the game!
     */

    // TODO: Once swapping bug is fixed, implement Dealer Swap AI (if < 5 then swap)
    // Make swap card button disabled after dealer refuses to swap.

    //Open panel function
    public void CoverHand()
    {
        //Checks to see if there is a HandCover1. 
        //If the HandCover1 is being displayed, close it. 
        //If the button has not been pressed, show HandCover1.
        if (HandCover1 != null)
        {
            if (HandCover1.activeSelf)
            {
                HandCover1.SetActive(false);
            }
            else
            {
                HandCover1.SetActive(true);
            }
        }

        //Checks to see if there is a HandCover2. 
        //If the HandCover2 is being displayed, close it. 
        //If the button has not been pressed, show HandCover2.
        if (HandCover2 != null) 
        {
            if (HandCover2.activeSelf)
            {
                HandCover2.SetActive(false);
            } 
            else
            {
                HandCover2.SetActive(true);
            }
        }

        // Checks to see if there is a HandCover3.
        // If the HandCover3 is being displayed, close it. 
        // If the button has not been pressed, show HandCover3.
        if (HandCover3 != null)
        {
            if (HandCover3.activeSelf)
            {
                HandCover3.SetActive(false);
            } 
            else
            {
                HandCover3.SetActive(true);
            }
        }
    }

    // Swap random card between opponent and player hand
    public void SwapCard()
    {
        swapCardButton.interactable = false;
        // Pick random cards from both hands to be swapped.
        int randomPlayer = Random.Range(0, 3);
        int randomDealer = Random.Range(0, 3);

        int tempCard = player.Draw(randomPlayer);
        player.InsertCard(randomPlayer, dealer.Draw(randomDealer));
        dealer.InsertCard(randomDealer, tempCard);
        // Update hand score.
        if (player.ChanceHandValue() == 30)
        {
            playerHandScore.text = "CHANCE";             
        }
        else 
        {
            playerHandScore.text = player.ChanceHandValue().ToString();
        }

        dealerHandScore.text = "";
        FindObjectOfType<AudioManager>().Play("cardSlide6");
    }

    // Function to end your turn for the round.
    public void EndTurn()
    {
        // Grey out buttons.
        endTurnButton.interactable = false;
        swapCardButton.interactable = false;
        // Show the player and dealer hands
        if (player.ChanceHandValue() == 30)             
        {                 
            playerHandScore.text = "CHANCE";             
        }
        else
        {
            playerHandScore.text = player.ChanceHandValue().ToString();
        }
        if (dealer.ChanceHandValue() == 30)             
        {
            dealerHandScore.text = "CHANCE";             
        }
        else
        {
            dealerHandScore.text = dealer.ChanceHandValue().ToString();
        }      
        
        // Compare hand values, update score/text.
        CoverHand();
        if (dealer.ChanceHandValue() > player.ChanceHandValue())
        {
            winnerText.text = "You lose the round.";
            roundWonByDealer++;
            dealerScore.text = roundWonByDealer.ToString();
        }
        else if (player.ChanceHandValue() > dealer.ChanceHandValue())
        {
            winnerText.text = "You win the round.";
            roundWonByPlayer++;
            playerScore.text = roundWonByPlayer.ToString();
        }
        else
        {
            winnerText.text = "Round Draw";
        }

        // Reactivate Next Round button.
        nextRoundButton.interactable = true;
    }

    // Function to shuffle the deck and restart the game.
    // Play the cardShuffle music
    public void PlayAgain()
    {
        deck.Shuffle();
        winnerText.text = "";
        // Without this line the score won't reset
        roundWonByPlayer = roundWonByDealer = 0;
        HandCover1.SetActive(true);
        HandCover2.SetActive(true);
        HandCover3.SetActive(true);
        Start();
        FindObjectOfType<AudioManager>().Play("cardShuffle");
    }

    // Function to move on to the next round of the game.
    public void NextRound()
    {
        nextRoundButton.interactable = false;
        winnerText.text = "";
        CoverHand();
        StartGame();
        FindObjectOfType<AudioManager>().Play("cardFan1");
    }

    // Starts a new game of Chance.
    public void Start()
    {
        playerScore.text = "0";
        dealerScore.text = "0";
        StartGame();
        FindObjectOfType<AudioManager>().Play("cardSlide6");
    }

    // Should change name to StartRound()?
    void StartGame()
    {
        endTurnButton.interactable = true;
        swapCardButton.interactable = true;
        nextRoundButton.interactable = false;

        // Emptying hands
        while(player.HasCards && dealer.HasCards)
        {
            player.Draw(0);
            dealer.Draw(0);
        }

        // If the deck has less than or equal to 6 cards, then we have reached 
        // the end of the game, so we dont draw.
        if (deck.CardCount >= 6)
        {
            // Draw hands.
            for (int i = 0; i < 3; i++)
            {
                player.push(deck.Draw(0));
                dealer.push(deck.Draw(0));
            }
            // Update hand scores.
            if (player.ChanceHandValue() == 30)
            {
                playerHandScore.text = "CHANCE";
            }
            else
            {
                playerHandScore.text = player.ChanceHandValue().ToString();
            }
            dealerHandScore.text = "";
        }
        else
        {
            // End game. 
            CoverHand();
            playerHandScore.text = "";
            dealerHandScore.text = "";

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

            // Grey out buttons.
            swapCardButton.interactable = false;
            nextRoundButton.interactable = false;
            endTurnButton.interactable = false;
        }
    }
}
