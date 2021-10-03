using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper : MonoBehaviour
{
    public void PlaySound(string name)
    {
        Sound.Play(name, false, 0.125f);
    }
}
