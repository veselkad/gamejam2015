using UnityEngine;
using System.Collections;

public class PickUpManager : MonoBehaviour
{
    private GameObject pickUp;
    private LevelManager levelManager;
    private basicmovement bm;
    public float rotationSpeed = 60;
    public float translationSpeed = 0;

    private bool audioFlag;

    private AudioSource audio1;
    public AudioClip endsound;

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
        audio1 = GetComponent<AudioSource>();
        pickUp = GameObject.FindGameObjectWithTag("pickUp");
        bm = GameObject.FindObjectOfType<basicmovement>();
        //levelManager = GameObject.FindObjectOfType<LevelManager>();

      //  endsound = GetComponent<AudioClip>();
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
        if (!audioFlag)
        {
            audio1.PlayOneShot(endsound, 1);
            audioFlag = true;
        }
        rotationSpeed = 180;
        translationSpeed = 20;
        yield return new WaitForSeconds(0.9f);
        audioFlag = false;
        LevelManager.completeLevel();
    }
}