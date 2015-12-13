using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LevelManager.achievements = new List<Achievement>();
        LevelManager.achievements.Add(new Achievement("360 no-scope", "Hidden achievement!", "360_noscope"));
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
                ach.Trigger();
                break;
            }
        }
    }
}
