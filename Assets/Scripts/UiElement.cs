using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiElement : MonoBehaviour
{
    private TextMeshProUGUI text;
    private Image image;

    private bool fadeIn;
    private bool fadeOut;

    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        image = GetComponentInChildren<Image>();

        text.color = Color.clear;
        image.color = Color.clear;
    }

    void Update()
    {
        if (fadeIn)
        {
            text.color = new Color(Color.white.r, Color.white.g, Color.white.b, text.color.a + Time.deltaTime);
            image.color = new Color(Color.black.r, Color.black.g, Color.black.b, image.color.a + Time.deltaTime);
        }
        else
        {
            text.color = new Color(Color.white.r, Color.white.g, Color.white.b, text.color.a - Time.deltaTime);
            image.color = new Color(Color.black.r, Color.black.g, Color.black.b, image.color.a - Time.deltaTime);
        }
    }

    public void FadeIn()
    {
        fadeIn = true;
        fadeOut = false;
        text.color = Color.clear;
        image.color = Color.clear;
    }

    public void FadeOut()
    {
        fadeIn = false;
        fadeOut = true;
        text.color = Color.white;
        image.color = Color.black;
    }
}
