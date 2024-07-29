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
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        eventInstance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        eventInstance.start();
    }

    public void SetSongParameter(int parameter)
    {
        Debug.Log($"Tension - {parameter}");
        eventInstance.setParameterByName("Tension", parameter);
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        eventInstance.start();
    }
}
