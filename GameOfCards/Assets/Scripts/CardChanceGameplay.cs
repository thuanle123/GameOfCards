using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardChanceGamePlay : MonoBehaviour
{
    int dealersFirstCard = -1;

    public CardStack dealer;
    public CardStack player;
    public CardStack deck;

    public Button continueButton;
    public Button revealButton;
    public Button playAgainButton;

    public Text winnerText;

    /*
     * Cards dealt to each player
     * First player hits/sticks/bust
     * Dealer's turn; must have minimum of 17 score hand
     * Dealers cards; first card is hidden, subsequent cards are facing
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

        //player.GetComponent<CardView>().Clear();
        //dealer.GetComponent<CardView>().Clear();
        //deck.GetComponent<CardView>().Clear();
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
            //HitDealer();
        }
    }

    /*
    void HitDealer()
    {
        int card = deck.Draw();

        if (dealersFirstCard < 0)
        {
            dealersFirstCard = card;
        }

        dealer.push(card);
        if (dealer.CardCount >= 2)
        {
            CardView view = dealer.GetComponent<CardView>();
        }
    }

    IEnumerator DealersTurn()
    {
        continueButton.interactable = false;
        revealButton.interactable = false;

        CardView view = dealer.GetComponent<CardView>();
        //view.Toggle(dealersFirstCard, true);
        view.ShowCards();
        yield return new WaitForSeconds(1f);


        if (dealer.ChanceHandValue() >= player.ChanceHandValue())
        {
            winnerText.text = "Sorry-- you lose";
        }
        else
        {
            winnerText.text = "You win";
        }

        yield return new WaitForSeconds(1f);
        playAgainButton.interactable = true;
    }*/
}
