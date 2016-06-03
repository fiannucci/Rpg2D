using UnityEngine;
using System.Collections;

public class ConversationManager : Singleton<ConversationManager> {

    bool talking = false;
    ConversationEntry currentConversationLine;
    public int displayTextureOffset = 70;
    Rect scaledTextureRect;
    int fontSpacign = 8;
    int conversationTextWidt;
    int dialogHeight = 80;

    protected ConversationManager () { } //SERVE PER NON FAR CREARE CON IL COSTRUTTORE NORMALE???

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
            conversationTextWidt = currentConversationLine.ConversationText.Length * fontSpacign;
            scaledTextureRect = new Rect(currentConversationLine.DisplayPic.textureRect.x / currentConversationLine.DisplayPic.texture.width, currentConversationLine.DisplayPic.textureRect.y / currentConversationLine.DisplayPic.texture.height, currentConversationLine.DisplayPic.textureRect.width / currentConversationLine.DisplayPic.texture.width, currentConversationLine.DisplayPic.textureRect.height / currentConversationLine.DisplayPic.texture.height);
            yield return new WaitForSeconds(4);
        }
        talking = false;
        yield return null;
    }

    void OnGUI()
    {
        if (talking)
        {
            GUI.BeginGroup(new Rect(Screen.width / 2 - conversationTextWidt / 2, 50, conversationTextWidt + 10, dialogHeight));
            GUI.Box(new Rect(0, 0, conversationTextWidt + 30, dialogHeight),"");
            GUI.Label(new Rect(displayTextureOffset, 10, conversationTextWidt + 30, 20), currentConversationLine.speakingCharachterName);
            GUI.Label(new Rect(displayTextureOffset, 30, conversationTextWidt + 30, 20), currentConversationLine.ConversationText);
            GUI.DrawTextureWithTexCoords(new Rect(10, 10, 50, 50), currentConversationLine.DisplayPic.texture, scaledTextureRect);
            GUI.EndGroup();
        }
    }

}
