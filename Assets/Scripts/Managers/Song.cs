using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Song : SingleInstance<Song> 
{
    public List<AudioClip> Songs;

    private AudioSource audioSource;

    private Coroutine pitchCoroutine;

    public static void ChangePitch(float pitch, float smoothing = 1f)
    {
        if (Instance.audioSource == null) return;
        if (Instance.pitchCoroutine != null) Instance.StopCoroutine(Instance.pitchCoroutine);
        Instance.pitchCoroutine = Instance.StartCoroutine(ChangePitchCoroutine(pitch, smoothing));
    }

    private static IEnumerator ChangePitchCoroutine(float pitch, float smoothing)
    {
        while (Instance.audioSource.pitch != pitch)
        {
            Instance.audioSource.pitch = Instance.audioSource.pitch.MoveOverTime(pitch, smoothing);
            yield return new WaitForUpdate();
        }
    }

    public static void Stop()
    {
        if (Instance.audioSource != null)
            Instance.audioSource.Stop();
    }

    /// <summary>
    /// Start playing a song.
    /// </summary>
    /// <param name="songName">The name of the song to play.</param>
    public static void Play(string songName)
    {
        AudioClip clip = Instance.Songs.Find(f => f.name == songName);

        if (clip == null) 
        {
            print($"Couldn't find a clip with the name {songName}");
            return;
        };

        if (Instance.audioSource == null) 
        {
            GameObject go = new GameObject();
            go.name = songName;
            go.AddComponent<DontDestroyOnLoad>();
            go.AddComponent<AudioSource>();
            Instance.audioSource = go.GetComponent<AudioSource>();
            Instance.audioSource.volume = CONSTANTS.MUSIC_VOLUME;
            Instance.audioSource.loop = true;
        }

        if (Instance.audioSource.clip == clip) return;

        Instance.audioSource.clip = clip;
        Instance.audioSource.Play();
    }
}