using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CowTrigger : MonoBehaviour
{
    public TalkTrigger talkTrigger;
    public TextMeshProUGUI Text;
    public int Goal = 5;
    private int count; 

    private void Start()
    {
        // max = GameObject.FindGameObjectsWithTag("DogCow").Count();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DogCow")
        {
            count++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "DogCow")
        {
            count--;
        }
    }

    private void Update()
    {
        Text.text = $"Cowdogs herded: {count}/{Goal}";
        if (count >= Goal)
        {
            talkTrigger.Key = "CowComplete";
        }

        if (Game.GotCowPart)
        {
            talkTrigger.Key = "CowExtra";
        }
    }
}
