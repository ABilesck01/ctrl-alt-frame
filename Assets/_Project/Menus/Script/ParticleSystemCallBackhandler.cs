using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParticleSystemCallBackhandler : MonoBehaviour
{
    public UnityEvent OnParticleFinish;

    private void OnParticleSystemStopped()
    {
        OnParticleFinish?.Invoke();
    }
}
