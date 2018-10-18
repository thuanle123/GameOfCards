using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    // Play the game to go to the next scene
    // case 0 loads the PlayMenu scene
    // case 1 quits the game
    // case 2 loads credit menu
    // case 3 loads option menu
    public void MenuManager(int i)
    {
        switch (i)
        {
            default:
            case (0):
                SceneManager.LoadScene(1);
                break;
            case (1):
                Debug.Log("Application Quit");
                Application.Quit();
                break;
            case (2):
                SceneManager.LoadScene(2);
                break;
            case (3):
                SceneManager.LoadScene(3);
                break;
        }
    }
	
    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}
