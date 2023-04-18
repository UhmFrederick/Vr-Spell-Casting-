using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using PDollarGestureRecognizer;
using System.IO;

public class SpellRecognizer : MonoBehaviour
{
    public InputHelpers.Button inputtrigger;
    public InputHelpers.Button inputgrip;
    public float inputThreshold = 0.1f;
    public XRNode inputSource; //the hand we want to use for the drawing
    public Transform movementScource;//the hand we want to use for the drawing

    public float newPosThresHoldDistance = 0.05f;

    public SpellManager spellManager;

    private List<Gesture> trainingSet = new List<Gesture>();
    private bool isMoving = false;
    private List<Vector3> positionList = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {

        string[] gestureFiles = Directory.GetFiles(Application.persistentDataPath, "(Spell)*.xml");//only read gestured that have the spell tag
        foreach (var item in gestureFiles)
        {
            trainingSet.Add(GestureIO.ReadGestureFromFile(item));
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (var i = 1; i < positionList.Count; i++)
        {
            Debug.DrawLine(positionList[i - 1], positionList[i], Color.red);
        }
        //set bool based on if we are pressing selected button(trigger)
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), inputgrip, out bool grip, inputThreshold);
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), inputtrigger, out bool trigger, inputThreshold);
        //Start Move
        if (!isMoving && grip)
        {
            StartMovement();
        }
        //End Move
        else if (isMoving && trigger)
        {
            EndMovement();
        }
        //Updating move
        else if (isMoving && grip)
        {
            UpdateMovement();
        }

        void StartMovement()
        {
            isMoving = true;
            positionList.Clear();
            //create first point
            positionList.Add(movementScource.position);
        }
        void EndMovement()
        {
            isMoving = false;
            //create the gesture from position list
            Point[] pointArray = new Point[positionList.Count];

            for (int i = 0; i < positionList.Count; i++)
            {
                Vector2 screenPoint = Camera.main.WorldToScreenPoint(positionList[i]); //project change 3d points to 2d by projecting them onto the camera
                pointArray[i] = new Point(screenPoint.x, screenPoint.y, 0);
            }
            Gesture newGesture = new Gesture(pointArray);

            //recognise gesture;
            Result result = PointCloudRecognizer.Classify(newGesture, trainingSet.ToArray());
            Debug.Log(result.GestureClass + result.Score);

            //gets the average position of all the points(where the spell should spawn)
            Vector3 spellSpawnPoint = Vector3.zero;
            for (int i = 0; i < positionList.Count; i++)
            {
                spellSpawnPoint += positionList[i];
            }
            spellSpawnPoint = spellSpawnPoint / positionList.Count;
            spellManager.GetSpell(result.GestureClass, result.Score, spellSpawnPoint);
        }
        void UpdateMovement()
        {
            Vector3 lastPosition = positionList[positionList.Count - 1];
            if (Vector3.Distance(movementScource.position, lastPosition) > newPosThresHoldDistance)
            {
                positionList.Add(movementScource.position);
            }

        }
    }
}