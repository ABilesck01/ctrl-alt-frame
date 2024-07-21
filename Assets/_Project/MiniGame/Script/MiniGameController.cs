using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    [SerializeField] private int sequenceAmount;
    [SerializeField] private int actionAmountBySequence;

    [SerializeField] private List<Sequence> enemySequence;
    [SerializeField] private List<Sequence> playerSequence;
    private void Awake()
    {
        RandomSequence();
    }
    private void RandomSequence()
    {
        for (int i = 0; i < actionAmountBySequence; i++)
        {
            int randSequence = Random.Range(0,4);
            enemySequence.Add((Sequence)randSequence);
        }
        StartCoroutine(showSequenceToPlayer());
    }
    IEnumerator showSequenceToPlayer()
    {
        for (int i = 0; i < enemySequence.Count; i++)
        {
            Debug.Log(enemySequence[i]); 
            yield return new WaitForSeconds(0.5f);  
        }
    }

    [ContextMenu("Check MiniGame")]
    private void CheckMiniGame()
    {
        if(enemySequence.Count != playerSequence.Count) {
            Debug.Log("wrong sequence");
            return;
        }
        for(int i = 0;i < enemySequence.Count; i++)
        {
            if (playerSequence[i] != enemySequence[i])
            {
                Debug.Log("wrong sequence");
                return;
            }
        }
        Debug.Log("right sequence");
    }
}
 public enum Sequence
{
    up , down, left, right      
}


