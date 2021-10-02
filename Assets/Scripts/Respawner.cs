using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    private Vector3 startPosition;
    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponentInChildren<Rigidbody>();
        startPosition = transform.position;
    }

    void Update()
    {
        if (transform.position.y < -5)
        {
            Restart();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lava")
        {
            Restart();
        }
    }

    void Restart()
    {
        transform.position = startPosition;
        rigidbody.velocity = Vector3.zero;
    }
}
