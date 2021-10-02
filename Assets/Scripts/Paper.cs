using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public GameObject Pixel;

    public List<GameObject> Pixels { get; private set; } = new List<GameObject>() ;
    
    Trigger trigger;

    public void Restart()
    {
        foreach (GameObject pixel in Pixels)
            Destroy(pixel);

        Pixels.Clear();
    }

    void Start()
    {
        trigger = GetComponentInChildren<Trigger>();
        trigger.OnStay = (Collider other) =>
        {
            if (Pixels.Count > 1000)
            {
                GameObject toRemove = Pixels[0];
                Pixels.Remove(toRemove);
                Destroy(toRemove);
            }

            if (other.tag == "Player")
            {
                Vector3 position = new Vector3(other.transform.position.x, transform.position.y + 0.4f, other.transform.position.z);
                if (Pixels.Any(p =>
                    Mathf.Abs((p.transform.position - position).x) < 0.1f
                        && Mathf.Abs((p.transform.position - position).y) < 0.1f
                        && Mathf.Abs((p.transform.position - position).z) < 0.1f)
                )
                {

                }
                else
                {
                    Pixels.Add(Instantiate(Pixel, position, Quaternion.identity));
                }
            }
        };
    }
}
