using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDealer : MonoBehaviour
{
    public CardStack dealer;
    public CardStack player;

    int[] faceCard = new int[] { 0,9, 8};
    int count = 0;

    void OnGUI()
    {
        /*if (GUI.Button(new Rect(10, 10, 256, 28),"Draw"))
        {
            for (int i = 0; i < 3; i++)
            {
                player.push(dealer.Draw());
           }
        }*/
        if (GUI.Button(new Rect(10, 10, 256, 28),"Draw"))
        {
            player.push(faceCard[count++]);
        }
    }


}
