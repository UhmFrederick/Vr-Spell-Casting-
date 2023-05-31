using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Rigidbody Rigidbody;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = this.GetComponent<Rigidbody>();
        Rigidbody.AddRelativeForce(Vector3.forward * speed);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
