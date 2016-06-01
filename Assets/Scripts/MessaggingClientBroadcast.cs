using UnityEngine;
using System.Collections;

public class MessaggingClientBroadcast : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        MessaggingManager.Instance.Broadcast();
    }
}
