using UnityEngine;
using System.Collections;

public class rotateArrows : MonoBehaviour {

    public float rotationSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(transform.parent.position, new Vector3(0, 1, 0), rotationSpeed * Time.deltaTime);

    }
}
