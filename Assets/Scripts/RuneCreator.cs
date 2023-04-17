using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using PDollarGestureRecognizer;
using System.IO;

public class RuneCreator : MonoBehaviour
{
    public string Spell; // Name of the new Spell ore element
    public string Element;
    public bool isSpell;


    private List<Vector3> positionList = new List<Vector3>();// list of positions for the path
    public XRNode inputSource; //the hand we want to use for the drawing
    public Transform movementScource;//the hand we want to use for the drawing
    public InputHelpers.Button inputButton; //button you need to press
    public float newPosThresHoldDistance = 0.05f; //the distance between the points in the scene
    private bool isMoving;
    private void Update()
    {
        //set bool based on if we are pressing selected button
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), inputButton, out bool isPressed, 0.1f);
        //Start Move
        if (!isMoving && isPressed)
        {
            StartMovement();
        }
        //End Move
        else if (isMoving && !isPressed)
        {
            if (isSpell)
            {
                CreateSpell();
            }
            else
            {
                CreateElement();
            }
        }
        //Updating move
        else if (isMoving && isPressed)
        {
            UpdateMovement();
        }
    }
    void StartMovement()
    {
        Debug.Log("start");
        isMoving = true;
        positionList.Clear();
        positionList.Add(movementScource.position);
    }

    void UpdateMovement()
    {

        Vector3 lastPosition = positionList[positionList.Count - 1];
        if (Vector3.Distance(movementScource.position, lastPosition) > newPosThresHoldDistance)
        {
            positionList.Add(movementScource.position);
        }

    }
    public void CreateElement()
    {
        isMoving = false;
        Point[] pointArray = new Point[positionList.Count]; //list of the points taken when making rune
        for (int i = 0; i < positionList.Count; i++)
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(positionList[i]); //project change 3d points to 2d by projecting them onto the camera
            pointArray[i] = new Point(screenPoint.x, screenPoint.y, 0);
        }
        Gesture newGesture = new Gesture(pointArray);
        //add gesture to training set
        newGesture.Name = Element;
        //save file of gesture
        string fileName = Application.persistentDataPath + "/" + "(Element)" + Element + ".xml";
        GestureIO.WriteGesture(pointArray, Element, fileName);
        Debug.Log("New Element Added");
    }
    public void CreateSpell()
    {
        isMoving = false;
        Point[] pointArray = new Point[positionList.Count]; //list of the points taken when making rune
        for (int i = 0; i < positionList.Count; i++)
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(positionList[i]); //project change 3d points to 2d by projecting them onto the camera
            pointArray[i] = new Point(screenPoint.x, screenPoint.y, 0);
        }
        Gesture newGesture = new Gesture(pointArray);
        //add gesture to training set
        newGesture.Name = Spell;
        //save file of gesture
        string fileName = Application.persistentDataPath + "/" + "(Spell)" + "(" + Element + ")" + Spell + ".xml";
        GestureIO.WriteGesture(pointArray, Spell, fileName);
        Debug.Log("New Spell Added");
    }
}
