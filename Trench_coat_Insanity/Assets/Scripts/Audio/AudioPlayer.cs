using UnityEngine;

namespace Audio
{
	/// <summary>
	/// Usage: Add AudioClip to GameObject and call __ AudioPlayer.instance.PlayAudio(audicClipName); __ whenever you
	/// want to play the clip.
	/// </summary>
	[RequireComponent(typeof(AudioSource))]
	public class AudioPlayer : MonoBehaviour
	{
		static AudioPlayer _instance; // singleton
		AudioSource[] _audioSource;

		void Awake()
		{
			if (_instance == null)
			{
				_instance = this;
			}

			_audioSource = GetComponents<AudioSource>();
		}

		// plays audio (interrupts any ongoing audio)
		public static void PlayAudio(AudioClip audioClip, float volumeScale = 1)
		{
			print("Playing audio: " + audioClip.ToString());
			if (_instance._audioSource[0].isPlaying)
			{
				if (_instance._audioSource.Length < 2)
				{
					_instance._audioSource[0].Stop();
					_instance._audioSource[0].PlayOneShot(audioClip, volumeScale);
				}
				else
					_instance._audioSource[1].PlayOneShot(audioClip, volumeScale);
			}

			else
			{
				_instance._audioSource[0].PlayOneShot(audioClip, volumeScale);
			}
		}
	}
}