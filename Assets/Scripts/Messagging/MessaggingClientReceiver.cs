using UnityEngine;
using System.Collections;

public class MessaggingClientReceiver : MonoBehaviour {
	
	void Start ()
    {
        MessaggingManager.Instance.Subscribe(ThePlayerIsTryingToLeave);
	}
	
    void OnDestroy()
    {
        MessaggingManager.Instance.UnSubscribe(ThePlayerIsTryingToLeave);
    }
    void ThePlayerIsTryingToLeave()
    {
        var dialog = GetComponent<ConversationComponent>();
        if (dialog != null)
        {
            if(dialog.Conversations != null && dialog.Conversations.Length > 0)
            {
                var conversation = dialog.Conversations[0];
                if (conversation != null)
                    ConversationManager.Instance.StartConversation(conversation);
            }
        }
    }
    
}
