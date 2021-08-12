using Audio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
	[RequireComponent(typeof(Canvas))]
	public class DialogueUICanvas : MonoBehaviour
	{
		public TextMeshProUGUI textToChange;
		public Button button;

		public GameObject manSprite;
		public GameObject womanSprite;
		public GameObject doctorSprite;

		public Button dialogueChoice0button;
		private DialogueContainer _choice0;
		public Button dialogueChoice1button;
		private DialogueContainer _choice1;


		private Dialogue _activeDialogue;

		private void Awake()
		{
			button.onClick.AddListener(OnButtonClick);
			dialogueChoice0button.onClick.AddListener(OnDialogueChoice0);
			dialogueChoice1button.onClick.AddListener(OnDialogueChoice1);
			button.gameObject.SetActive(false);
		}
		
		public void InitConversation(Dialogue dialogue)
		{
			button.gameObject.SetActive(true);
			_activeDialogue = dialogue;
			
			OnButtonClick();
		}

		public void EndConversation(Dialogue dialogue)
		{
			Reset();

			if (dialogue.currentContainer.doSwitchScene)
			{
				LevelManager.Instance.LoadSceneFromIndex(dialogue.currentContainer.sceneToSwitchTo, 1);
			}
		}

		private void OnButtonClick()
		{
			if (_activeDialogue.NextConvoText(out string convoText, out AudioClip voiceLine, out DialogueContainer.PersonSpeaking personSpeaking))
			{
				textToChange.text = convoText;

				if (voiceLine != null)
				{
					AudioPlayer.PlayAudio(voiceLine);
				}

				switch (personSpeaking)
				{
					case DialogueContainer.PersonSpeaking.none:
						manSprite.SetActive(false);
						womanSprite.SetActive(false);
						doctorSprite.SetActive(false);

						dialogueChoice0button.gameObject.SetActive(false);
						dialogueChoice1button.gameObject.SetActive(false);
						button.gameObject.SetActive(true);
						break;
					case DialogueContainer.PersonSpeaking.man:
						manSprite.SetActive(true);
						womanSprite.SetActive(false);
						doctorSprite.SetActive(false);

						dialogueChoice0button.gameObject.SetActive(false);
						dialogueChoice1button.gameObject.SetActive(false);
						button.gameObject.SetActive(true);
						break;
					case DialogueContainer.PersonSpeaking.woman:
						manSprite.SetActive(false);
						womanSprite.SetActive(true);
						doctorSprite.SetActive(false);

						dialogueChoice0button.gameObject.SetActive(false);
						dialogueChoice1button.gameObject.SetActive(false);
						button.gameObject.SetActive(true);
						break;
					case DialogueContainer.PersonSpeaking.doc:
						manSprite.SetActive(false);
						womanSprite.SetActive(false);
						doctorSprite.SetActive(true);

						dialogueChoice0button.gameObject.SetActive(false);
						dialogueChoice1button.gameObject.SetActive(false);
						button.gameObject.SetActive(true);
						break;
					case DialogueContainer.PersonSpeaking.choice:
						DialogueContainer[] containers = _activeDialogue.GetChoices();
						manSprite.SetActive(false);
						womanSprite.SetActive(false);
						doctorSprite.SetActive(false);

						_choice0 = containers[0];
						_choice1 = containers[1];
						
						button.gameObject.SetActive(false);

						TextMeshProUGUI text0 = dialogueChoice0button.gameObject.GetComponentInChildren<TextMeshProUGUI>();
						if (text0 != null)
						{
							text0.text = _activeDialogue.currentContainer.choice0text;
						}
						
						TextMeshProUGUI text1 = dialogueChoice1button.gameObject.GetComponentInChildren<TextMeshProUGUI>();
						if (text1 != null)
						{
							text1.text = _activeDialogue.currentContainer.choice1text;
						}
						
						dialogueChoice0button.gameObject.SetActive(true);
						dialogueChoice1button.gameObject.SetActive(true);
						
						Debug.Log("Time for a choice");
						break;
					default:
						//Reset();
						break;
				}
			}
		}
		
		private void OnDialogueChoice0()
		{
			Reset();
			_activeDialogue.SetActiveContainer(_choice0);
			OnButtonClick();
		}

		private void OnDialogueChoice1()
		{
			Reset();
			_activeDialogue.SetActiveContainer(_choice1);
			OnButtonClick();
		}

		private void Reset()
		{
			button.gameObject.SetActive(false);
			manSprite.SetActive(false);
			womanSprite.SetActive(false);

			button.gameObject.SetActive(false);
			dialogueChoice0button.gameObject.SetActive(false);
			dialogueChoice1button.gameObject.SetActive(false);
			
			_activeDialogue.Reset();
		}
	}
}