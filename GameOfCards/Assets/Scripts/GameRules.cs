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
            if (Panel.activeSelf) { Panel.SetActive(false); } //If the panel is being displayed, close it. 
            else { Panel.SetActive(true); } //If the button has not been pressed, show panel.
        }

    }
}
