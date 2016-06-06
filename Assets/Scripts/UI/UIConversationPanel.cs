using UnityEngine;
using System.Collections;

public class UIConversationPanel : Singleton<UIConversationPanel> {

	[SerializeField]
	private UIBalloon _uiBalloon;

	public void ShowBalloon(ConversationEntry conversationEntry)
	{
		_uiBalloon.Show(conversationEntry);
	}

	public void HideBallon()
	{
		_uiBalloon.Hide();
	}
}
