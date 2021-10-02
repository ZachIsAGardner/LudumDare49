using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiThrust : MonoBehaviour
{
    TextMeshProUGUI text;
    Image image;
    Player player;
    float disappearTime = 0;

    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        image = GetComponentInChildren<Image>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.Thrust == player.MaxThrust)
        {
            disappearTime += Time.deltaTime;
        }
        else
        {
            disappearTime = 0;
        }

        text.text = Mathf.Max(Mathf.Round(player.Thrust * 100), 0).ToString();

        if (disappearTime > 1)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (2 * Time.deltaTime));
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - (2 * Time.deltaTime));
        }
        else
        {
            image.color = Color.black;

            if (player.Thrust == player.MaxThrust)
            {
                text.color = Color.white;
            }
            else
            {
                text.color = new Color(
                    Color.white.r,
                    Color.white.g * (player.Thrust / player.MaxThrust),
                    Color.white.b * (player.Thrust / player.MaxThrust)
                );
            }
        }
    }
}
