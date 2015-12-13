using UnityEngine;
using System.Collections;

public class deactivate : MonoBehaviour {

    private MeshRenderer ms;
	// Use this for initialization
	void Start () {
        ms = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        ms.enabled = basicmovement.mr.enabled;
	}
}
