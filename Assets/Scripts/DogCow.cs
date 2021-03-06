using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogCow : MonoBehaviour
{
    private float timer;
    private Rigidbody rigidbody;

    void Start()
    {
        timer = Random.Range(1, 10);
        rigidbody = GetComponentInChildren<Rigidbody>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = Random.Range(1, 10);
            rigidbody.AddForce(new Vector3(0, Random.Range(50, 100), 0));
            Sound.Play("CowJump", false, 0.25f, false, 1, transform.position, Random.Range(0.5f, 1.5f));
        }
    }
}
