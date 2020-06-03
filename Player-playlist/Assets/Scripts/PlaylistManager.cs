using System;
using System.Collections;
using System.Security.Policy;
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

        playButton = GameObject.Find("Play Button");
    }


    //____________________________________
   


    public AudioSource myAudioSource;

    Dropdown _dropdown;

    GameObject playButton;

    public AudioMixer myAM;

    public Sprite pauseSprite;

    public Sprite playSprite;

    public Sprite shuffleOnSprite;

    public Sprite shuffleOffSprite;

    public Sprite loopOnSprite;

    public Sprite loopOffSprite;

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
        if (myAudioSource.isPlaying)
        {
            myAudioSource.Pause();
            Time.timeScale = 0;
            playButton.GetComponent<Image>().sprite = playSprite;
        }
        else
        if (myAudioSource.isPlaying == false)
        {

            playButton.GetComponent<Image>().sprite = pauseSprite;

            if (_pfRunning == true)
            {
                Time.timeScale = 1.0f;
                myAudioSource.UnPause();

            }
            else
            if (_pfRunning == true)
            {
                Time.timeScale = 1.0f;
                myAudioSource.UnPause();

            }
            else
            if (_dropdown.value == 0)
            {

                StartCoroutine(PlayNoFade());

            }
            else
            if (_dropdown.value == 1)
            {
                StartCoroutine(PlayFade());
              
            }

        }
    }

   
    public void Stop()
    {
        myAudioSource.Stop();
        StopAllCoroutines();
        _pfRunning = false;
        _pnfRunning = false;
        playButton.GetComponent<Image>().sprite = playSprite;
        GameObject.Find("SongName").GetComponent<Text>().text = "Now playing: ";
         

    }



    public void SetLoop()
    {
        if(_loop == false)
        {
            _loop = true;
            GameObject.Find("Loop Button").GetComponentInChildren<Image>().sprite = loopOnSprite;
        }
        else
        {
            _loop = false;
            GameObject.Find("Loop Button").GetComponentInChildren<Image>().sprite = loopOffSprite;
        }
    }

    public void SetShuffle()
    {
        if(_shuffle == false)
        {
            _shuffle = true;
            GameObject.Find("Shuffle Button").GetComponentInChildren<Image>().sprite = shuffleOffSprite;
        }
        else
        {
            _shuffle = false;
            GameObject.Find("Shuffle Button").GetComponentInChildren<Image>().sprite = shuffleOnSprite ;
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

    private bool _pnfRunning; 

    IEnumerator PlayNoFade()
    {
        _pnfRunning = true;
        Debug.Log("PNF Running");
        int _lastClip = -1;


        foreach (AudioClip audio in audioPlaylist)
        {
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
                GameObject.Find("SongName").GetComponent<Text>().text = "Now playing: " + myAudioSource.clip.name;
                yield return new WaitForSeconds(myAudioSource.clip.length);
                _lastClip = _currentClip;
                Debug.Log(_lastClip);

            }

            while (_loop == true)
            {
                
               
                    myAudioSource.clip = audioPlaylist[i];//.myClip;                
                    myAudioSource.Play();
                    GameObject.Find("SongName").GetComponentInChildren<Text>().text = "Now playing: " + myAudioSource.clip.name;
                    yield return new WaitForSeconds(myAudioSource.clip.length);



                    if (i == audioPlaylist.Length)
                        i = 0;

                
            }

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

    private bool _pfRunning;
    IEnumerator PlayFade()
    {
        _pfRunning = true;
        Debug.Log("PF running");
        int _lastClip = -1;

       

        for (int i = 0; i < audioPlaylist.Length; i++)
        {

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

            while (_loop == true)
            {

                    myAudioSource.clip = audioPlaylist[i];
                    StartCoroutine(FadeIn(0.05f));
                    GameObject.Find("SongName").GetComponentInChildren<Text>().text = "Now playing: " + myAudioSource.clip.name;
                    yield return new WaitForSeconds(myAudioSource.clip.length - 3);
                    StartCoroutine(FadeOut(0.05f));
                    yield return new WaitForSeconds(3f);


                    if (i == audioPlaylist.Length)
                        i = 0;

                

            }


            myAudioSource.clip = audioPlaylist[i];
            StartCoroutine(FadeIn(0.05f));
            GameObject.Find("SongName").GetComponentInChildren<Text>().text = "Now playing: " + myAudioSource.clip.name;
            yield return new WaitForSeconds(myAudioSource.clip.length - 3);
            StartCoroutine(FadeOut(0.05f));
            yield return new WaitForSeconds(3f);


        }
        


    }

}
