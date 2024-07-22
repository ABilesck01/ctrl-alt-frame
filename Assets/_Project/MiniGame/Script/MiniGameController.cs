using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniGameController : MonoBehaviour
{
    public static MiniGameController instance;

    [SerializeField] private int sequenceAmount;
    [SerializeField] private int actionAmountBySequence;

    [SerializeField] private List<Sequence> enemySequence;
    [SerializeField] private List<Sequence> playerSequence;
    [Header("Callbacks")]
    public UnityEvent OnCorrectMinigame;
    public UnityEvent OnWrongMinigame;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //inscribe events
        var allEnemies = FindObjectsOfType<EnemyHealth>();
        foreach (var enemy in allEnemies)
        {
            enemy.OnCharacterDeath.AddListener(StartMinigame);
        }
    }

    private void StartMinigame()
    {
        RandomSequence();
        StartCoroutine(showSequenceToPlayer());
    }

    private void RandomSequence()
    {
        for (int i = 0; i < actionAmountBySequence; i++)
        {
            int randSequence = Random.Range(0,4);
            enemySequence.Add((Sequence)randSequence);
        }
        
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
            OnWrongMinigame?.Invoke();
            return;
        }
        for(int i = 0;i < enemySequence.Count; i++)
        {
            if (playerSequence[i] != enemySequence[i])
            {
                Debug.Log("wrong sequence");
                OnWrongMinigame?.Invoke();
                return;
            }
        }

        Debug.Log("right sequence");
        OnCorrectMinigame?.Invoke();
    }
}
 public enum Sequence
{
    up , down, left, right      
}


