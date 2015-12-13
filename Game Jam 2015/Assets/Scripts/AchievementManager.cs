using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour {

    static Achievement mostRecentAch;
    static AchievementManager achMan;

	// Use this for initialization
	void Start () {
        LevelManager.achievements = new List<Achievement>();
        LevelManager.achievements.Add(new Achievement("First steps", "Complete the first level", "level1"));
        LevelManager.achievements.Add(new Achievement("Master", "Complete the fifth level", "level5"));
        LevelManager.achievements.Add(new Achievement("Quinoa", "Hidden achievement!", "quinoa"));
        LevelManager.achievements.Add(new Achievement("360 no-scope", "Hidden achievement!", "360_noscope"));
        achMan = this;
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
                mostRecentAch = ach;
                achMan.StartCoroutine(achMan.triggerTimer());
                ach.Trigger();
                break;
            }
        }
    }

    void OnGUI()
    {
        if (mostRecentAch!= null)
        {
            GUI.DrawTexture(new Rect(Screen.width-160, 0, 50, 50), mostRecentAch.obtainedTexture);
            GUI.Label(new Rect(Screen.width-100, 10, 100, 40), "Achievement GET!");
        }
    }

    IEnumerator triggerTimer()
    {
        Debug.Log("Yay!");
        yield return new WaitForSeconds(3);
        mostRecentAch = null;
        Debug.Log("Yay?");
        //yield break;
    }

}
