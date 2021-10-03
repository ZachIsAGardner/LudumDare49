using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool Activated = false;
    public Material Off;
    public Material On;
    List<MeshRenderer> meshRenderers;
    ParticleSystem particleSystem;
    TextMeshPro text;

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

            particleSystem.enableEmission = true;
            text.gameObject.SetActive(true);
            Timer.Create(2f, () =>
            {
                text.gameObject.SetActive(false);
            });

            Sound.Play("Win", false, 0.5f, true);
        }
    }

    void Start()
    {
        text = GetComponentInChildren<TextMeshPro>();
        text.gameObject.SetActive(false);
        particleSystem = GetComponentInChildren<ParticleSystem>();
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
            text.gameObject.SetActive(false);
            particleSystem.enableEmission = false;
            
            foreach (MeshRenderer meshRenderer in meshRenderers)
            {
                meshRenderer.material = Off;
            }
        }
    }
}
