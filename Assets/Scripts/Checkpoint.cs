using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool Activated = false;
    public Material Off;
    public Material On;
    List<MeshRenderer> meshRenderers;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (Checkpoint checkpoint in FindObjectsOfType<Checkpoint>())
            {
                checkpoint.Activated = false;                
            }

            Activated = true;
            Player player = other.GetComponent<Player>();
            player.Checkpoint = transform.position;
        }
    }

    void Start()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>().ToList();
    }

    void Update()
    {
        if (Activated)
        {
            foreach (MeshRenderer meshRenderer in meshRenderers)
            {
                meshRenderer.material = On;
            }
        }
        else
        {
            foreach (MeshRenderer meshRenderer in meshRenderers)
            {
                meshRenderer.material = Off;
            }
        }
    }
}
