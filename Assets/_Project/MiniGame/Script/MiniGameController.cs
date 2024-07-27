using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniGameController : MonoBehaviour
{
    public static MiniGameController instance;

    [SerializeField] private int sequenceAmount;
    [SerializeField] private int actionAmountBySequence;
    [SerializeField] private float startMinigameDelay = 1;

    [SerializeField] private List<Sequence> enemySequence;
    [SerializeField] private List<Sequence> playerSequence;
    [Header("Callbacks")]
    public UnityEvent<Character> OnStartMinimage;
    public UnityEvent OnCorrectMinigame;
    public UnityEvent OnWrongMinigame;

    public bool hasMinigame = false;
    private Star characterOnMinigame;

    public int SequenceAmount { get => sequenceAmount; set => sequenceAmount = value; }
    public int ActionAmountBySequence { get => actionAmountBySequence; set => actionAmountBySequence = value; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //inscribe events
        var allEnemies = FindObjectsOfType<EnemyHealth>();
        //foreach (var enemy in allEnemies)
        //{
        //    enemy.OnCharacterDeath.AddListener(StartMinigame);
        //}
    }

    public void AddActionToSequence(Sequence sequence)
    {
        playerSequence.Add(sequence);
    }

    public void StartMinigame(Star character)
    {
        characterOnMinigame = character;
        StartCoroutine(StartMinigameCoroutine());
    }

    private IEnumerator StartMinigameCoroutine()
    {
        yield return new WaitForSeconds(startMinigameDelay);
        OnStartMinimage?.Invoke(characterOnMinigame);
        RandomSequence();
        StartCoroutine(ShowSequenceToPlayer());
    }

    private void RandomSequence()
    {
        enemySequence.Clear();
        playerSequence.Clear();

        for (int i = 0; i < actionAmountBySequence; i++)
        {
            int randSequence = Random.Range(0,4);
            enemySequence.Add((Sequence)randSequence);
        }
        
    }
    IEnumerator ShowSequenceToPlayer()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < enemySequence.Count; i++)
        {
            Debug.Log(enemySequence[i]);
            characterOnMinigame.ShowSequence(enemySequence[i]);
            yield return new WaitForSeconds(1.5f);  
        }

        hasMinigame = true;
    }

    [ContextMenu("Check MiniGame")]
    public void CheckMiniGame()
    {
        hasMinigame = false;

        if (enemySequence.Count != playerSequence.Count) {
            Debug.Log("wrong sequence");
            characterOnMinigame.WrongMinigame();
            OnWrongMinigame?.Invoke();
            return;
        }
        for(int i = 0;i < enemySequence.Count; i++)
        {
            if (playerSequence[i] != enemySequence[i])
            {
                Debug.Log("wrong sequence");
                characterOnMinigame.WrongMinigame();
                OnWrongMinigame?.Invoke();
                return;
            }
        }

        Debug.Log("right sequence");
        characterOnMinigame.UnlockStar();
        OnCorrectMinigame?.Invoke();
    }
}
 public enum Sequence
{
    up , down, left, right      
}


