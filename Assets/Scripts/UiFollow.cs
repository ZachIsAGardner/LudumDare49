using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiFollow : MonoBehaviour
{
    public Transform Target;

    private RectTransform rectTransform;
    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
        rectTransform = GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        Camera.main.ResetWorldToCameraMatrix(); // Force camera matrix to be updated
        Vector3 targetPosition = Camera.main.WorldToScreenPoint(Target.position);
        if (targetPosition.z > 0)
        {
            Vector3 position = new Vector3(
                Mathf.Round(targetPosition.x),
                Mathf.Round(targetPosition.y),
                Mathf.Round(targetPosition.z)
            ); // Pixel snapping
            position.z = 0;

            // rectTransform.SetPositionAndRotation(position, Quaternion.identity);
        }
        // transform.position = camera.WorldToScreenPoint(Target.position);
    }
}
