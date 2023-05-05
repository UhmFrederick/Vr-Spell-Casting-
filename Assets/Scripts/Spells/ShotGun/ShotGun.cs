using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : MonoBehaviour
{
    //Pellet Gameobject
    public GameObject Pellet;
    //the Number of Pellets Spawned
    public int PelletNumber;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < PelletNumber; i++)
        {
            Instantiate(Pellet, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
