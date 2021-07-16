using System.Collections.Generic;
using SnakeMaze.Enums;
using SnakeMaze.SO.Audio;
using UnityEngine;

namespace SnakeMaze.SO
{
    [CreateAssetMenu(fileName = "SkinContainer", menuName = "Scriptables/SkinContainerSO")]
    public class SkinContainerSO : InitiableSO
    {
        [SerializeField] private MazeSkinSO defaultMazeSkin;
        [SerializeField] private MazeSkinSO mockupMazeSkin;
        [SerializeField] private SnakeSkinSO defaultSnakeSkin;
        [SerializeField] private AudioSkinSO defaultAudioSkin;

        private Dictionary<MazeSkins, MazeSkinSO> _mazeSkinDic;
        private Dictionary<SnakeSkins, SnakeSkinSO> _snakeSkinDic;
        private Dictionary<AudioSkins, AudioSkinSO> _audioSkinDic;

        public MazeSkinSO CurrentMazeSkin { get; set; }
        public SnakeSkinSO CurrentSnakeSkin { get; set; }
        public AudioSkinSO CurrentAudioSkin { get; set; }

        public Dictionary<MazeSkins, MazeSkinSO> MazeSkinDic => _mazeSkinDic;
        public Dictionary<SnakeSkins, SnakeSkinSO> SnakeSkinDic => _snakeSkinDic;
        public Dictionary<AudioSkins, AudioSkinSO> AudioSkinDic => _audioSkinDic;

        public override void InitScriptable()
        {
            InitDics();
            CurrentMazeSkin = _mazeSkinDic[MazeSkins.Default];
            CurrentSnakeSkin = _snakeSkinDic[SnakeSkins.Default];
            CurrentAudioSkin = _audioSkinDic[AudioSkins.Default];
            InitCurrentSkin();
        }
        
        private void InitDics()
        {
            _mazeSkinDic = new Dictionary<MazeSkins, MazeSkinSO>()
            {
                {MazeSkins.Default, defaultMazeSkin},
                {MazeSkins.Mockup, mockupMazeSkin}
            };
            _snakeSkinDic = new Dictionary<SnakeSkins, SnakeSkinSO>()
            {
                {SnakeSkins.Default, defaultSnakeSkin}
            };
            _audioSkinDic = new Dictionary<AudioSkins, AudioSkinSO>()
            {
                {AudioSkins.Default, defaultAudioSkin}
            };
        }

        private void InitCurrentSkin()
        {
            CurrentAudioSkin.InitScriptable();
            CurrentMazeSkin.InitScriptable();
            CurrentSnakeSkin.InitScriptable();
        }
        
        //TODO: Eventos
        public void ChangeMazeSkin(MazeSkins newSkin)
        {
            CurrentMazeSkin = _mazeSkinDic[newSkin];
            CurrentMazeSkin.InitScriptable();
        }
        public void ChangeSnakeSkin(SnakeSkins newSkin)
        {
            CurrentSnakeSkin = _snakeSkinDic[newSkin];
            CurrentSnakeSkin.InitScriptable();
        }
        public void ChangeAudioSkin(AudioSkins newSkin)
        {
            CurrentAudioSkin = _audioSkinDic[newSkin];
            CurrentAudioSkin.InitScriptable();
        }

    }
}