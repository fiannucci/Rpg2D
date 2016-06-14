using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    public GameObject[] EnemySpawnPoints;
    public GameObject[] EnemyPrefabs;
    public AnimationCurve SpawnAnimationCurve;

    private Animator battleStateManager;
    private Dictionary<int, BattleState> battleStateHash = new Dictionary<int, BattleState>();
    private BattleState currentBattleState;

    private int enemyCount;

    public enum BattleState
    {
        Begin_Battle,
        Intro,
        Player_Move,
        Player_Attack,
        Change_Control,
        Enemy_Attack,
        Battle_Result,
        Battle_End
    }

    void GetAnimationStates()
    {
        foreach(BattleState state in (BattleState[])System.Enum.GetValues(typeof(BattleState)))
        {
            battleStateHash.Add(Animator.StringToHash("Base Layer." + state.ToString()), state);
        }
    }

    void Start()
    {
        battleStateManager = GetComponent<Animator>();
        GetAnimationStates();
        enemyCount = Random.Range(1, EnemySpawnPoints.Length);
        StartCoroutine(SpawnEnemies());
        
    }

    void Update()
    {
        currentBattleState = battleStateHash[battleStateManager.GetCurrentAnimatorStateInfo(0).nameHash]; // da rivedere

        switch (currentBattleState)
        {
            case BattleState.Intro:
                break;
            case BattleState.Player_Move:
                break;
            case BattleState.Player_Attack:
                break;
            case BattleState.Change_Control:
                break;
            case BattleState.Enemy_Attack:
                break;
            case BattleState.Battle_Result:
                break;
            case BattleState.Battle_End:
                break;
            default:
                break;
        }
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            var newEnemy = (GameObject)Instantiate(EnemyPrefabs[0]);
            newEnemy.transform.position = new Vector3(7, -1, 0);

            yield return StartCoroutine(MoveCharachterToPoint(EnemySpawnPoints[i], newEnemy));

            newEnemy.transform.parent = EnemySpawnPoints[i].transform;
        }
        battleStateManager.SetBool("BattleReady", true);
    }

    IEnumerator MoveCharachterToPoint(GameObject destination, GameObject charachter)
    {
        float timer = 0f;
        var StartPosition = charachter.transform.position;
        
        if(SpawnAnimationCurve.length > 0)
        {
            while(timer < SpawnAnimationCurve.keys[SpawnAnimationCurve.length - 1].time)
            {
                charachter.transform.position = Vector3.Lerp(StartPosition, destination.transform.position, SpawnAnimationCurve.Evaluate(timer));
                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            charachter.transform.position = destination.transform.position;            
        }
        yield return null;
    }

    void OnGUI() // DA MODIFICARE CON CALMA(sostituire con UI elements)
    {
        switch (currentBattleState)
        {
            case BattleState.Begin_Battle:
                break;
            case BattleState.Intro:
                GUI.Box(new Rect((Screen.width / 2) - 150, 50, 300, 50), "Battle between Player and Goblins");
                break;
            case BattleState.Player_Move:
                if (GUI.Button(new Rect(10, 10, 100, 50), "Run Away"))
                {
                    GameState.PlayerReturningHome = true;
                    NavigationManager.NavigateTo("World");
                }
                break;
            case BattleState.Player_Attack:
                break;
            case BattleState.Change_Control:
                break;
            case BattleState.Enemy_Attack:
                break;
            case BattleState.Battle_Result:
                break;
            case BattleState.Battle_End:
                break;
            default:
                break;
        }                
    }
}
