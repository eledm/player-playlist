using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoFadeClass : PlaylistManager
{
    public static NoFadeClass Instance;

    private void Awake()
    {

        Instance = this;
    }

    public void NoFadePlay()
    {
        StartCoroutine(PlayNoFade());
    }
    
    private IEnumerator PlayNoFade()
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

    }
}
