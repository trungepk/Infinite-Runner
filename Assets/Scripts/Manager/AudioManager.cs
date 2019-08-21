using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance;
    [HideInInspector] public AudioSource currentAudio;

    [SerializeField] private Sound[] sounds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);

        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound " + name + " not found");
            return;
        }
        currentAudio = s.source;
        s.source.Play();
    }

    private void Start()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0: Play("Menu BGM"); break;
            case 1: Play("Start BGM"); break;
            default: Debug.Log("WTF"); break;
        }
    }

}
