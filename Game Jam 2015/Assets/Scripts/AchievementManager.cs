using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour {

    static Achievement mostRecentAch;
    static AchievementManager achMan;

    private static Texture2D boxTexture;
    private static GUIStyle boxStyle;

	// Use this for initialization
	void Start () {
        LevelManager.achievements = new List<Achievement>();
        LevelManager.achievements.Add(new Achievement("First steps", "Complete the first level", "level1"));
        LevelManager.achievements.Add(new Achievement("Master", "Complete the fifth level", "level5"));
        LevelManager.achievements.Add(new Achievement("Quinoa", "Hidden achievement!", "quinoa"));
        LevelManager.achievements.Add(new Achievement("360 no-scope", "Hidden achievement!", "360_noscope"));
        achMan = this;
        boxTexture = new Texture2D(1, 1);
        boxTexture.SetPixel(0, 0, new Color(84, 84, 84));
        boxStyle = new GUIStyle();
        boxStyle.normal.background = boxTexture;
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public static void Trigger(string achName)
    {
        foreach(Achievement ach in LevelManager.achievements)
        {
            if (ach.name== achName)
            {
                if (!ach.Obtained)
                {
                    mostRecentAch = ach;
                    achMan.StartCoroutine(achMan.triggerTimer());
                    ach.Trigger();
                }
                break;
            }
        }
    }

    void OnGUI()
    {
        if (mostRecentAch!= null)
        {
            GUI.Box(new Rect(Screen.width - 170, 0, 170, 70),GUIContent.none, boxStyle);
            GUI.DrawTexture(new Rect(Screen.width-160, 10, 50, 50), mostRecentAch.obtainedTexture);
            GUI.Label(new Rect(Screen.width-100, 20, 100, 40), "Achievement GET!");
        }
    }

    IEnumerator triggerTimer()
    {
        yield return new WaitForSeconds(3);
        mostRecentAch = null;
    }

}
