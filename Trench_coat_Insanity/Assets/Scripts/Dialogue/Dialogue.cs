using Interaction;
using UnityEngine;

namespace Dialogue
{
	public class Dialogue : MonoBehaviour, IInteractable
	{
		public GameObject uiCanvasPrefab;
		public DialogueContainer startContainer;

		[HideInInspector] public DialogueContainer currentContainer;
		
		private DialogueUICanvas _uiCanvas;

		int currentI = 0;

		private void Awake()
		{
			_uiCanvas = uiCanvasPrefab.GetComponent<DialogueUICanvas>();
			Reset();	
		}

		public void Interact() // on mouse click on this object
		{
			SetActiveContainer(startContainer);
			_uiCanvas.InitConversation(this);
		}
		
		public void SetActiveContainer(DialogueContainer container)
		{
			currentContainer = container;
			currentI = 0;
		}

		public void Reset()
		{
			currentI = 0;
		}
		
		public bool NextConvoText(out string convoText, out AudioClip voiceLine, out DialogueContainer.PersonSpeaking personSpeaking)
		{
			if (currentI != currentContainer.Length)
			{
				convoText = currentContainer.GetText(currentI);
				voiceLine = currentContainer.GetVoiceLine(currentI);
				personSpeaking = currentContainer.GetPersonSpeaking(currentI);
				currentI++;
				return true;
			}

			_uiCanvas.EndConversation(this);
			
			currentI = 0;
			convoText = null;
			voiceLine = null;
			personSpeaking = DialogueContainer.PersonSpeaking.none;
			return false;
		}

		public DialogueContainer[] GetChoices()
		{
			return new[] {currentContainer.choice0, currentContainer.choice1};
		}
	}
}