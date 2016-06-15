using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    private BattleManager battleManager;
    public Enemy EnemyProfile;
    Animator enemyAI;

    private bool selected;
    GameObject selectionCircle;


    public BattleManager BattleManager
    {
        get
        {
            return battleManager;
        }
        set
        {
            battleManager = value;
        }
    }

    IEnumerator SpinObject(GameObject target)
    {
        while (true)
        {
            target.transform.Rotate(0, 0, 180 * Time.deltaTime);
            yield return null;
        }
    }

    void OnMouseDown()
    {
        if (battleManager.CanSelectEnemy)
        {
            var selection = !selected;
            battleManager.ClearSelectedEnemy();
            selected = selection;
            if (selected)
            {
                selectionCircle = GameObject.Instantiate(battleManager.selectionCircle) as GameObject;
                selectionCircle.transform.parent = transform;
                selectionCircle.transform.localPosition = new Vector3(0, -0.5f, 0); ;
                StartCoroutine("SpinObject",selectionCircle);
                battleManager.SelectEnemy(this, EnemyProfile.Name);
            }
        }
    }

    public void ClearSelection()
    {
        if (selected)
        {
            selected = false;
            if(selectionCircle != null)
            {
                DestroyObject(selectionCircle);
                StopCoroutine("SpinObject");
            }
        }
    }

    public void UpdateAI()
    {
        if (enemyAI != null && EnemyProfile != null)
        {
            enemyAI.SetInteger("EnemyHealth", EnemyProfile.Health);
            enemyAI.SetInteger("PlayerHealth", GameState.currentPlayer.Health);
            enemyAI.SetInteger("EnemiesInBattle", battleManager.EnemyCount);
        }
    }

    void Awake()
    {
        enemyAI = GetComponent<Animator>();
        if (enemyAI == null)
            Debug.LogWarning("No AI System found");
    }
    void Update()
    {
        UpdateAI();
    }
}
