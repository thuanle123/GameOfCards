using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// === Testing file for Deck === //

public class DeckTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Deck d = new Deck();
        for(var i = 0; i < 52; ++i)
        {
            Card c = d.Draw();
            Debug.Log(c.Suit + " " + c.Rank);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
