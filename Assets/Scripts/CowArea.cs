using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowArea : MonoBehaviour
{
    public UiElement Element;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("fade in");
            Element.FadeIn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            print("fade out");
            Element.FadeOut();
        }
    }
}
