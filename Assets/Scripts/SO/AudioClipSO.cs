using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeMaze
{
    public class AudioClipSO : MonoBehaviour
    {
        [SerializeField] private AudioClip clip;
        [SerializeField] private bool loop;
        [SerializeField] private bool ignorePause;

        public bool IgnorePuase
        {
            get => ignorePause;
            set => ignorePause = value;
        }

        public bool Loop
        {
            get => loop;
            set => loop = value;
        }
        public AudioClip Clip
        {
            get => clip;
            set => clip = value;
        }
        


    }
}
