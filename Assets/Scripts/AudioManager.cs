using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] Clips;
    public static AudioManager Instance;

    public float VolumeMusic { get; private set; }

    AudioClip GetClip(string name)
    {
        foreach (var item in Clips)
        {
            if (item.name == name)
            {
                return item;
            }

        }
            return null;
    }

    public void PlayMusic(string name)
    {
        AudioClip clip = GetClip(name);

        if (clip != null)
        {
            GameObject go = new GameObject();
            go.name = name;
            AudioSource source = go.AddComponent<AudioSource>();
            source.clip = clip;
            source.volume = VolumeMusic;
            source.loop = true;
            source.Play();
        }
    }

    private void Awake()
        {
            Instance = this;
            
            PlayMusic("TreetopTrouble");
        }


    public void Play(string name)
        {
            AudioClip clip = GetClip(name);

            if (clip != null)
            {
                GameObject go = new GameObject();
                go.name = name;
                AudioSource source = go.AddComponent<AudioSource>();
                source.clip = clip;
                source.volume = VolumeMusic;
                source.Play();
                GameObject.Destroy(go, source.clip.length);
            }
        
        }

}


