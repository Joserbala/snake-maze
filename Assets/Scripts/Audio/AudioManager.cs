using System;
using System.Collections;
using System.Collections.Generic;
using SnakeMaze.Audio;
using SnakeMaze.Enums;
using SnakeMaze.SO;
using SnakeMaze.SO.Audio;
using UnityEngine;
using UnityEngine.Audio;

namespace SnakeMaze.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSkinSO audioSkin;
        [SerializeField] private BusAudioSO busAudio;
        [SerializeField] private BusAudioSO busMusic;
        [SerializeField] private SoundEmitterPoolSO pool;
        [SerializeField] private int initSize = 4;

        private SoundEmitter _musicEmitter;

        private void Awake()
        {
            pool.PreWarm(initSize);
            pool.SetParent(transform);
            audioSkin.InitDic();
        }

        private void OnEnable()
        {
            busAudio.OnAudioPlay += PlayAudioClip;
            
            busMusic.OnAudioPlay += PlayMusic;
            busMusic.OnAudioStop += StopMusic;

        }
        private void OnDestroy()
        {
            busAudio.OnAudioPlay += PlayAudioClip;
            
            busMusic.OnAudioPlay -= PlayMusic;
            busMusic.OnAudioStop -= StopMusic;
        }

        private void PlayMusic(AudioClipType clipType, AudioConfigSO settings)
        {
            var fadeDuration = 2f;
            var startTime = 0f;
            AudioClipSO clip = audioSkin.AudioDic[clipType];
            if (_musicEmitter != null && _musicEmitter.IsPlaying())
            {
                if(_musicEmitter.GetClip()==clip.Clip)
                    return;
                startTime = _musicEmitter.FadeMusicOut(fadeDuration);
            }

            _musicEmitter = pool.Request();
            _musicEmitter.FadeMusicIn(clip.Clip,settings,1f,startTime);
            _musicEmitter.OnFinishedPlaying += StopMusicEmitter;
        }
        
        private void PlayAudioClip(AudioClipType clipType, AudioConfigSO settings)
        {
            AudioClipSO clip = audioSkin.AudioDic[clipType];
            Debug.Log(clipType);
            SoundEmitter soundEmitter = pool.Request();
            if (soundEmitter != null)
            {
                soundEmitter.PlayAudioClip(clip.Clip,settings,clip.Loop);
                soundEmitter.OnFinishedPlaying += OnSoundEmitterFinished;
            }
        }
        private void StopMusic()
        {
            if (_musicEmitter != null && _musicEmitter.IsPlaying())
                _musicEmitter.Stop();
            
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
