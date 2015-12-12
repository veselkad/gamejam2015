using UnityEngine;
using System.Collections;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

    static int currentLevel;
    static int levelsUnlocked;
    static int maxLevel;
    static menuStatus status;
    public static List<pickupInfo> obtainedPickups;
    public static bool paused;

#if UNITY_EDITOR
    [MenuItem("Edit/Reset Playerprefs")]
    public static void DeletePlayerPrefs() { PlayerPrefs.DeleteAll(); }

    [MenuItem("Play/Play game")]
    public static void PlayGame()
    {
        EditorApplication.isPlaying = true;
        Application.UnloadLevel(Application.loadedLevel);
        Application.LoadLevel("init");
    }
#endif

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
        obtainedPickups = new List<pickupInfo>();
        obtainedPickups.Add(new pickupInfo("Translation", "Adds translation moves", "translation", "translation"));
        obtainedPickups.Add(new pickupInfo("Rotation", "Adds rotation moves", "rotation", "rotation"));
        currentLevel = -1;              //-1 indicates menu
        if (PlayerPrefs.HasKey("levelsUnlocked"))
        {
            levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked");
        }
        else
        {
            LevelsUnlocked = 0;
        }
        maxLevel = Application.levelCount - 3;              //Max index, -2 for init and menu, -1 for level number to level index
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

    public static void loadLevel(int level)
    {
        if (level == -1)
        {
            //Debug.Log("level -1");
            Status = menuStatus.mainMenu;
            Application.LoadLevel("mainMenu");
        }
        else
        {
            currentLevel = level;
            Status = menuStatus.ingame;
            //Debug.Log("Status: " + status);
            Application.LoadLevel("level"+level);
        }
    }

    public static void completeLevel()
    {
        if (currentLevel == levelsUnlocked)
        {
            LevelsUnlocked = LevelsUnlocked + 1;
        }
        loadLevel(-1);
        Status = menuStatus.levelSelect;
        //Debug.Log("Loaded level select? ");
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
            else if (GUILayout.Button("Pickups"))
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
            int x, y;
            y = 25;
            foreach (pickupInfo pui in obtainedPickups)
            {
                x = 15;
                GUI.DrawTexture(new Rect(x, y, 50, 50), pui.ingameTexture);
                x = 75;
                GUI.Label(new Rect(x, y, 200, 50), pui.name + "\n" + pui.description);
                x = 300;
                if (pui.Obtained)
                {
                    GUI.DrawTexture(new Rect(x, y, 50, 50), pui.mathematicalTexture);
                }
                else
                {
                    GUI.DrawTexture(new Rect(x, y, 50, 50), pickupInfo.unknownTexture);
                }
                y += 60;
            }
        }
        else if (Status == menuStatus.levelSelect)
        {
            for(int i = 0; i<=Math.Min(levelsUnlocked, maxLevel); i++)
            {
                if (GUILayout.Button((i + 1).ToString()))
                {
                    loadLevel(i);
                }
            }
            if (GUILayout.Button("Return"))
            {
                Status = menuStatus.mainMenu;
            }
        }
        else if (Status == menuStatus.ingame && paused)
        {
            if (GUILayout.Button("Return to game"))
            {
                paused = false;
            }
            else if (GUILayout.Button("Reset"))
            {
                Application.LoadLevel(Application.loadedLevel);
                paused = false;
            }
            else if (GUILayout.Button("Quit to main menu"))
            {
                Status = menuStatus.mainMenu;
            }
        }
    }
    

}

public enum menuStatus
{
    ingame,
    mainMenu,
    levelSelect,
    pickupDisplay,
    //achievementsDisplay,
}

