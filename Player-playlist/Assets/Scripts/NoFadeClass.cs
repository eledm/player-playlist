using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class NoFadeClass
{

    /*//singleton structure
    private static NoFadeClass _instance;
    public static NoFadeClass Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("NoFadeClass Instance is null");

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        
    }*/

    //

    /*public static  List<AudioItem> audioPlaylist = PlaylistManager.Instance.audioPlaylist;
    public static  AudioSource myAudioSource = PlaylistManager.Instance.myAudioSource;
    public static bool isLooping = PlaylistManager.Instance.loop; 


    public static void NoFadePlay()
    {
        //StartCoroutine(PlayNoFade());
    }

    /*private static void StartCoroutine(IEnumerator enumerator)
    {
        throw new NotImplementedException();
    }*/


        /*
    private static IEnumerator PlayNoFade()
    {
        
        while (isLooping == true)
        {
            for (int i = 0; i < audioPlaylist.Count; i++)
            {
                myAudioSource.clip = audioPlaylist[i].myClip;
                myAudioSource.Play();
                yield return new WaitForSeconds(myAudioSource.clip.length);
                Debug.Log(audioPlaylist.Count);


                if (i == audioPlaylist.Count)
                    i = 0;

            }
        }


        foreach (AudioItem audio in audioPlaylist)
        {
            myAudioSource.clip = audio.myClip;
            myAudioSource.Play();
            yield return new WaitForSeconds(myAudioSource.clip.length);

        }

    }*/
}
