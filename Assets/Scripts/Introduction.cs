using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Introduction : MonoBehaviour
{
    public Animator Animator;

    void Start()
    {
        _ = Cutscene();
        Song.Play("Introduction");
    }

    async Task Cutscene()
    {
        Animator.SetInteger("State", 1);

        await new WaitForSeconds(1f);

        var textbox = await Dialogue.Begin(new TextBoxModel(
            text: "I love flying in space.",
            speaker: "Boxelda"
        ));

        await Dialogue.Next(textbox, new TextBoxModel(
            text: "I sure hope nothing <color=#ff8000>Unstable</color> happens.",
            speaker: "Boxelda"
        ));

        Animator.SetInteger("State", 2);

        await new WaitForSeconds(1);

        await Dialogue.End(textbox, new TextBoxModel(
            text: "Uh oh!!!",
            speaker: "Boxelda"
        ));

        await new WaitForSeconds(1);

        await Game.LoadAsync("Test", Prefabs.Get("FadeSceneTransition").GetComponent<SceneTransition>());
    }
}
