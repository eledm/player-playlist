using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioItem
{
    public string name;
    public AudioClip myClip;
    
    //public Volume vol;
    //public Panning pan;
    //public Pitch pitch;

    public AudioItem(AudioClip aClip)
    {
        this.myClip = aClip;
    }
	
}

/*public class Volume : AudioItem
{
	
}*/