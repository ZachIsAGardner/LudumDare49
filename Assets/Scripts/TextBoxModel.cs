using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TextBoxModel
{
    public string Text;
    public string Speaker;
    public bool? Auto;
    public string Tone;
    public float? CrawlTime;
    public bool? CloseWhenDone;
    public int? ToneIntervalMax;
    public List<string> ProceedInputs = null;

    public TextBoxModel(
        string text = null,
        string speaker = "",
        bool? auto = false,
        string tone = null,
        float? crawlTime = null,
        bool? closeWhenDone = true,
        int? toneIntervalMax = null,
        List<string> proceedInputs = null
    )
    {
        Text = text;
        Speaker = speaker;
        Auto = auto;
        Tone = tone ?? CONSTANTS.TEXT_TONE;
        CrawlTime = crawlTime ?? CONSTANTS.CRAWL_TIME;
        CloseWhenDone = closeWhenDone;
        ToneIntervalMax = toneIntervalMax;
        ProceedInputs = proceedInputs;
    }
}
