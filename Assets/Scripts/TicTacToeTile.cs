using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeTile : MonoBehaviour
{
    public int Y;
    public int X;
    public bool Ready { get; set; } = false;
    [HideInInspector] public Action Triggered;
    [HideInInspector] public GameObject Cross;
    [HideInInspector] public GameObject Circle;

    private Trigger trigger;

    void Start()
    {
        trigger = GetComponentInChildren<Trigger>();

        Cross = transform.Find("Cross").gameObject;
        Circle = transform.Find("Circle").gameObject;

        trigger.OnEnter = (Collider other) =>
        {

            if (other.tag == "Player")
            {
                SetCircle();
            }
        };
    }

    public void SetCircle()
    {
        if (Ready && !Cross.activeSelf && !Circle.activeSelf)
        {
            Circle.SetActive(true);
            if (Triggered != null) Triggered();
        }
    }

    public void SetCross()
    {
        if (Ready && !Cross.activeSelf && !Circle.activeSelf)
        {
            Cross.SetActive(true);
        }
    }

    public void Reset()
    {
        Cross.SetActive(false);
        Circle.SetActive(false);
    }
}
