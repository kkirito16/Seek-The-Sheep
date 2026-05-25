using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class BGMManager : MonoBehaviour
{
    public static BGMManager instance { get; private set; }

    [Header("Music")]
    public List<BGM> bgms = new List<BGM>();


    AudioSource audioSrc;
    AudioClip main;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);

        audioSrc = GetComponent<AudioSource>();
        main = FindBGMByName(BGMtype.main);
        audioSrc.clip = main;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            audioSrc.mute = !audioSrc.mute;
        }



    }

    public void ChangeBGM(BGMtype name)
    {
        if (audioSrc.clip != FindBGMByName(name))
        {
            audioSrc.clip = FindBGMByName(name);
            audioSrc.Play();
        }
    }





    public AudioClip FindBGMByName(BGMtype name)
    {
        for (int i = 0; i < bgms.Count; i++)
        {
            if (bgms[i].name == name)
            {
                print("[bgm]find " + name);
                return bgms[i].clip;
            }
        }
        print("not found!");
        return null;
    }

}


[System.Serializable]
public class BGM
{
    public BGMtype name;
    public AudioClip clip;

}
