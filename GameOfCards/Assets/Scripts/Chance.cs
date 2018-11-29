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

    int roundWonByPlayer = 0;
    int roundWonByDealer = 0;


    /*
     * 3 Cards dealt to each player
     * The score of your hand is calculated by adding up all three cards and taking the value of the digit in the ones place.
     * You have the option to make one random "swap" with your opponent. You give up a random card from your hand 
     * in exchange for a random one of your opponent's.
     * At the end of the turn, the hand with the higher score wins the round.
     * 3 face cards (Jack/Jack/Jack) or Queen/Jack/King have the highest value
     * You continue playing until you reach the end of the deck. The player who won the most amount of rounds wins the game!
     */


    #region Public Methods

    // TODO: Fix bug where face down cards are offset.

    // TODO: Reveal dealer hand at end of the round
    // Solution should be in blackjack tutorial.

    // TODO: Fix bug where swapping more than 2 times no longer is shown visually.
    // Current fix: only allow 1 swap total (by the player)
    // Cards are being represented correctly in the cardStack object, but not graphically being updated.
    // Might have something to do with CardView (ShowCards() / AddCard())...

    // TODO: Once swapping bug is fixed, implement Dealer Swap AI (if < 5 then swap)

    // TODO: Do we want hand scores to be visible throughout round?

    // TODO: Convert "Back" button from onGUI() to a public method like the rest of our buttons? Done

    // Function for player to swap a random card with dealer.
    public void SwapCard()
    {
        // Pick random cards from both hands to be swapped.
        int randomPlayer = Random.Range(0, 3);
        int randomDealer = Random.Range(0, 3);
        Debug.Log("randomP number = " + randomPlayer);
        Debug.Log("randomD number = " + randomDealer);

        // Swap cards.
        if (dealer.ChanceHandValue() <= 5)
        {
            int tempCard = player.cards[randomPlayer];
            player.cards[randomPlayer] = dealer.cards[randomDealer];
            dealer.cards[randomDealer] = tempCard;
        }
        else
        {
            winnerText.text = "Dealer refuses to swap with you";
        }

        // Update hand score.
        playerHandScore.text = player.ChanceHandValue().ToString();
        //dealerHandScore.text = dealer.ChanceHandValue().ToString();
        dealerHandScore.text = "";
        FindObjectOfType<AudioManager>().Play("cardSlide6");
        /*
        // Debugging
        if (player.cards.Count == 3 && dealer.cards.Count == 3)
        { 
            Debug.Log("player hand =");
            for (int i = 0; i < 3; i++)
            {
                Debug.Log(player.cards[i]);
            }

            Debug.Log("dealer hand =");
            for (int i = 0; i < 3; i++)
            {
                Debug.Log(dealer.cards[i]);
            }
        }*/

        // Grey out Swap Card button
        swapCardButton.interactable = false;
    }

    // Function to end your turn for the round.
    public void EndTurn()
    {
       
        // Grey out buttons.
        endTurnButton.interactable = false;
        swapCardButton.interactable = false;
        // Show the player and dealer hands
        playerHandScore.text = player.ChanceHandValue().ToString();
        dealerHandScore.text = dealer.ChanceHandValue().ToString();
        
        // Compare hand values, update score/text.
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

    #endregion

    // Function to shuffle the deck and restart the game.
    // Play the cardShuffle music
    public void PlayAgain()
    {
        deck.Shuffle();
        winnerText.text = "";
        // Without this line the score won't reset
        roundWonByPlayer = roundWonByDealer = 0;
        Start();
        FindObjectOfType<AudioManager>().Play("cardShuffle");
    }

    // Function to move on to the next round of the game.
    public void NextRound()
    {
        nextRoundButton.interactable = false;
        winnerText.text = "";
        StartGame();
        FindObjectOfType<AudioManager>().Play("cardFan1");
    }

    // Starts a new game of Chance.
    void Start()
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

        // Empty the hands.
        if (player.HasCards && dealer.HasCards)
        {
            for (int i = 0; i < 3; i++)
            {
                player.Draw();
                dealer.Draw();
            }
        }
        // If the deck has less than 4 cards, then we have reached 
        // the end of the game, so we dont draw.
        if (deck.CardCount > 4)
        {

            // Draw hands.
            for (int i = 0; i < 3; i++)
            {
                player.push(deck.Draw());
                dealer.push(deck.Draw());
            }
            // Update hand scores.
            playerHandScore.text = player.ChanceHandValue().ToString();
            dealerHandScore.text = "";
        }
        else
        {
            // End game. 
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
