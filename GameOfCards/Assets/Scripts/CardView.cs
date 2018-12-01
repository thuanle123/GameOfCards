﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// need Deck script and a private field Deck
[RequireComponent(typeof(CardStack))]
public class CardView : MonoBehaviour
{
    CardStack deck;
    Dictionary<int, GameObject> fetchedCards; // change from list to dictionary, key that unlocks
    public Vector3 start;
    public float cardOffset;
    public bool faceUp = false;
    public bool reverseLayer = false;
    public GameObject cardPrefab;

	void Start ()
    {
        // This grab a reference to the component of Deck
        fetchedCards = new Dictionary<int, GameObject>();
        deck = GetComponent<CardStack>();
        ShowCards();
        deck.CardRemoved += Deck_CardRemoved;
	}

    // This handler check to see if we have the card index
    // Does contain the key, destroy the object
    // Remove the card from the deck
    // Destroy take the game object and remove the scene
    private void Deck_CardRemoved(object sender, CardRemoved e)
    {
        
        if (fetchedCards.ContainsKey(e.CIndex))
        {
            Destroy(fetchedCards[e.CIndex]);
            fetchedCards.Remove(e.CIndex);
        }
    }

    void Update()
    {
        ShowCards();
    }

    // Show the cards
    // Iterate through all the integer and create a new instance
    // of card prefab (Vector3) and add to the position
    public void ShowCards()
    {
        int cardCount = 0;
        if (deck.HasCards)
        {
            foreach (int i in deck.GetCards())
            {
                float co = cardOffset * cardCount;
                
                //start position plus the "float co"
                // value we get back from get card is going to be the
                // index into the card deck and the position index is a position in the hand
                Vector3 temp = start + new Vector3(co, 0f);
                AddCard(temp, i, cardCount);
                cardCount++;
            }
        }
    }

    void AddCard(Vector3 position, int cardIndex, int positionalIndex)
    {
        // do nothing if already fetch the card
        // without this it won't add the card
        if (fetchedCards.ContainsKey(cardIndex))
        {
           return;
        }
        GameObject cardCopy = (GameObject)Instantiate(cardPrefab);
        cardCopy.transform.position = position;

        CardModel cardModel = cardCopy.GetComponent<CardModel>();
        cardModel.cardIndex = cardIndex;
        cardModel.ToggleFace(faceUp);

        SpriteRenderer spriteRenderer = cardCopy.GetComponent<SpriteRenderer>();

        spriteRenderer.sortingOrder = 51 - positionalIndex;
        fetchedCards.Add(cardIndex, cardCopy); // The line that actually displays the card
        //Debug.Log("Hand Value = " + deck.ChanceHandValue());
        Debug.Log("Test Value = " + deck.BlackjackSumValue());
    }

}
