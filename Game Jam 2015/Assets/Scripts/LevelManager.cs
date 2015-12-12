using UnityEngine;
using System.Collections;
using System;

public class LevelManager : MonoBehaviour {

    int currentLevel;
    int levelsUnlocked;
    menuStatus status;

    public menuStatus Status
    {
        get
        {
            return status;
        }

        set
        {
            status = value;
        }
    }

    // Use this for initialization
    void Start () {
        currentLevel = -1;              //-1 indicates menu
        levelsUnlocked = 5;             //Index of highest level available
        Status = menuStatus.mainMenu;
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
            Status = menuStatus.mainMenu;
        }
        else
        {
            Status = menuStatus.ingame;
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
        if (Status == menuStatus.mainMenu)
        {
            if (GUILayout.Button("Play game"))
            {
                Status = menuStatus.ingame;
                currentLevel = levelsUnlocked;
            }
            else if (GUILayout.Button("Level select"))
            {
                Status = menuStatus.levelSelect;
            }
            else if (GUILayout.Button("Pickups obtained"))
            {
                Status = menuStatus.pickupDisplay;
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
        else if (Status == menuStatus.pickupDisplay)
        {
            if (GUILayout.Button("Return"))
            {
                Status = menuStatus.mainMenu;
            }
        }
        else if (Status == menuStatus.levelSelect)
        {
            for(int i = 0; i<=levelsUnlocked; i++)
            {
                if (GUILayout.Button((i + 1).ToString()))
                {
                    loadLevel(i);
                }
            }
        }
    }
    

}


public enum menuStatus
{
    ingame,
    mainMenu,
    pickupDisplay,
    levelSelect,
    //achievementsDisplay,
}

