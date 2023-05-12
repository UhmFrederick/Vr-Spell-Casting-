using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHand : MonoBehaviour
{
    public InputActionProperty pinchAction;
    public InputActionProperty gripAction;
    public Animator Hand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pinchAction.action.ReadValue<float>();
        Hand.SetFloat("Trigger",triggerValue);
        float gripValue = gripAction.action.ReadValue<float>();
        Hand.SetFloat("Grip",gripValue);

        Debug.Log(triggerValue);

    }
}
