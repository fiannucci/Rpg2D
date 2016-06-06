using UnityEngine;
using System.Collections;

public class ConversationManager : Singleton<ConversationManager> {

    bool talking = false;
    ConversationEntry currentConversationLine;

    protected ConversationManager () { } 

    public void StartConversation(Conversation conversation)
    {
        if (!talking)
            StartCoroutine(DislpayConversation(conversation));
    }
    
    IEnumerator DislpayConversation(Conversation conversation)
    {
        talking = true;
        foreach(var conversationLine in conversation.ConversationLines)
        {
           	currentConversationLine = conversationLine;
			UIConversationPanel.Instance.ShowBalloon(currentConversationLine);
           	yield return new WaitForSeconds(4);
        }
        talking = false;

		UIConversationPanel.Instance.HideBallon();
        yield return null;
    }
}
