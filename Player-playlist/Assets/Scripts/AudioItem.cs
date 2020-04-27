using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;


[System.Serializable]
public class AudioItem
{
    public string name;
    public AudioClip myClip;
    //[Range (0.0f,1.0f)]
    //public float volumeSlider;


    //public Volume vol;
    //public Panning pan;
    //public Pitch pitch;

    public AudioItem(AudioClip aClip)
    {
        this.name = myClip.name;
        this.myClip = aClip;
        
         
    }

    public static explicit operator AudioItem(UnityEngine.Object v)
    {
        throw new NotImplementedException();
    }
}

/*public class Volume : AudioItem
{
	
}*/