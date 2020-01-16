using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
[System.Serializable]
public class Sound
{

    public string name;

    public AudioClip clip;

    [Range(0f,1f)]
    public float volume;
    [Range(0f,3f)]
    public float pitch;
    [Range(0f,1f)]
    public float spatialBlend;
    [Range(0,1000)]
    public float MinimumDist;
    [Range(0,1000)]
    public float MaximumDist;

    [HideInInspector]
    public AudioSource source;
    
}
