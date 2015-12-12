using UnityEngine;
using System.Collections;
using System;

public class LevelManager : MonoBehaviour {

    int currentLevel;
    int levelsUnlocked;
    menuStatus status;

	// Use this for initialization
	void Start () {
        currentLevel = -1;              //-1 indicates menu
        levelsUnlocked = 0;             //Index of highest level available
        status = menuStatus.mainMenu;
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    //private void displayMenu()
    //{

    //}

    public void loadLevel(int level)
    {
        if (level == -1)
        {
            status = menuStatus.mainMenu;
        }
        else
        {
            status = menuStatus.ingame;
        }
    }

    public void completeLevel()
    {
        if (currentLevel == levelsUnlocked)
        {
            levelsUnlocked++;
        }
    }

    void OnGUI()
    {
        if (status == menuStatus.mainMenu)
        {
            if (GUILayout.Button("Play game"))
            {
                status = menuStatus.ingame;
            }
            else if (GUILayout.Button("Pickups obtained"))
            {
                status = menuStatus.pickupDisplay;
            }
            else if (GUILayout.Button("More games"))
            {
                Application.OpenURL("https://drproject.twi.tudelft.nl/ewi3620tu1/Index.html");
            }
            else if (GUILayout.Button("Quit"))
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                Application.Quit();
            }
        }
        else if (status == menuStatus.pickupDisplay)
        {
            if (GUILayout.Button("Return"))
            {
                status = menuStatus.mainMenu;
            }
        }
    }

}


public enum menuStatus
{
    ingame,
    mainMenu,
    pickupDisplay,
    LevelSelect,
    //achievementsDisplay,
}