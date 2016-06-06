using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIBalloon : MonoBehaviour {

	[SerializeField]
	private Text _speakerName;

	[SerializeField]
	private Image _speakerImage;

	[SerializeField]
	private Text _bodyText;

	public void Show(ConversationEntry conversationEntry)
	{
		_speakerName.text = conversationEntry.speakingCharachterName;
		_speakerImage.sprite = conversationEntry.DisplayPic;
		_bodyText.text = conversationEntry.ConversationText;

		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
}
