using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sound : SingleInstance<Sound>
{
    public List<AudioClip> Sounds;
    private static List<AudioSource> audioSources = new List<AudioSource>();

    void Start()
    {
        SceneManager.sceneUnloaded += (Scene scene) =>
        {
            audioSources.Clear();
        };
    }

    void Update()
    {
        // Clean up audio sources that are done.
        for (int i = 0; i < audioSources.Count; i++)
        {
            var audioSource = audioSources[i];

            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSources.Remove(audioSource);
                Destroy(audioSource.gameObject);
            }
        }
    }

    /// <summary>
    /// Play a sound effect.
    /// </summary>
    /// <param name="sfxName">The name of the sfx to play.</param>
    public static void Play(string sfxName, bool interupt = true, float volume = 1, bool oneOnly = false, int spatialBlend = 0, Vector3? position = null, float pitch = 1)
    {
        AudioClip clip = Instance.Sounds.Find(f => f.name == sfxName);

        if (clip == null) return;

        var audioSource = (interupt || oneOnly) ? audioSources.Find(s => s.gameObject.name == clip.name) : null;

        if (oneOnly && audioSource != null) return;

        if (audioSource == null)
        {
            GameObject go = Game.New(clip.name);
            audioSource = go.AddComponent<AudioSource>();
            audioSources.Add(audioSource);
            audioSource.volume = volume;
            audioSource.spatialBlend = spatialBlend;
            if (position.HasValue) audioSource.transform.position = position.Value;
            audioSource.pitch = pitch;
        }
        else
        {
            audioSource.Stop();
        }

        audioSource.PlayOneShot(clip);
    }
}
