using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaylistManager : MonoBehaviour
{
    AudioSource _myAudioSource;
    //create an enum with different states: fade, no fade, continuous
    public enum FadeType
    {
        Fade,
        NoFade,
        Continuous
    }

    public FadeType currentFadeType;

    //list of AudioItems
	public List<AudioItem> audioPlaylist = new List<AudioItem>();

    

	// Use this for initialization
	void Start ()
    {
        _myAudioSource.clip = audioPlaylist[0].myClip;
        _myAudioSource.Play();

    }

    // Update is called once per frame
	void Update ()
    {
        
	}

    

    
}
