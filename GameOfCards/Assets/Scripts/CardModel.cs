using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel : MonoBehaviour
{
    //reference the ocmponent
    SpriteRenderer spriteRenderer;
    public Sprite[] Faces;
    public Sprite cardBack;

    //Card Model uses card index to the array faces faces[cardIndex]
    public int cardIndex;

    public void ToggleFace(bool showFace)
    {
        if (showFace)
        {
            // Show the card Face
            spriteRenderer.sprite = Faces[cardIndex];
        }

        else
        {
            // Show the card back
            spriteRenderer.sprite = cardBack;
        }
    }
    // Grab the component that has been attach to this object
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
