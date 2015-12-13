using UnityEngine;
using System.Collections;

public class AchievementManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LevelManager.achievements.Add(new Achievement("360 no-scope", "Hidden achievement!", "360_noscope"));
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
