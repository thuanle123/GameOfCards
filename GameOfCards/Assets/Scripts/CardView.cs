using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// need Deck script and a private field Deck
[RequireComponent(typeof(Deck))]
public class CardView : MonoBehaviour
{
    Deck deck;
    public Vector3 start;
    public float cardOffset;
    public GameObject cardPrefab;
	// Use this for initialization
	void Start ()
    {
        // This grab a reference to the component of Deck
        deck = GetComponent<Deck>();
        ShowCards();
	}

    // Show the cards
    // Iterate through all the integer and create a new instance
    // of card prefab (Vector3) and add to the position
    void ShowCards()
    {
        int cardCount = 0;
        foreach(int i in deck.GetCards())
        {
            float co = cardOffset * cardCount;
            GameObject cardCopy = (GameObject)Instantiate(cardPrefab);

            //start position plus the "float co"
            Vector3 temp = start + new Vector3(co, 0f);
            cardCopy.transform.position = temp;

            CardModel cardModel = cardCopy.GetComponent<CardModel>();
            cardModel.cardIndex = i;
            cardModel.ToggleFace(true);
            cardCount++;
        }
    }
	
}
