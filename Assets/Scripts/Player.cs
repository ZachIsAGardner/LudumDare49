using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Power = 50;

    Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();    
    }

    void Update()
    {       
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddForce(new Vector3(0, Power * Time.deltaTime, 0));
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rigidbody.AddForce(new Vector3(0, 0, Power * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody.AddForce(new Vector3(-Power * Time.deltaTime, 0, 0));
        }

         if (Input.GetKey(KeyCode.DownArrow))
        {
            rigidbody.AddForce(new Vector3(0, 0, -Power * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody.AddForce(new Vector3(Power * Time.deltaTime, 0, 0));
        }
    }
}
