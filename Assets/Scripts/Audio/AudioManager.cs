using System;
using SnakeMaze.Enums;
using SnakeMaze.SO;
using SnakeMaze.SO.Audio;
using UnityEngine;

namespace SnakeMaze.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private SkinContainerSO skinContainer;
        [SerializeField] private BusAudioSO busAudio;
        [SerializeField] private BusAudioSO busMusic;
        [SerializeField] private SoundEmitterPoolSO pool;
        [SerializeField] private int initSize = 4;

        private SoundEmitter _musicEmitter;

        private void Awake()
        {
            pool.Prewarm(initSize);
            pool.SetParent(transform);
        }

        private void OnEnable()
        {
            busAudio.OnAudioPlay += PlayAudioClip;

            busMusic.OnAudioPlay += PlayMusic;
            busMusic.OnMusicStop += StopMusic;

        }
        private void OnDestroy()
        {
            busAudio.OnAudioPlay -= PlayAudioClip;

            busMusic.OnAudioPlay -= PlayMusic;
            busMusic.OnMusicStop -= StopMusic;
        }

        private void PlayMusic(AudioClipType clipType, AudioConfigSO settings)
        {
            AudioClipSO clip = skinContainer.CurrentAudioSkin.AudioDic[clipType];
            if (_musicEmitter != null && _musicEmitter.IsPlaying())
            {
                if (_musicEmitter.GetClip() == clip.Clip)
                    return;
                _musicEmitter.StopMusic();
            }

            _musicEmitter = pool.Request();
            _musicEmitter.PlayAudioClip(clip.Clip, settings, true);
            _musicEmitter.OnFinishedPlaying += StopMusicEmitter;
        }

        private void PlayAudioClip(AudioClipType clipType, AudioConfigSO settings)
        {
            AudioClipSO clip = skinContainer.CurrentAudioSkin.AudioDic[clipType];
            SoundEmitter soundEmitter = pool.Request();
            if (soundEmitter != null)
            {
                soundEmitter.PlayAudioClip(clip.Clip, settings, clip.Loop);
                soundEmitter.OnFinishedPlaying += OnSoundEmitterFinished;
            }
            else
            {
                throw new NullReferenceException();
            }
        }
        private void StopMusic(AudioConfigSO settings)
        {

            if (_musicEmitter == null || !_musicEmitter.IsPlaying()) return;
            _musicEmitter.OnFinishedPlaying += StopMusicEmitter;
            _musicEmitter.StopMusic();

        }

        private void OnSoundEmitterFinished(SoundEmitter soundEmitter)
        {
            StopAndCleanEmitter(soundEmitter);
        }

        private void StopAndCleanEmitter(SoundEmitter soundEmitter)
        {
            if (!soundEmitter.IsLooping())
                soundEmitter.OnFinishedPlaying -= OnSoundEmitterFinished;
            soundEmitter.Stop();
            pool.Return(soundEmitter);
        }

        private void StopMusicEmitter(SoundEmitter soundEmitter)
        {
            soundEmitter.OnFinishedPlaying -= StopMusicEmitter;
            pool.Return(soundEmitter);
        }
    }
}
