using UnityEngine;
using System.Collections;

public class basicmovement : MonoBehaviour {

    private string inputRotationLeft, inputRotationRight, inputTranslation;
    public float rotationSpeed, translationSpeed, rotationTime, rotationAngle, tempRotationAngle, translationDistance, reloadTime;
    private int rotationDirection;
    private bool rotationFlag, translationFlag, differenceFlag, reloadFlag;

    private float currentRotation;
    private Vector3 currentPosition, previousPosition;

    private float debugRotation, differenceRotation, totalTranslation;
    private MovesManager mm;
    private LevelManager lm;
    //private SetActive sa;

	// Use this for initialization
	void Start () {
        inputRotationLeft = KeyManager.inputRotationLeft;
        inputRotationRight = KeyManager.inputRotationRight;
        inputTranslation = KeyManager.inputTranslation;
        mm = GameObject.FindObjectOfType<MovesManager>();
        //sa = GameObject.FindObjectOfType<SetActive>();
        //lm = GameObject.FindObjectOfType<LevelManager>();
        tempRotationAngle = rotationAngle;
        totalTranslation = 0;
        reloadFlag = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (LevelManager.Status != menuStatus.ingame)
        {
            return;
        }
        if (!LevelManager.paused)
        {
            if (Input.GetKeyDown(inputRotationLeft)) //trigger rotate left
            {
                if (mm.NumberOfRotateMoves > 0)
                {
                    mm.NumberOfRotateMoves--;
                    rotationDirection = -1;
                    // StartCoroutine(RotationTimer()); uncomment this for time-based rotation
                    rotationFlag = true;
                    currentRotation = transform.rotation.eulerAngles.y;
                    //Debug.Log(rotationFlag + "rotationFlag");

                    debugRotation = transform.rotation.eulerAngles.y;
                }

            }

            if (Input.GetKeyDown(inputRotationRight)) //trigger rotate right
            {
                if (mm.NumberOfRotateMoves > 0)
                {
                    mm.NumberOfRotateMoves--;
                    rotationDirection = 1;
                    // StartCoroutine(RotationTimer()); uncomment this for time-based rotation
                    rotationFlag = true;
                    currentRotation = transform.rotation.eulerAngles.y;
                }

            }

            if (Input.GetKeyDown(inputTranslation))
            {
                if (mm.NumberOfTranslateMoves > 0)
                {
                    mm.NumberOfTranslateMoves--;
                    translationFlag = true;
                    currentPosition = transform.position;
                }

            }

            if (Input.GetKeyDown("r"))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
	    

        if (rotationFlag) //rotate in the given direction until the desired angle is reached
        {
           transform.Rotate(new Vector3(0, rotationDirection * rotationSpeed * Time.deltaTime, 0));

            differenceRotation = transform.rotation.eulerAngles.y - debugRotation;

            if(Mathf.Abs(differenceRotation) > 42)
            {
                differenceFlag = true; //for accessing the restore rotationAngle condition below

                //tempRotationAngle = rotationAngle; //store previous rotation value in a temporary variable

                if(differenceRotation < 0) //rotating right towards problem point
                {
                    rotationAngle += (currentRotation - 360);
                }
                if (differenceRotation > 0) //rotating left towards problem point
                {
                    rotationAngle -= currentRotation;
                }

                currentRotation = transform.rotation.eulerAngles.y;
            }

            debugRotation = transform.rotation.eulerAngles.y; //used to save the previous rotation


            if (Mathf.Abs(currentRotation - transform.rotation.eulerAngles.y) >= rotationAngle)
            {
                //rotationFlag = false;

                //if (differenceFlag)
                //{
                //    rotationAngle = tempRotationAngle;
                //    differenceFlag = false;
                //}
                ResetRotation();

                //Debug.Log("stop rotating");
            }
        }

        if (translationFlag) //translate in the given direction until the desired distance is reached
        {
            transform.Translate(new Vector3(translationSpeed * Time.deltaTime, 0, 0));
            //Debug.Log(totalTranslation);
            totalTranslation += Vector3.Distance(transform.position, currentPosition);
            currentPosition = transform.position;
            //Debug.Log("translating...");
            if (totalTranslation >= translationDistance)
            {
                //translationFlag = false;
                //totalTranslation = 0;
                ResetTranslation();
            }
        }
        Debug.Log(debugRotation);
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("collision");
        //ResetTranslation();
        //ResetRotation();

        if (reloadFlag && !col.gameObject.CompareTag("rotatepickup") && !col.gameObject.CompareTag("translatepickup"))
        {
            StartCoroutine(ReloadTime());
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("rotatepickup"))
        {
            foreach(pickupInfo pui in LevelManager.obtainedPickups)
            {
                if (pui.name == "Rotation")
                {
                    pui.Obtained = true;
                }
            }
            mm.NumberOfRotateMoves += 10;
            col.gameObject.SetActive(false);
        }

        if(col.gameObject.CompareTag("translatepickup"))
        {
            foreach (pickupInfo pui in LevelManager.obtainedPickups)
            {
                if (pui.name == "Translation")
                {
                    pui.Obtained = true;
                }
            }
            mm.numberOfTranslateMoves += 10;
            col.gameObject.SetActive(false);
        }
    }

    IEnumerator ReloadTime()
    {
        reloadFlag = false;
        yield return new WaitForSeconds(reloadTime);
        Application.LoadLevel(Application.loadedLevel);
        reloadFlag = true;
    }


    //IEnumerator RotationTimer() uncomment this for time-based rotation
    //{
    //    rotationFlag = true; 
    //    yield return new WaitForSeconds(rotationTime);
    //    rotationFlag = false;
    //}

    void ResetTranslation()
    {
        translationFlag = false;
        totalTranslation = 0;
    }
    
    void ResetRotation()
    {
        rotationFlag = false;

        //if (differenceFlag)
        {
            rotationAngle = tempRotationAngle;
            differenceFlag = false;
        }

    }
}
