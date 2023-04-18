using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public string activeElement;
    public string spell;
    public float scoreThreshold;
    private Vector3 spellSpawnPoint;

    //recognizers
    public GameObject spellRecognizer;
    public GameObject elementRecognizer;
    //List of Spells(to instanciate)
    public GameObject fireBall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //sets the active element (yes ik i wrote it wrong)
    public void GetActiveElement(string output, float score)
    {
        //check if the algorithm is sure its the right symbol
        if (score >= scoreThreshold)
        {
            activeElement = output;
            Debug.Log(score);
            //eneble and disable recognisers as needed
            elementRecognizer.SetActive(false);
            spellRecognizer.SetActive(true);
        }
        else
        {
            activeElement = "no";
            //element failure
        }
    }
    public void GetSpell(string output, float score, Vector3 SpellPosition)
    {
        //check if the algorithm is sure its the right symbol
        //also check if the spell is of the right element
        if (score >= scoreThreshold && output.Contains(activeElement))
        {
            spell = output;
            spellSpawnPoint = SpellPosition;
            Invoke(output, 0f);
            //eneble and disable recognisers as needed
            spellRecognizer.SetActive(false);
            elementRecognizer.SetActive(true);
            activeElement = "";
            spell = "";
             
        }
        else
        {
            spell = "no";
            //element failure
        }
    }
    public void fireball()
    {
        Instantiate(fireBall, spellSpawnPoint, Quaternion.identity);
    }
}
