using UnityEngine;
using System.Collections;

public class PickUpManager : MonoBehaviour
{
    private GameObject pickUp;
    private LevelManager levelManager;
    private basicmovement bm;
    public float rotationSpeed = 60;
    public float translationSpeed = 0;

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
    void Start()
    {
        pickUp = GameObject.FindGameObjectWithTag("pickUp");
        bm = GameObject.FindObjectOfType<basicmovement>();
        //levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        basicmovement player = GameObject.FindObjectOfType<basicmovement>();
        pickUp.transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
        pickUp.transform.Translate(new Vector3(0, translationSpeed, 0) * Time.deltaTime);
        foreach(pickupInfo pui in LevelManager.obtainedPickups)
        {
            if (pui.name == "Identity")
            {
                pui.Obtained = true;
                break;
            }
            
        }
        if (Vector3.Distance(player.transform.position, pickUp.transform.position) < 2.0)
        {
            bm.complete();
            // LevelManager.completeLevel();
            StartCoroutine(FinishLevel());
        }
    }

    IEnumerator FinishLevel()
    {
        rotationSpeed = 180;
        translationSpeed = 20;
        yield return new WaitForSeconds(1);
        LevelManager.completeLevel();
    }
}