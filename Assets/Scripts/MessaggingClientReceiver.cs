using UnityEngine;
using System.Collections;

public class MessaggingClientReceiver : MonoBehaviour {
	
	void Start ()
    {
        MessaggingManager.Instance.Subscribe(ThePlayerIsTryingToLeave);
	}
	
    void ThePlayerIsTryingToLeave()
    {
        Debug.Log("Don't leave me - " + tag.ToString());
    }
    
}
