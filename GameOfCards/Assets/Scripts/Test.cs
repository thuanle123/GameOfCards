using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    //Debug stuff
    CardFlip flip;
    CardModel cardModel;
    int cardIndex = 0;
    public GameObject card;

    void Awake()
    {
        cardModel = card.GetComponent<CardModel>();
        flip = card.GetComponent<CardFlip>();
    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(500, 200, 200, 20), "Draw!"))
        {
            //Reset the card index
            if (cardIndex >= cardModel.Faces.Length)
            {
                cardIndex = 0;
                // Grab some information from the card model
                flip.FlipCard(cardModel.Faces[cardModel.Faces.Length - 1], cardModel.cardBack, -1);
                //cardModel.ToggleFace(false);
            }
            else
            {
                if (cardIndex > 0)
                {
                    //previous card, take the current index and pass to the new card index
                    flip.FlipCard(cardModel.Faces[cardIndex - 1], cardModel.Faces[cardIndex], cardIndex);
                }
                else
                {
                    flip.FlipCard(cardModel.cardBack, cardModel.Faces[cardIndex], cardIndex);
                }
                cardIndex++;
            }
        }

        //Back Button
        if (GUI.Button(new Rect(500,10,200,20), "Back"))
        {
            SceneManager.LoadScene(1);
        }
    }
}
