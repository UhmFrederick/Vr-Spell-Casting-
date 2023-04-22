using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using PDollarGestureRecognizer;
using System.IO;

public class ElementRecognizer: MonoBehaviour
{
    public InputHelpers.Button inputButton;
    public float inputThreshold = 0.1f;
    public XRNode inputSource; //the hand we want to use for the drawing
    public Transform movementScource;//the hand we want to use for the drawing

    public float newPosThresHoldDistance = 0.05f;

    private List<Gesture> trainingSet = new List<Gesture>();
    private bool isMoving = false;
    private List<Vector3> positionList = new List<Vector3>();

    //Spell and elemetn container
    public RuneContainer Spell;
    public RuneContainer Element;

    // Start is called before the first frame update
    void Start()
    {
        string[] gestureFiles = Directory.GetFiles(Application.persistentDataPath, "(Element)*.xml");//schould only read gesture that have the Element tag in front
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
        //set bool based on if we are pressing selected button
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), inputButton, out bool isPressed, inputThreshold);
        //Start Move
        if (!isMoving && isPressed)
        {
            StartMovement();
        }
        //End Move
        else if (isMoving && !isPressed)
        {
            EndMovement();
        }
        //Updating move
        else if (isMoving && isPressed)
        {
            UpdateMovement();
        }

        void StartMovement()
        {
            Debug.Log("start");
            isMoving = true;
            positionList.Clear();
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
            Result result = PointCloudRecognizer.Classify(newGesture,trainingSet.ToArray());
            Debug.Log(result.GestureClass + result.Score);

            //sends info to rune container

        }
        void UpdateMovement()
        {
            Vector3 lastPosition = positionList[positionList.Count - 1];
            if (Vector3.Distance(movementScource.position,lastPosition) > newPosThresHoldDistance)
            {
                positionList.Add(movementScource.position);
            }

        }
    }
}
