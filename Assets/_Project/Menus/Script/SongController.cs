using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SongController : MonoBehaviour
{
    public static SongController instance;
    private FMOD.Studio.EventInstance eventInstance;
    public FMODUnity.EventReference fmodEvent;

    private void Awake()
    {
        instance = this;
    }

    public void SetSongParameter(int  parameter)
    {
        eventInstance.setParameterByName("ParameterName", parameter);
    }
}
