using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public bool Exit = true;

    private Trigger trigger;

    void Start()
    {
        if (!Exit)
        {
            trigger = GetComponentInChildren<Trigger>();
            trigger.OnEnter = (Collider other) =>
            {
                if (other.tag == "Player")
                {
                    other.transform.position = FindObjectsOfType<Teleporter>().First(t => t.Exit).transform.position + (Vector3.up * 2);
                }
            };
        }
    }
}
