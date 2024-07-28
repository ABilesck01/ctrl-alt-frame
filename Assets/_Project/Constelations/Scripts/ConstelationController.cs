using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                Destroy(star.gameObject);
            }

            //play festival animations
        }
    }

    public ConstelationData[] data;

    public static ConstelationController instance;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            List<Star> stars01 = new List<Star>();
            List<Star> stars02 = new List<Star>();
            List<Star> stars03 = new List<Star>();
            List<Star> stars04 = new List<Star>();

            Star[] starsInScene = FindObjectsOfType<Star>();
            for(int i = 0; i < starsInScene.Length; i++)
            {
                if (starsInScene[i].currentStarSate == StarState.FollowingPlayer)
                {
                    switch (starsInScene[i].constelation)
                    {
                        case ConstelationType.constelation_01:
                            stars01.Add(starsInScene[i]);
                            break;
                        case ConstelationType.constelation_02:
                            stars02.Add(starsInScene[i]);
                            break;
                        case ConstelationType.constelation_03:
                            stars03.Add(starsInScene[i]);
                            break;
                        case ConstelationType.constelation_04:
                            stars04.Add(starsInScene[i]);
                            break;
                    }
                }
            }

            data[0].CheckForCompletion(stars01);
            data[1].CheckForCompletion(stars02);
            data[2].CheckForCompletion(stars03);
            data[3].CheckForCompletion(stars04);
            
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
