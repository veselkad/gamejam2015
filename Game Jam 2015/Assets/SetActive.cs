using UnityEngine;
using System.Collections;

public class SetActive : MonoBehaviour {

	// Use this for initialization
	void Start() {
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Disable(GameObject gm)
    {
        gm.SetActive(false);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
