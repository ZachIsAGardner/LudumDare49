using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiControls : MonoBehaviour
{
    private float showTimer = 2;
    private float hideTimer = 6;

    private TextMeshProUGUI text;
    private Image image;

    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        image = GetComponentInChildren<Image>();

        text.color = Color.clear;
        image.color = Color.clear;
    }

    void Update()
    {
        showTimer -= Time.deltaTime;
        hideTimer -= Time.deltaTime;

        if (hideTimer <= 0)
        {
            text.color = new Color(Color.white.r, Color.white.g, Color.white.b, text.color.a - Time.deltaTime);
            image.color = new Color(Color.black.r, Color.black.g, Color.black.b, image.color.a - Time.deltaTime);
        }
        else if (showTimer <= 0)
        {
            text.color = new Color(Color.white.r, Color.white.g, Color.white.b, text.color.a + Time.deltaTime);
            image.color = new Color(Color.black.r, Color.black.g, Color.black.b, image.color.a + Time.deltaTime);
        }
    }
}
