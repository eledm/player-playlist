using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
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

        _dropdown = FindObjectOfType<Dropdown>();
    }


    //____________________________________
   


    public AudioSource myAudioSource;

    Dropdown _dropdown;

    public AudioMixer myAM;


    bool _loop;

    bool _shuffle;
    
	public AudioClip[] audioPlaylist;
    

    void Start()
    {
        audioPlaylist = Resources.LoadAll<AudioClip>("Audio"); 
        foreach (AudioClip audio in audioPlaylist)
        {
            Debug.Log("AudioClip: " + audio.name);
        }

}

    

    public void StartRoutines ()
    {

        if(_dropdown.value == 0)
        {
            StartCoroutine(PlayNoFade());
        }
        else
        if( _dropdown.value == 1)
        {
            StartCoroutine(PlayFade());
        }

    }

   
    public void Stop()
    {
        myAudioSource.Stop();
        StopAllCoroutines();
        if (GameObject.Find("Pause Button").GetComponentInChildren<Text>().text == "Resume")
        {
            GameObject.Find("Pause Button").GetComponentInChildren<Text>().text = "Pause";
            GameObject.Find("SongName").GetComponentInChildren<Text>().text = "Now playing: " ;
        }
            

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
        if(_loop == false)
        {
            _loop = true;
            GameObject.Find("Loop Button").GetComponentInChildren<Image>().color = Color.green;
        }
        else
        {
            _loop = false;
            GameObject.Find("Loop Button").GetComponentInChildren<Image>().color = Color.white;
        }
    }

    public void SetShuffle()
    {
        if(_shuffle == false)
        {
            _shuffle = true;
            GameObject.Find("Shuffle Button").GetComponentInChildren<Image>().color = Color.green;
        }
        else
        {
            _shuffle = false;
            GameObject.Find("Shuffle Button").GetComponentInChildren<Image>().color = Color.white;
        }
    }

    public void MasterVolume(float masterLvl)
    {
        myAM.SetFloat("masterVol", Mathf.Log10(masterLvl) * 20);
    }

    

    public void PlayNext()
    {
        var indexOfSong = Array.IndexOf(audioPlaylist, myAudioSource.clip);
        Debug.Log(indexOfSong);
        indexOfSong++;
        if (indexOfSong >= audioPlaylist.Length)
            indexOfSong = 0;
 
        myAudioSource.clip = audioPlaylist[indexOfSong];
       
        myAudioSource.Play();
        GameObject.Find("SongName").GetComponentInChildren<Text>().text = "Now playing: " + myAudioSource.clip.name;
    }

    public void PlayPrevious()
    {
        int indexOfSong = Array.IndexOf(audioPlaylist, myAudioSource.clip);
        indexOfSong--;
        
        if (indexOfSong < 0)
            indexOfSong = 0;

        Debug.Log(indexOfSong);

        myAudioSource.clip = audioPlaylist[indexOfSong];
        myAudioSource.Play();
        GameObject.Find("SongName").GetComponentInChildren<Text>().text = "Now playing: " + myAudioSource.clip.name;
    }

    
    
    // Update is called once per frame
    void Update ()
	{
        
	}

    //COROUTINES

        

    IEnumerator PlayNoFade()
    {
        int _lastClip = -1;


        while (_shuffle == true)
        {

            


            int _currentClip = UnityEngine.Random.Range(0, audioPlaylist.Length);

            while (_lastClip == _currentClip)
            {
                _currentClip = UnityEngine.Random.Range(0, audioPlaylist.Length);
            }
            Debug.Log(_currentClip);
            myAudioSource.clip = audioPlaylist[_currentClip];//.myClip;           
            myAudioSource.Play();
            GameObject.Find("SongName").GetComponentInChildren<Text>().text = "Now playing: " + myAudioSource.clip.name;
            yield return new WaitForSeconds(myAudioSource.clip.length);
            _lastClip = _currentClip;
            Debug.Log(_lastClip);

        }



        while (_loop == true)
        {
            for (int i = 0; i < audioPlaylist.Length; i++)
            {
                myAudioSource.clip = audioPlaylist[i];//.myClip;                
                myAudioSource.Play();
                GameObject.Find("SongName").GetComponentInChildren<Text>().text = "Now playing: " + myAudioSource.clip.name;
                yield return new WaitForSeconds(myAudioSource.clip.length);
            


                if (i == audioPlaylist.Length)
                    i = 0;

            }
        }

        foreach (AudioClip audio in audioPlaylist)
        {
            myAudioSource.clip = audio;//.myClip;
            myAudioSource.Play();
            GameObject.Find("SongName").GetComponentInChildren<Text>().text = "Now playing: " + myAudioSource.clip.name;
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
        int _lastClip = -1;

        while (_shuffle == true)
        {
            

            int _currentClip = UnityEngine.Random.Range(0, audioPlaylist.Length);

            while (_lastClip == _currentClip)
            {
                _currentClip = UnityEngine.Random.Range(0, audioPlaylist.Length);
            }
            Debug.Log(_currentClip);
            myAudioSource.clip = audioPlaylist[_currentClip];
            StartCoroutine(FadeIn(0.05f));
            GameObject.Find("SongName").GetComponentInChildren<Text>().text = "Now playing: " + myAudioSource.clip.name;
            yield return new WaitForSeconds(myAudioSource.clip.length - 3);
            StartCoroutine(FadeOut(0.05f));
            yield return new WaitForSeconds(3f);

            _lastClip = _currentClip;
            Debug.Log(_lastClip);
        }

        //loop
        //start fade in coroutine with the first clip
        //wait until the end of the clip
        //start fade out coroutine
        //start fade in second clip



        while (_loop == true)
        {
            for (int i = 0; i < audioPlaylist.Length; i++)
            {
                myAudioSource.clip = audioPlaylist[i];//.myClip;
                StartCoroutine(FadeIn(0.05f));
                GameObject.Find("SongName").GetComponentInChildren<Text>().text = "Now playing: " + myAudioSource.clip.name;
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
            GameObject.Find("SongName").GetComponentInChildren<Text>().text = "Now playing: " + myAudioSource.clip.name;
            yield return new WaitForSeconds(myAudioSource.clip.length - 3);
            StartCoroutine(FadeOut(0.05f));
            yield return new WaitForSeconds(3f);
        }
        


    }

}
