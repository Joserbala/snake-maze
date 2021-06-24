using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeMaze.SO
{
    [CreateAssetMenu(fileName = "AudioSkin",menuName = "Scriptables/Audio/AudioSkin")]
    public class AudioSkinSO : ScriptableObject
    {
        [SerializeField] private AudioClipSO gameMusic;

        [SerializeField] private AudioClipSO menuMusic;

        [SerializeField] private AudioClipSO boostIn;

        [SerializeField] private AudioClipSO boostOut;

        [SerializeField] private AudioClipSO death;

        [SerializeField] private AudioClipSO eat;

        [SerializeField] private AudioClipSO tapUi;

        #region PROPERTIES

        public AudioClipSO GameMusic
        {
            get => gameMusic;
            set => gameMusic = value;
        }

        public AudioClipSO MenuMusic
        {
            get => menuMusic;
            set => menuMusic = value;
        }

        public AudioClipSO BoostIn
        {
            get => boostIn;
            set => boostIn = value;
        }

        public AudioClipSO BoostOut
        {
            get => boostOut;
            set => boostOut = value;
        }

        public AudioClipSO Death
        {
            get => death;
            set=> death=value;
        }

        public AudioClipSO Eat
        {
            get => eat;
            set => eat = value;
        }

        public AudioClipSO TapUi
        {
            get => tapUi;
            set => tapUi = value;
        }

        #endregion
        
    }
}
