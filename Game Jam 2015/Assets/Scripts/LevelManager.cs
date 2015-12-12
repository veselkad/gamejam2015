using UnityEngine;
using System.Collections;
using System;

public class LevelManager : MonoBehaviour {

    static int currentLevel;
    static int levelsUnlocked;
    static menuStatus status;

    public static menuStatus Status
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

    public static int LevelsUnlocked
    {
        get
        {
            return levelsUnlocked;
        }

        set
        {
            levelsUnlocked = value;
            PlayerPrefs.SetInt("levelsUnlocked", value);
        }
    }

    // Use this for initialization
    void Start () {
        //Debug.Log("Instantiated level manager");
        DontDestroyOnLoad(this);
        currentLevel = -1;              //-1 indicates menu
        if (PlayerPrefs.HasKey("levelsUnlocked"))
        {
            levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked");
        }
        else
        {
            LevelsUnlocked = 0;
        }
        //Debug.Log("Started");
        Status = menuStatus.mainMenu;
	}

    // Update is called once per frame
    void Update () {
        //Debug.Log(status);
    }

    //private void displayMenu()
    //{

    //}

    public void loadLevel(int level)
    {
        if (level == -1)
        {
            //Debug.Log("level -1");
            Status = menuStatus.mainMenu;
        }
        else
        {
            Status = menuStatus.ingame;
            //Debug.Log("Status: " + status);
            Application.LoadLevel("level"+level);
        }
    }

    public static void completeLevel()
    {
        if (currentLevel == levelsUnlocked)
        {
            LevelsUnlocked++;
        }
    }

    void OnGUI()
    {
        if (Status == menuStatus.mainMenu)
        {
            //Debug.Log("wut");
            if (GUILayout.Button("Play game"))
            {
                loadLevel(levelsUnlocked);
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
                //Debug.Log("Returned");
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

