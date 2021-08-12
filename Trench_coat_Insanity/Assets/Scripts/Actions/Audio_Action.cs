using UnityEngine;

/**
 * Activates a sound clip through the audio player singleton class
 */
public class Audio_Action : ActionType
{
    [SerializeField] private AudioClip _soundToPlay;
    [SerializeField, Range(0,1f)] private float _audioStrength = 1f;

    public override void Activate()
    {
        Audio.AudioPlayer.PlayAudio(_soundToPlay, _audioStrength);
    }
}
