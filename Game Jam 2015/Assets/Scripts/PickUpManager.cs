using UnityEngine;
using System.Collections;

public class PickUpManager : MonoBehaviour {
    private GameObject pickUp;
    private LevelManager levelManager;

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
        levelManager = GameObject.FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
        basicmovement player = GameObject.FindObjectOfType<basicmovement>();
        if (Vector3.Distance(player.transform.position, pickUp.transform.position) < 1)
        {
            levelManager.completeLevel();
            Debug.Log("complete");
        }
        pickUp.transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
    }
}
