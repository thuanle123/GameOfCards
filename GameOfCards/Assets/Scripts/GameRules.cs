using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Open panel upon button click.
 *Panel will show rules.
 * Click back button to go back to screen. 
 * OpenPanel() to open the panel
 * Back() exit the panel
 */

public class GameRules : MonoBehaviour {

    public GameObject Panel; //This is the panel that will be opened, can be used for any panel UI.

    public void OpenPanel() //Open panel function
    {
        if(Panel != null)//Checks to see if there is a panel. 
        {
            Panel.SetActive(true); //If panel is found, open the pannel. 
        }

    }

    public void Back() //Back function. 
    {
        Panel.SetActive(false); //When the back button is pressed, exits rule menu. 
    }
}
