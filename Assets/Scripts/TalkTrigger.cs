using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class TalkTrigger : MonoBehaviour
{
    public Camera Camera;

    bool busy = false;
    RectTransform prompt;
    float timer;

    public string Key = "";

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
        player.gameObject.SetActive(false);

        await DoTalk();

        busy = false;
        timer = 1f;
        Camera.gameObject.SetActive(false);
        oldCamera.gameObject.SetActive(true);
        player.UnPause();
        player.gameObject.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        prompt.anchoredPosition = new Vector2(0, 50000);
    }

    async Task DoTalk()
    {
        switch (Key)
        {
            case "Cow":
                await Cow();
                break;
            case "CowComplete":
                await CowComplete();
                break;
            case "CowExtra":
                await CowExtra();
                break;
            case "Alium":
                await Alium();
                break;
            case "Spaceship":
                await Spaceship();
                break;
            case "Toe":
                await Toe();
                break;
            default:
                await DefaultTalk();
                break;
        }
    }

    async Task DefaultTalk()
    {
        await Dialogue.Single(new TextBoxModel(
            text: "Hello."
        ));
    }

    async Task Alium()
    {
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
    }

    async Task Cow()
    {
        TextBox textBox = await Dialogue.Begin(new TextBoxModel(
            text: "Hello my name is Cow Man.",
            speaker: "Cow Man"
        ));

        await Dialogue.Next(textBox, new TextBoxModel(
            text: "My cows need to be herded, its their favorite thing. But the thing is I just don't feel like it.",
            speaker: "Cow Man"
        ));

        await Dialogue.Next(textBox, new TextBoxModel(
            text: "If you could herd a few for me I'll give you a cool space ship part.",
            speaker: "Cow Man"
        ));

        await Dialogue.End(textBox, new TextBoxModel(
            text: "Thanks!",
            speaker: "Cow Man"
        ));
    }

    async Task CowComplete()
    {
        TextBox textBox = await Dialogue.Begin(new TextBoxModel(
            text: "You are amazing!",
            speaker: "Cow Man"
        ));

        await Dialogue.Next(textBox, new TextBoxModel(
            text: "Here take this countom cbaooo!",
            speaker: "Cow Man"
        ));

        await Dialogue.End(textBox, new TextBoxModel(
           text: "(Got the spaceship part!)"
       ));

        Game.GotCowPart = true;
    }

    async Task CowExtra()
    {
        TextBox textBox = await Dialogue.Begin(new TextBoxModel(
            text: "Thanks for you help!",
            speaker: "Cow Man"
        ));

        await Dialogue.End(textBox, new TextBoxModel(
            text: "Go on now.",
            speaker: "Cow Man"
        ));
    }

    async Task Spaceship()
    {
        await Dialogue.Single(new TextBoxModel(
            text: $"Looks like you are missing some spaceship parts."
        ));
    }

    int toePart = 0;

    async Task Toe()
    {
        TicTacToe puzzle = FindObjectOfType<TicTacToe>();
        if (puzzle.PlayerVictory)
        {
            if (toePart == 0)
            {
                var textbox = await Dialogue.Begin(new TextBoxModel(
                    text: "I can't believe I've lost...",
                    speaker: "Stupidius"
                ));

                await Dialogue.Next(textbox, new TextBoxModel(
                    text: "I will need some time.",
                    speaker: "Stupidius"
                ));

                await Dialogue.End(textbox, new TextBoxModel(
                    text: "(You got the spaceship part!)"
                ));

                Game.GotButtonPart = true;
                toePart = 1;
            }
            else
            {
                await Dialogue.Single(new TextBoxModel(
                    text: "I will need some time.",
                    speaker: "Stupidius"
                ));
            }
        }
        else
        {
            var textbox = await Dialogue.Begin(new TextBoxModel(
                text: "Bet you can't beat me at this epic puzzle!",
                speaker: "Brainius"
            ));

            await Dialogue.End(textbox, new TextBoxModel(
                text: "I'll even let you go first!",
                speaker: "Brainius"
            ));

            FindObjectsOfType<TicTacToeTile>().ToList().ForEach(t => t.Ready = true);
        }
    }
}
