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

    private InventoryItem selectedWeapon;

    private string selectedTargetName;
    private EnemyController selectedTarget;
    public GameObject selectionCircle;
    private bool canSelectEnemy = true;

    bool attacking = false;

    public bool CanSelectEnemy
    {
        get
        {
            return canSelectEnemy;
        }
    }

    public int EnemyCount
    {
        get
        {
            return enemyCount;
        }
    }
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

    public void SelectEnemy(EnemyController enemy, string name)
    {
        selectedTarget = enemy;
        selectedTargetName = name;
    }

    public void ClearSelectedEnemy()
    {
        if(selectedTarget != null)
        {
            var enemyController = selectedTarget.GetComponent<EnemyController>();
            enemyController.ClearSelection();
            selectedTarget = null;
            selectedTargetName = string.Empty;
        }
    }

    private void InventoryItemSelect(InventoryItem item)
    {
        selectedWeapon = item;
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
        MessaggingManager.Instance.SubscribeInventoryEvent(InventoryItemSelect);
        battleStateManager = GetComponent<Animator>();
        GetAnimationStates();
        enemyCount = Random.Range(1, EnemySpawnPoints.Length);
        StartCoroutine(SpawnEnemies());        
    }

    void OnDestroy()
    {
        MessaggingManager.Instance.UnSubscribeInventoryEvent(InventoryItemSelect);
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

            var controller = newEnemy.GetComponent<EnemyController>();
            controller.BattleManager = this;
            var EnemyProfile = ScriptableObject.CreateInstance<Enemy>();
            EnemyProfile.Level = 1;
            EnemyProfile.Damage = 1;
            EnemyProfile.Health = 2;
            EnemyProfile.Name = "Goblin " + (i+1).ToString();
            controller.EnemyProfile = EnemyProfile;
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
                if(selectedWeapon == null)
                {
                    GUI.Box(new Rect((Screen.width / 2) - 50, 10, 100, 50), "Select Weapon");
                }
                else if(selectedTarget == null)
                {
                    GUI.Box(new Rect((Screen.width / 2) - 50, 10, 100, 50), "Select Target");
                }
                else
                {
                    if(GUI.Button(new Rect((Screen.width / 2) - 50, 10, 100, 50), "Attack " + selectedTargetName))
                    {
                        canSelectEnemy = false;
                        battleStateManager.SetBool("PlayerReady", true);
                        MessaggingManager.Instance.BroadcastUIEvent(true);
                    }
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
