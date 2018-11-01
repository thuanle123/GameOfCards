using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardChanceGamePlay : MonoBehaviour
{
    int dealersFirstCard = -1;

    // These are game objects
    public CardStack dealer;
    public CardStack player;
    public CardStack deck;
    
    // Create these buttons in Chance scene
    public Button continueButton;
    public Button revealButton;
    public Button playAgainButton;

    public Text winnerText;

    /*
     * 3 Cards dealt to each player
     * First player reveal the card
     * Add up all three cards and take the mod 10
     * Compare the 2 cards
     * you win if you have higher hand
     * 3 face cards (Jack/Jack/Jack) or Queen/Jack/King have the highest value
     */

    public void Reveal()
    {
        continueButton.interactable = false;
        revealButton.interactable = false;
        //StartCoroutine(DealersTurn());
    }

    public void PlayAgain()
    {
        playAgainButton.interactable = false;
        
        // PROBLEM: THE CARD RESET INSTEAD OF RUNNING OUT OF THE STACK
        // Probably happen during shuffle function
        // no swap so it's okay(?)
        deck.Shuffle();
        winnerText.text = "";

        continueButton.interactable = true;
        revealButton.interactable = true;

        dealersFirstCard = -1;

        StartGame();
    }

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        for (int i = 0; i < 3; i++)
        {
            player.push(deck.Draw());
            dealer.push(Deck.Draw()); // may or may not work?
        }
    }

/*
    IEnumerator DealersTurn()
    {
        continueButton.interactable = false;
        revealButton.interactable = false;

        CardView view = dealer.GetComponent<CardView>();
        view.ShowCards();
        yield return new WaitForSeconds(1f);


        // Compare the two hands should be around here
        // CardStack.cs has a comment out function call Value(), it will take the mod 10 of ChanceHandValue()
        // cannot put it in here

        yield return new WaitForSeconds(1f);
        playAgainButton.interactable = true;
    }*/
}
