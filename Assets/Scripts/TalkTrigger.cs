using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TalkTrigger : MonoBehaviour
{
    public Camera Camera;

    bool busy = false;
    RectTransform prompt;
    float timer;

    void Start()
    {
        prompt = GameObject.FindGameObjectWithTag("TalkPrompt").GetComponent<RectTransform>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
    }

    void OnTriggerStay(Collider other)
    {
        if (timer > 0 || busy) return;

        if (other.tag == "Player")
        {
            prompt.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 106);

            if (Input.GetKey(KeyCode.C))
            {
                Player player = FindObjectOfType<Player>();
                if (!player.Paused)
                {
                    _ = Execute();
                }
            }
        }
    }

    async Task Execute()
    {
        busy = true;
        timer = 1f;
        prompt.anchoredPosition = new Vector2(0, 50000);
        Camera oldCamera = Camera.main;
        oldCamera.gameObject.SetActive(false);
        Camera.gameObject.SetActive(true);
        Player player = FindObjectOfType<Player>();
        player.Pause();

        TextBox textBox = await Dialogue.Begin(new TextBoxModel(
            text: "Wow watching you crash your spaceship was pretty cool.",
            speaker: "Alium"
        ));

        await Dialogue.Next(textBox, new TextBoxModel(
            text: "Can you do it again?",
            speaker: "Alium"
        ));

        await Dialogue.Next(textBox, new TextBoxModel(
            text: "My friends are always talking about cool they are, maybe they can help you fix it.",
            speaker: "Alium"
        ));

        await Dialogue.End(textBox, new TextBoxModel(
            text: "So I can watch you crash it again.",
            speaker: "Alium"
        ));
        
        busy = false;
        timer = 1f;
        Camera.gameObject.SetActive(false);
        oldCamera.gameObject.SetActive(true);
        player.UnPause();
    }

    void OnTriggerExit(Collider other)
    {
        prompt.anchoredPosition = new Vector2(0, 50000);
    }
}
