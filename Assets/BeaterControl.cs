using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaterControl : MonoBehaviour
{
    public float acceleration;
    public float angularSpeed;
    public float maxSpeed;

    private float speed;
    private float direction;
    private bool flag;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.0f;
        direction = transform.parent.rotation.eulerAngles.y * Mathf.PI / 180;
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent.position += UpdateVelocity() * Time.deltaTime;
        transform.position = transform.parent.position + new Vector3(0, 0.03f, 0);
        transform.rotation = transform.parent.rotation * Quaternion.Euler(-89.98f, 0, 0);
    }

    Vector3 UpdateVelocity()
    {
        if (Input.GetKey(KeyCode.W)) speed += acceleration * Time.deltaTime;
        if (Input.GetKey(KeyCode.S)) speed -= acceleration * Time.deltaTime;
        if (Input.GetKey(KeyCode.A)) direction -= angularSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) direction += angularSpeed * Time.deltaTime;
        if (speed > maxSpeed) speed = maxSpeed;
        if (speed < 0.0f) speed = 0.0f;
        if (flag) speed = 0.0f;
        transform.parent.rotation = Quaternion.Euler(0, direction * 180 / Mathf.PI, 0);
        return new Vector3(Mathf.Sin(direction), 0, Mathf.Cos(direction)) * speed;
    }

    // Handle collisions
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Wall")
        {
            flag = true;
            Vector3 newPosition = transform.parent.position;
            if (collision.collider.name == "WallN") newPosition -= new Vector3(0, 0, 0.2f);
            if (collision.collider.name == "WallE") newPosition -= new Vector3(0.2f, 0, 0);
            if (collision.collider.name == "WallS") newPosition += new Vector3(0, 0, 0.2f);
            if (collision.collider.name == "WallW") newPosition += new Vector3(0.2f, 0, 0);
            transform.parent.position = newPosition;
            transform.position = newPosition + new Vector3(0, 0.03f, 0);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Wall") flag = false;
    }
}
