using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] StudioEventEmitter hitSound;
    [SerializeField] StudioEventEmitter deathSound;
    [SerializeField] StudioEventEmitter damageSound;
    [SerializeField] StudioEventEmitter idleSound;

    private void Start()
    {
        idleSound.Play();
    }

    public void PlayHitSound()
    {
        hitSound.Play();
    }

    public void PlayDeathSound()
    {
        deathSound.Play();
    }

    public void PlayDamageSound()
    {
        damageSound.Play();
    }

}
