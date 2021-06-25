using UnityEngine;
using SnakeMaze.Audio;
using SnakeMaze.Interfaces;

namespace SnakeMaze.SO.Audio
{
    [CreateAssetMenu(fileName = "SoundEmitterPool", menuName = "Scriptables/Audio/SoundEmitterPoolSO")]
    public class SoundEmitterPoolSO : ComponentPoolSO<SoundEmitter>
    {
        [SerializeField] private SoundEmitterFactorySO _factory;

        public override IFactory<SoundEmitter> Factory
        {
            get => _factory;
            set => _factory = value as SoundEmitterFactorySO; 
        }
        public void ResetValues()
        {
            HasBeenPrewarmed = false;
        }
    }
}