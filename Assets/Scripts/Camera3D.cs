using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Camera3D : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset = new Vector3(0, 16, 16);

    void Start()
    {

    }

    void LateUpdate()
    {
        if (Target != null)
        {
            transform.position = Target.transform.position + Offset;
        }
    }
}
