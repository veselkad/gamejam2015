using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TextStats : MonoBehaviour {

    public Text txt;
    private MovesManager mm;
    private LevelManager lm;

	// Use this for initialization
	void Start () {
        txt = GetComponent<Text>();
        //Debug.Log(txt);
        txt.text = "";
        mm = FindObjectOfType<MovesManager>();
        //lm = FindObjectOfType<LevelManager>();
        //Debug.Log(mm);
	}
	
	// Update is called once per frame
	void Update () {
        if (LevelManager.Status==menuStatus.ingame)
        {
            txt.text = "rotations left: " + mm.NumberOfRotateMoves + "\ntranslations left: " + mm.NumberOfTranslateMoves;
        }
        else
        {
            txt.text = "";
        }
        
	}
}
