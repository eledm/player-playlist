using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Audio;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaylistManager : MonoBehaviour
{
    public AudioSource myAudioSource;
    //create an enum with different states: fade, no fade, continuous
    public enum FadeType
    {
        Fade,
        NoFade,
        Continuous
    }

    public FadeType currentFadeType;

    public bool isLooping;

    //list of AudioItems
	public List<AudioItem> audioPlaylist = new List<AudioItem>();


    

	// Use this for initialization
	void Start ()
    {

        //myAudioSource.clip = audioPlaylist[0].myClip;
        //myAudioSource.Play();
        switch (currentFadeType)
		{
            case FadeType.NoFade:
                NoFadeClass.Instance.NoFadePlay();
                break;

            case FadeType.Fade:
                StartCoroutine(FadeIn(0.01f));
                break;

            case FadeType.Continuous:
                break;
        }
        
    }

    // Update is called once per frame
    void Update ()
	{

	}

    //COROUTINES

    /*IEnumerator PlayNoFade()
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

    }***/

    private float _minVolume = 0f;
    private float _maxVolume = 1f;

    IEnumerator FadeIn(float speed)
    {

        myAudioSource.clip = audioPlaylist[0].myClip;
        myAudioSource.volume = _minVolume;
        float audioVolume = myAudioSource.volume;
        myAudioSource.Play();

        while (myAudioSource.volume < _maxVolume)
        {
            audioVolume += speed;
            myAudioSource.volume = audioVolume;
            yield return new WaitForSeconds(0.1F);
        }

    }

}
