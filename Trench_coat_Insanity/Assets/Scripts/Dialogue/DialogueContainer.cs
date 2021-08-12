using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Create New Dialogue Container", fileName = "newDialogueContainer")]
public class DialogueContainer : ScriptableObject
{
	public string[] text;
	public AudioClip[] voiceLines;

	public PersonSpeaking[] personSpeaking;

	public DialogueContainer choice0;
	public DialogueContainer choice1;
	
	public string choice0text = "";
	public string choice1text = "";

	public bool doSwitchScene;
	public int sceneToSwitchTo;
	
	public enum PersonSpeaking
	{
		none,
		man,
		woman,
		doc,
		choice
	}

	public int Length => text.Length;

	public string GetText(int i)
	{
		return text[i];
	}

	public AudioClip GetVoiceLine(int i)
	{
		if (i < voiceLines.Length)
		{
			return voiceLines[i];
		}

		return null;
	}

	public PersonSpeaking GetPersonSpeaking(int i)
	{
		return personSpeaking[i];
	}
}