using UnityEngine;
using System.Collections;

public class PickUpManager : MonoBehaviour {

    private GameObject pickUp;

    public GameObject PickUp
    {
        get
        {
            return pickUp;
        }

        set
        {
            pickUp = value;
        }
    }

    // Use this for initialization
    void Start () {
        pickUp = GameObject.FindGameObjectWithTag("pickUp");
	}
	
	// Update is called once per frame
	void Update () {
        basicmovement player = GameObject.FindObjectOfType<basicmovement>();
        if (Vector3.Distance(player.transform.position, pickUp.transform.position) < 1)
        {
            Debug.Log("Picked up pickup!");
        }
        //pickUp.transform.
	}
}
