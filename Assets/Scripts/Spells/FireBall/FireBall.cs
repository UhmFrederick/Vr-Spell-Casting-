using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Rigidbody Rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = this.GetComponent<Rigidbody>();
        Rigidbody.AddRelativeForce(new Vector3());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
