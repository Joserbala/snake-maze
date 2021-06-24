using UnityEngine;
using UnityEngine.Audio;

namespace SnakeMaze.SO
{
    [CreateAssetMenu(fileName = "AudioClip", menuName = "Scriptables/Audio/AudioClip")]
    public class AudioConfigSO : ScriptableObject
    {
        [SerializeField] private AudioMixerGroup audioMixerGroup;
        [Range(0f, 1f)] [SerializeField] private float volume;
        [Range(-3f, 3f)] [SerializeField] private float pitch;
        [Range(-1f, 1f)] [SerializeField] private float panStereo;
        [Range(0f, 1.1f)] [SerializeField] private float reverbZoneMix;
        [SerializeField] private bool ignorePause;

        public void ApplyTo(AudioSource audioSource)
        {
            audioSource.outputAudioMixerGroup = audioMixerGroup;
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.panStereo = panStereo;
            audioSource.reverbZoneMix = reverbZoneMix;
            audioSource.ignoreListenerPause = ignorePause;

        }
    }
}
