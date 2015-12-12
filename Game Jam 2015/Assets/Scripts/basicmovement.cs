using UnityEngine;
using System.Collections;

public class basicmovement : MonoBehaviour {

    public string inputRotationLeft, inputRotationRight, inputTranslation;
    public float rotationSpeed, translationSpeed, rotationTime, rotationAngle, tempRotationAngle, translationDistance;
    private int rotationDirection;
    private bool rotationFlag, translationFlag, differenceFlag;

    private float currentRotation;
    private Vector3 currentPosition, previousPosition;

    private float debugRotation, differenceRotation, totalTranslation;
    private MovesManager mm;
    private LevelManager lm;

	// Use this for initialization
	void Start () {
        mm = GameObject.FindObjectOfType<MovesManager>();
        lm = GameObject.FindObjectOfType<LevelManager>();
        tempRotationAngle = rotationAngle;
        totalTranslation = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (lm.Status != menuStatus.ingame)
        {
            return;
        }
	    if(Input.GetKeyDown(inputRotationLeft)) //trigger rotate left
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


        if (rotationFlag) //rotate in the given direction until the desired angle is reached
        {
           transform.Rotate(new Vector3(0, rotationDirection * rotationSpeed * Time.deltaTime, 0));

            differenceRotation = transform.rotation.eulerAngles.y - debugRotation;

            if(Mathf.Abs(differenceRotation) > 42)
            {
                differenceFlag = true; //for accessing the restore rotationAngle condition below

                tempRotationAngle = rotationAngle; //store previous rotation value in a temporary variable

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
                rotationFlag = false;

                if (differenceFlag)
                {
                    rotationAngle = tempRotationAngle;
                    differenceFlag = false;
                }

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
                translationFlag = false;
                totalTranslation = 0;
            }
        }




    }

    //IEnumerator RotationTimer() uncomment this for time-based rotation
    //{
    //    rotationFlag = true; 
    //    yield return new WaitForSeconds(rotationTime);
    //    rotationFlag = false;
    //}
}
