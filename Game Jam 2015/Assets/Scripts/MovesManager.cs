using UnityEngine;
using System.Collections;

public class MovesManager : MonoBehaviour {

    int numberOfTranslateMoves, numberOfRotateMoves;

    public int NumberOfRotateMoves
    {
        get
        {
            return numberOfRotateMoves;
        }

        set
        {
            numberOfRotateMoves = value;
        }
    }

    public int NumberOfTranslateMoves
    {
        get
        {
            return numberOfTranslateMoves;
        }

        set
        {
            numberOfTranslateMoves = value;
        }
    }

    // Use this for initialization
    void Start () {
        numberOfRotateMoves = 3;
        numberOfTranslateMoves = 3;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
