using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConstelationController : MonoBehaviour
{
    [System.Serializable]
    public class ConstelationData
    {
        public ConstelationType type;
        public int starCount;
        public bool hasConstelation;
        public void CheckForCompletion(List<Star> stars)
        {
            if(stars.Count != starCount)
            {
                Debug.Log("Nao tem todas as estrelas =(");
                return;
            }

            hasConstelation = true;

            foreach(Star star in stars)
            {
                star.DestroySelf();
            }

            //play festival animations
        }
    }

    public ConstelationData data;

    public static ConstelationController instance;

    public UnityEvent<ConstelationController> OnFinishConstelation;

    private void Start()
    {
        MiniGameController.instance.OnCorrectMinigame.AddListener(CheckForCompletion);
    }

    private void CheckForCompletion()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            List<Star> stars = new List<Star>();

            Star[] starsInScene = FindObjectsOfType<Star>();
            for(int i = 0; i < starsInScene.Length; i++)
            {
                if (starsInScene[i].currentStarSate == StarState.FollowingPlayer)
                {
                    if (starsInScene[i].constelation == data.type)
                    {
                        stars.Add(starsInScene[i]);
                    }
                }
            }

            data.CheckForCompletion(stars);
            if(data.hasConstelation)
            {
                OnFinishConstelation?.Invoke(this);
            }
            
        }
    }
}

public enum ConstelationType
{
    constelation_01,
    constelation_02,
    constelation_03,
    constelation_04
}
