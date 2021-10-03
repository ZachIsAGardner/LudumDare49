using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Dialogue : SingleInstance<Dialogue>
{
    private static TextBox instance;

    public static async Task<TextBox> Begin(TextBoxModel model)
    {
        if (instance != null) 
        {
            Destroy(instance.gameObject);
        }

        model.CloseWhenDone = false;

        var prefab = Prefabs.Get("TextBox");
        var go = Game.NewCanvasElement(prefab);
        var textBox = go.GetComponent<TextBox>();
        instance = textBox;
        await textBox.ExecuteAsync(model);
        await new WaitForUpdate();
        return textBox;
    }

    public static async Task<TextBox> Next(TextBox textBox, TextBoxModel model)
    {
        if (textBox == null) return null;
        model.CloseWhenDone = false;

        await textBox.ExecuteAsync(model);
        await new WaitForUpdate();
        return textBox;
    }

    public static async Task End(TextBox textBox, TextBoxModel model)
    {
        if (textBox == null) return;

        model.CloseWhenDone = true;
        await textBox.ExecuteAsync(model);
    }

    public static async Task Single(TextBoxModel model)
    {
        if (instance != null) 
        {
            Destroy(instance.gameObject);
        }

        model.CloseWhenDone = true;

        var prefab = Prefabs.Get("TextBox");
        var go = Game.NewCanvasElement(prefab);
        var textBox = go.GetComponent<TextBox>();
        instance = textBox;
        await textBox.ExecuteAsync(model);
        await new WaitForUpdate();
    }
}
