using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MapMovement : MonoBehaviour {

    Vector3 StartLocation;
    Vector3 TargetLocation;
    float timer = 0;
    bool inputActive = true;
    bool inputReady = true;
    bool startedTravelling = false;
    bool battleStarted = false;
    private Collider2D playerCollider;
    
    private int EncounterChance = 100;
    private float EncounterDistance = 0; 

    void Awake()
    {
        
        playerCollider = GetComponent<Collider2D>();
        playerCollider.enabled = false;
        var lastPosition = GameState.GetLastScenePosition(SceneManager.GetActiveScene().name);

        if (lastPosition != Vector3.zero)
            transform.position = lastPosition;
    }
    void Start()
    {
        MessaggingManager.Instance.SubscribeUIEvent(UpdateInputAction);
    }

    private void UpdateInputAction(bool uiVisible)
    {
        inputReady = !uiVisible;
    }

    void OnDestroy()
    {
        GameState.SetLastScenePosition(SceneManager.GetActiveScene().name, transform.position);
    }

	void Update ()
    {
        
        if (inputActive && (Input.GetMouseButtonUp(0) || Input.touchCount == 1) )
        {
            StartLocation = transform.position.ToVector3_2D();
            timer = 0;
            TargetLocation = WorldExtensions.GetScreenPositionFor2D(Input.mousePosition);
            startedTravelling = true;

            var EncounterProbability = Random.Range(1, 100);
            if (EncounterProbability < EncounterChance && !GameState.PlayerReturningHome)
            {
                EncounterDistance = (Vector3.Distance(StartLocation, TargetLocation) / 100) * Random.Range(10, 100);
            }
            else
                EncounterDistance = 0;
        }
        /*else if(inputActive && Input.touchCount == 1)
        {
            StartLocation = transform.position.ToVector3_2D();
            timer = 0;
            TargetLocation = WorldExtensions.GetScreenPositionFor2D(Input.GetTouch(0).position);
            startedTravelling = true;
        }
        */

        if(TargetLocation != Vector3.zero && TargetLocation != transform.position && TargetLocation!= StartLocation)
        {
            transform.position = Vector3.Lerp(StartLocation, TargetLocation, timer);
            timer += Time.deltaTime;
        }

        if(startedTravelling && Vector3.Distance(StartLocation, transform.position.ToVector3_2D()) > 0.9f)
        {
            playerCollider.enabled = true;
            startedTravelling = false;
        }

        if (EncounterDistance > 0 && !battleStarted)
        {
            if(Vector3.Distance(StartLocation,transform.position) > EncounterDistance)
            {
                TargetLocation = Vector3.zero;
                battleStarted = true;
                NavigationManager.NavigateTo("Battle");
            }
        }
        if(!inputReady && inputActive)
        {
            TargetLocation = this.transform.position;
            Debug.Log("Stopping player");
        }

        inputActive = inputReady;
    }
}
