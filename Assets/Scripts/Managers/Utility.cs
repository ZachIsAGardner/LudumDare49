using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : SingleInstance<Utility>
{
    // max exclusive
    public static int RandomInt(int max) 
    {
        System.Random r = new System.Random();
        return r.Next(0, max);
    }
    public static int RandomInt(int min, int max) 
    {
        System.Random r = new System.Random();
        return r.Next(min, max);
    }
}
