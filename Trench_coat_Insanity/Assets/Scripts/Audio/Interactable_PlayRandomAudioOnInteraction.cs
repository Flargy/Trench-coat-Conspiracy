using Interaction;
using UnityEngine;

namespace Audio
{
	public class Interactable_PlayRandomAudioOnInteraction : MonoBehaviour, IInteractable
	{
		public AudioClip[] audioClips;

		void Awake()
		{
			if (audioClips == null)
			{
				Debug.LogError(name + ": no AudioClips set.");
			}
		}

		public void Interact()
		{
			int i = Random.Range(0, audioClips.Length);
			
			AudioPlayer.PlayAudio(audioClips[i]);
		}
	}
}
