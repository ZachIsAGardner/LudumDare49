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
    }

    async Task Cutscene()
    {
        Animator.SetInteger("State", 1);

        var textbox = await Dialogue.Begin(new TextBoxModel(
            text: "I sure do love flying in space.",
            speaker: "Boxelda"
        ));

        await Dialogue.Next(textbox, new TextBoxModel(
            text: "I sure hope nothing Unstable happens.",
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
