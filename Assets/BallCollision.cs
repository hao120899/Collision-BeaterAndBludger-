using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public float initSpeed;
    public float acceleration;

    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        float angle = Random.value * 2 * Mathf.PI;
        velocity = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
        GetComponent<Rigidbody>().position = Vector3.zero;
        GetComponent<Rigidbody>().velocity = velocity * initSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Beater")
        {
            initSpeed += acceleration;
            GetComponent<Rigidbody>().velocity = collision.collider.transform.parent.forward * initSpeed;
        }
    }
}
