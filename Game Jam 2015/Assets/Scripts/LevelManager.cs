﻿using UnityEngine;
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
    public static List<Achievement> achievements;
    public static bool paused;
    public static GUIStyle instructionStyle;
    
#if UNITY_EDITOR
    [MenuItem("Edit/Reset Playerprefs")]
    public static void DeletePlayerPrefs() { PlayerPrefs.DeleteAll(); }

    [MenuItem("Play/Play game")]
    public static void PlayGame()
    {
        EditorApplication.SaveScene(EditorApplication.currentScene);
        EditorApplication.OpenScene("Assets/_Scenes/init.unity");
        EditorApplication.isPlaying = true;
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
        obtainedPickups.Add(new pickupInfo("Identity", "Completes the level", "identity", "identity"));
        
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
        instructionStyle = new GUIStyle();
        instructionStyle.normal.textColor = Color.red;

        Status = menuStatus.mainMenu;
	}

    // Update is called once per frame
    void Update () {
    }


    public static void loadLevel(int level)
    {
        if (level == -1)
        {
            Status = menuStatus.mainMenu;
            Application.LoadLevel("mainMenu");
        }
        else
        {
            currentLevel = level;
            Status = menuStatus.ingame;
            Application.LoadLevel("level"+level);
        }
    }

    public static void completeLevel()
    {
        if (currentLevel == 0)
        {
            AchievementManager.Trigger("First steps");
        }
        else if(currentLevel == 4){
            AchievementManager.Trigger("Master");
        }
        if (currentLevel == levelsUnlocked)
        {
            LevelsUnlocked = LevelsUnlocked + 1;
        }
        loadLevel(-1);
        Status = menuStatus.levelSelect;
    }

    void OnGUI()
    {
        if (Status == menuStatus.mainMenu)
        {
            if (GUILayout.Button("Play game"))
            {
                loadLevel(Math.Min(levelsUnlocked,maxLevel));
            }
            else if (GUILayout.Button("Level select"))
            {
                Status = menuStatus.levelSelect;
            }
            else if (GUILayout.Button("Instructions"))
            {
                Status = menuStatus.instructions;
            }
            else if (GUILayout.Button("Pickups"))
            {
                Status = menuStatus.pickupDisplay;
            }
            else if (GUILayout.Button("Achievements"))
            {
                Status = menuStatus.achievementsDisplay;
            }
            else if (GUILayout.Button("More games"))
            {
                AchievementManager.Trigger("Quinoa");
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
        

        else if (Status == menuStatus.instructions)
        {
            if (GUILayout.Button("Return"))
            {
                Status = menuStatus.mainMenu;
            }
            string instr = @"The goal of each level is to reach the identity matrix.
This can be done by moving forward (w), and rotating (q/e).
You can interrupt a turn by turning in the other direction.
You can move and turn at the same time - but be careful!
The key to succes is to do so in the right order!

You only have a limited amount of moves, use them wisely.
In some levels, you may find additional moves.";
            GUI.Label(new Rect(30, 30, 400, 1000), instr, instructionStyle);
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


        else if (Status == menuStatus.achievementsDisplay)
        {
            if (GUILayout.Button("Return"))
            {
                Status = menuStatus.mainMenu;
            }
            int x, y;
            y = 25;
            foreach (Achievement ach in achievements)
            {
                x = 15;
                if (ach.Obtained)
                {
                    GUI.DrawTexture(new Rect(x, y, 50, 50), ach.obtainedTexture);
                }
                else
                {
                    GUI.DrawTexture(new Rect(x, y, 50, 50), Achievement.unknownTexture);
                }
                x = 75;
                GUI.Label(new Rect(x, y, 200, 50), ach.name + "\n" + ach.description);
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
                paused = false;
                Status = menuStatus.mainMenu;
                Application.LoadLevel("mainMenu");
            }
        }
    }
    

}

public enum menuStatus
{
    ingame,
    mainMenu,
    instructions,
    levelSelect,
    pickupDisplay,
    achievementsDisplay,
}

