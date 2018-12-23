using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{


    public Sound[] sounds;


    public AudioMixerGroup musicGroup;
    public AudioMixerGroup sfxGroup;
    public AudioMixerGroup foleyGroup;
    // Use this for initialization
    void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();

            sound.source.clip = sound.clip;

            if (sound.name == "Music")
            {
                sound.source.outputAudioMixerGroup = musicGroup;
                sound.source.loop = true;
                sound.source.playOnAwake = true;
            }
            else if (sound.group == "Foley")
            {
                sound.source.outputAudioMixerGroup = foleyGroup;
            }
            else if (sound.group == "SFX")
            {
                sound.source.outputAudioMixerGroup = sfxGroup;
            }
            else
            {
                sound.source.outputAudioMixerGroup = foleyGroup;
            }

        }
    }

    void Start()
    {
        Play("Music");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("No music");
            return;
        }
        s.source.Play();
    }

    public bool IsPlaying(string audio)
    {
        Sound s = Array.Find(sounds, sound => sound.name == audio);
        if (s != null && s.source.isPlaying)
        {
            Debug.Log("playing clip");
            return true;
        }
        else
        {
            Debug.Log("not playing");
            return false;
        }
    }

    public void Stop(string audioClip)
    {
        Sound s = Array.Find(sounds, sound => sound.name == audioClip);
        if (s != null && s.source.isPlaying)
        {
            s.source.Stop();
        }
    }
}
