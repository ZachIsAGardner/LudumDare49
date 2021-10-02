using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Erase : MonoBehaviour
{
    public Paper Paper;
    private Trigger trigger;

    void Start()
    {
        trigger = GetComponentInChildren<Trigger>();
        trigger.OnEnter = (Collider other) =>
        {
            Paper.Restart();
        };
    }
}
