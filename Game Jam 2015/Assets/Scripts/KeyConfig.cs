using UnityEngine;
using System.Collections;

public class KeyConfig : MonoBehaviour {
    public string rotationLeft, rotationRight, translation;
    public static string inputRotationLeft, inputRotationRight, inputTranslation;

    // Use this for initialization
    void Start () {
        inputRotationLeft = rotationLeft;
        inputRotationRight = rotationRight;
        inputTranslation = translation;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
