using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    void Start()
    {
        Song.Play("End");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           _ = Game.Quit();
        }
    }
}
