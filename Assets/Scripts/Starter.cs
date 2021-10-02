using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    public SceneTransition Transition;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _ = Game.LoadAsync("Test", Transition);
        }
    }
}
