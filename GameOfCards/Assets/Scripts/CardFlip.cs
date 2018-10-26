using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFlip : MonoBehaviour
{
    //Component that get added to the card
    // access to render and card model
    // have a method flipcard
    // stop and start the flip
    // Flip is the Ienumerator set the sprite render to the be first image
    // use animation curve to control the scale of the card
    SpriteRenderer spriteRenderer;
    CardModel model;

    // Going from scale 1 to 0, to one again
    // 
    public AnimationCurve scaleCurve;
    public float duration = 0.5f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        model = GetComponent<CardModel>();
    }
    // this flip the card
    public void FlipCard(Sprite startImage, Sprite endImage, int cardIndex)
    {
        StopCoroutine(Flip(startImage, endImage, cardIndex));
        StartCoroutine(Flip(startImage, endImage, cardIndex));
    }
    IEnumerator Flip(Sprite startImage, Sprite endImage, int cardIndex)
    {
        spriteRenderer.sprite = startImage;

        float time = 0f;
        while (time <= 1f)
        {
            float scale = scaleCurve.Evaluate(time);
            time = time + Time.deltaTime / duration;

            Vector3 localScale = transform.localScale;
            localScale.x = scale;
            transform.localScale = localScale;

            if (time >= 0.5f)
            {
                spriteRenderer.sprite = endImage;
            }
            yield return new WaitForFixedUpdate();
        }
        if (cardIndex == -1)
        {
            model.ToggleFace(false);
        }
        else
        {
            //show the face of the card
            model.cardIndex = cardIndex;
            model.ToggleFace(true);
        }
    }
}
