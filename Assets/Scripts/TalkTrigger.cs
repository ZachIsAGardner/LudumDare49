using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class TalkTrigger : MonoBehaviour
{
    public Camera Camera;
    public SceneTransition Fade;

    bool busy = false;
    RectTransform prompt;
    float timer;

    public string Key = "";

    GameObject teleporter = null;

    void Awake()
    {
        teleporter = FindObjectsOfType<Teleporter>().ToList().Find(t => !t.Exit).gameObject;
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
            case "Artist":
                await Artist();
                break;
            case "Shadow":
                await Shadow();
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
            text: "My friends are always talking about how cool they are, maybe they can help you fix it.",
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
            text: "Here take this spacehip part!",
            speaker: "Cow Man"
        ));

        Game.GotCowPart = true;

        await SpaceshipPart(textBox);
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

    async Task Artist()
    {
        Paper paper = FindObjectOfType<Paper>();

        if (paper.Pixels.Count() > 150 || Game.GotArtistPart)
        {
            TextBox textBox = await Dialogue.Begin(new TextBoxModel(
                text: "Wow I love your drawing!",
                speaker: "Fartist"
            ));

            await Dialogue.Next(textBox, new TextBoxModel(
               text: "I just happen to have this cool thing. You can have it!",
               speaker: "Fartist"
           ));

            Game.GotArtistPart = true;

            await SpaceshipPart(textBox);
        }
        else
        {
            TextBox textBox = await Dialogue.Begin(new TextBoxModel(
                text: "Hello! I love drawing!",
                speaker: "Fartist"
            ));

            await Dialogue.End(textBox, new TextBoxModel(
                text: "Do you think you could make me a drawing?",
                speaker: "Fartist"
            ));
        }
    }

    async Task Spaceship()
    {
        if (Game.GotArtistPart && Game.GotButtonPart && Game.GotCowPart)
        {
            var textbox = await Dialogue.Begin(new TextBoxModel(
                text: $"(The spaceship is impressed by the parts you have collected.)"
            ));

            await Dialogue.End(textbox, new TextBoxModel(
                text: $"(It feels like flying again!)"
            ));

            _ = Game.LoadAsync("End", Fade);
        }
        else
        {
            await Dialogue.Single(new TextBoxModel(
                text: $"(Looks like you are missing some spaceship parts.)"
            ));
        }
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
                    text: "I will need some time to be alone.",
                    speaker: "Stupidius"
                ));

                await Dialogue.Next(textbox, new TextBoxModel(
                    text: "Please take the teleporter and get out of here. You have insulted me beyond words.",
                    speaker: "Stupidius"
                ));

                Game.GotButtonPart = true;
                toePart = 1;
                teleporter.SetActive(true);

                await SpaceshipPart(textbox);
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

    async Task Shadow()
    {
        var textbox = await Dialogue.Begin(new TextBoxModel(
            text: "I heard that when you're high in the air, it's really helpful to look at your shadow.",
            speaker: "Shadow"
        ));

        await Dialogue.Next(textbox, new TextBoxModel(
            text: "It goes straight down, so you'll know where you'll land.",
            speaker: "Shadow"
        ));

        await Dialogue.End(textbox, new TextBoxModel(
           text: "Wow!",
           speaker: "Shadow"
       ));
    }

    async Task SpaceshipPart(TextBox textbox)
    {
        if (Game.GotArtistPart && Game.GotButtonPart && Game.GotCowPart)
        {
            Sound.Play("Win", true, 0.5f, false, 0, null, 1.5f);
            await Dialogue.Next(textbox, new TextBoxModel(
                text: "(Got the spaceship part!)"
            ));

            await Dialogue.End(textbox, new TextBoxModel(
                text: "(It looks like you have everything you need, better return to you spaceship.)"
            ));
        }
        else
        {
            int count = 3;

            if (Game.GotArtistPart) count--;
            if (Game.GotButtonPart) count--;
            if (Game.GotCowPart) count--;

            Sound.Play("Win", true, 0.5f, false, 0, null, 1.25f);
            await Dialogue.Next(textbox, new TextBoxModel(
                text: "(Got the spaceship part!)"
            ));

            await Dialogue.End(textbox, new TextBoxModel(
                text: $"(It feels like there {(count == 1 ? "is" : "are")} {count} more you need)"
            ));
        }
    }
}
