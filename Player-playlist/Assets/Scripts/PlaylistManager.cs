using System.Collections;
using System.Collections.Generic;
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
                StartCoroutine(NoFade());
                break;

            case FadeType.Fade:
                break;

            case FadeType.Continuous:
                break;
        }
        
    }

    // Update is called once per frame
    void Update ()
	{

	}

    
	IEnumerator NoFade ()
    {
        foreach (AudioItem audio in audioPlaylist)
        {
            myAudioSource.clip = audio.myClip;
            myAudioSource.Play();
            yield return new WaitForSeconds(myAudioSource.clip.length);

        }
    }

}
