using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public Rigidbody rb;
    public float Speed;
    public float Spread;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        transform.eulerAngles += new Vector3(Random.Range(-Spread, Spread), Random.Range(-Spread, Spread), 0);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * Speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Pellet>())
        {
            Destroy(this.gameObject);
        }
    }
}
