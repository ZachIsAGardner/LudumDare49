using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Prefabs: SingleInstance<Prefabs>
{
    public List<GameObject> Items;

    public static GameObject Get(string name)
    {
        var result = Instance.Items.First(i => i.name == name);

        if (result == null) 
        {
            throw new System.Exception($"Couldn't find prefab with name {name}");
        }

        return result;
    }

    public static T Get<T>(string name)
    {
        var result = Instance.Items.First(i => i.name == name).GetComponent<T>();

        if (result == null) 
        {
            throw new System.Exception($"Couldn't find prefab with name {name}");
        }

        return result;
    }
}