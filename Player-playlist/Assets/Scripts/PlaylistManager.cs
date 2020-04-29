using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Audio;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlaylistManager : MonoBehaviour
{
    //singleton structure
    private static PlaylistManager _instance;
    public static PlaylistManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Playlist Manager Instance is null.");
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    //____________________________________
    
    public AudioSource myAudioSource;
    
    
    //create an enum with different states: fade, no fade, continuous
    public enum FadeType
    {
        Fade,
        NoFade,
        Continuous
    }

    public FadeType currentFadeType;

    public bool loop;

    public bool shuffle;
    

    //list of AudioItems
	public AudioClip[] audioPlaylist;
    //public AudioItem[] audioPlaylist;
    //public 

    void Start()
    {

        //AudioItem[] gos = (Resources.LoadAll("Ground", AudioItem));


        //theAudio = Resources.LoadAll("Audio");

        audioPlaylist = Resources.LoadAll<AudioClip>("Audio"); //as AudioClip[];
        foreach (AudioClip audio in audioPlaylist)
        {
            Debug.Log("AudioClip: " + audio.name);
        }



    }

    // Use this for initialization
    public void StartRoutines ()
    {
        
        switch (currentFadeType)
		{
            case FadeType.NoFade:
                StartCoroutine(PlayNoFade());
                break;

            case FadeType.Fade:
                StartCoroutine(PlayFade());
                break;

            case FadeType.Continuous:
                break;
        }
        
    }

    public void Stop()
    {
        myAudioSource.Stop();
        StopAllCoroutines();
        if (GameObject.Find("Pause Button").GetComponentInChildren<Text>().text == "Resume")
            GameObject.Find("Pause Button").GetComponentInChildren<Text>().text = "Pause";
    }

    public void Pause ()
    {
        if (myAudioSource.isPlaying)
        {
            myAudioSource.Pause();
            GameObject.Find("Pause Button").GetComponentInChildren<Text>().text = "Resume";
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1.0f;
            myAudioSource.Play();
            GameObject.Find("Pause Button").GetComponentInChildren<Text>().text = "Pause";
        }
        
    }

    public void SetLoop()
    {
        if(loop == false)
        {
            loop = true;
            GameObject.Find("Loop Button").GetComponentInChildren<Image>().color = Color.green;
        }
        else
        {
            loop = false;
            GameObject.Find("Loop Button").GetComponentInChildren<Image>().color = Color.white;
        }
    }

    public void SetShuffle()
    {
        if(shuffle == false)
        {
            shuffle = true;
            GameObject.Find("Shuffle Button").GetComponentInChildren<Image>().color = Color.green;
        }
        else
        {
            shuffle = false;
            GameObject.Find("Shuffle Button").GetComponentInChildren<Image>().color = Color.white;
        }
    }

    // Update is called once per frame
    void Update ()
	{
        
	}

    //COROUTINES

        

    IEnumerator PlayNoFade()
    {
        

        while (shuffle == true)
        {

            int _lastClip = -1;

            
            int _currentClip = UnityEngine.Random.Range(0, audioPlaylist.Length);

            while (_lastClip == _currentClip)
            {
                _currentClip = UnityEngine.Random.Range(0, audioPlaylist.Length);
            }
            myAudioSource.clip = audioPlaylist[_currentClip];//.myClip;
            //myAudioSource.volume = audioPlaylist[i].volumeSlider;
            myAudioSource.Play();
            yield return new WaitForSeconds(myAudioSource.clip.length);
            _lastClip =  _currentClip;
            



        }


        while (loop == true)
        {
            for (int i = 0; i < audioPlaylist.Length; i++)
            {
                myAudioSource.clip = audioPlaylist[i];//.myClip;
                //myAudioSource.volume = audioPlaylist[i].volumeSlider;
                myAudioSource.Play();
                yield return new WaitForSeconds(myAudioSource.clip.length);
            


                if (i == audioPlaylist.Length)
                    i = 0;

            }
        }


        foreach (AudioClip audio in audioPlaylist)
        {
            myAudioSource.clip = audio;//.myClip;
            //myAudioSource.volume = audio.volumeSlider;
            myAudioSource.Play();
            yield return new WaitForSeconds(myAudioSource.clip.length);

        }

    }

    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    //int i;

    IEnumerator FadeIn(float speed)
    {

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

    IEnumerator FadeOut(float speed)
    {
        
        myAudioSource.volume = _maxVolume;
        float audioVolume = myAudioSource.volume;

        while (myAudioSource.volume > _minVolume)
        {
            audioVolume -= speed;
            myAudioSource.volume = audioVolume;
            yield return new WaitForSeconds(0.1F);
        }
    }

    IEnumerator PlayFade()
    {
        
        while (shuffle == true)
        {
            int _lastClip = -1;

            int _currentClip = UnityEngine.Random.Range(0, audioPlaylist.Length);

            while (_lastClip == _currentClip)
            {
                _currentClip = UnityEngine.Random.Range(0, audioPlaylist.Length);
            }
            myAudioSource.clip = audioPlaylist[_currentClip];
            StartCoroutine(FadeIn(0.05f));
            yield return new WaitForSeconds(myAudioSource.clip.length - 3);
            StartCoroutine(FadeOut(0.05f));
            yield return new WaitForSeconds(3f);

            _lastClip = _currentClip;
        }

        //loop
        //start fade in coroutine with the first clip
        //wait until the end of the clip
        //start fade out coroutine
        //start fade in second clip



        while (loop == true)
        {
            for (int i = 0; i < audioPlaylist.Length; i++)
            {
                myAudioSource.clip = audioPlaylist[i];//.myClip;
                StartCoroutine(FadeIn(0.05f));
                yield return new WaitForSeconds(myAudioSource.clip.length - 3);
                StartCoroutine(FadeOut(0.05f));
                yield return new WaitForSeconds(3f);


                if (i == audioPlaylist.Length)
                    i = 0;

            }

        }

        for (int i = 0; i < audioPlaylist.Length; i++)
        {
            myAudioSource.clip = audioPlaylist[i];//.myClip;
            StartCoroutine(FadeIn(0.05f));
            yield return new WaitForSeconds(myAudioSource.clip.length - 3);
            StartCoroutine(FadeOut(0.05f));
            yield return new WaitForSeconds(3f);
        }
        


    }

}
