using UnityEngine;
using System.Collections;

public class MapMovement : MonoBehaviour {

    Vector3 StartLocation;
    Vector3 TargetLocation;
    float timer = 0;
    bool inputActive = true;
    bool inputReady = true;
    bool startedTravelling = false;
    private Collider2D playerCollider;

    void Awake()
    {
        playerCollider = GetComponent<Collider2D>();
        playerCollider.enabled = false;
    }
    void Start()
    {
        MessaggingManager.Instance.SubscribeUIEvent(UpdateInputAction);
    }

    private void UpdateInputAction(bool uiVisible)
    {
        inputReady = !uiVisible;
    }
	void Update ()
    {
        
        if (inputActive && Input.GetMouseButtonUp(0))
        {
            StartLocation = transform.position.ToVector3_2D();
            timer = 0;
            TargetLocation = WorldExtensions.GetScreenPositionFor2D(Input.mousePosition);
            startedTravelling = true;
        }
        else if(inputActive && Input.touchCount == 1)
        {
            StartLocation = transform.position.ToVector3_2D();
            timer = 0;
            TargetLocation = WorldExtensions.GetScreenPositionFor2D(Input.GetTouch(0).position);
            startedTravelling = true;
        }

        if(TargetLocation != Vector3.zero && TargetLocation != transform.position && TargetLocation!= StartLocation)
        {
            transform.position = Vector3.Lerp(StartLocation, TargetLocation, timer);
            timer += Time.deltaTime;
        }
        if(startedTravelling && Vector3.Distance(StartLocation, transform.position.ToVector3_2D()) > 0.5f)
        {
            playerCollider.enabled = true;
            startedTravelling = false;
        }
        if(!inputReady && inputActive)
        {
            TargetLocation = this.transform.position;
            Debug.Log("Stopping player");
        }
        inputActive = inputReady;
    }
}
