using System;
using System.Collections;
using SnakeMaze.Enums;
using SnakeMaze.SO;
using SnakeMaze.SO.Audio;
using UnityEngine;

namespace SnakeMaze.Audio
{
    public class MusicRequest : MonoBehaviour
    {
        [SerializeField] private AudioClipType clipType;
        [SerializeField] private bool playOnAwake;

        [SerializeField] private BusAudioSO busMusic;
        [SerializeField] private AudioConfigSO audioConfig;

        private void Awake()
        {
            if (playOnAwake)
                StartCoroutine(PlayDelayed());
        }

        private IEnumerator PlayDelayed()
        {
            yield return new WaitForSeconds(1f);
            
            PlayMusic();
        }

        public void PlayMusic()
        {
            Debug.Log("Playing Music");
            busMusic.OnAudioPlay?.Invoke(clipType, audioConfig);
        }
        private void StopAudio()
        {
            busMusic.OnAudioStop?.Invoke();
        }
        private void FinishAudio()
        {
            busMusic.OnAudioFinish?.Invoke();
        }
        public void StopMusic()
        {
            busMusic.OnMusicStop?.Invoke(audioConfig);
        }
    }
}