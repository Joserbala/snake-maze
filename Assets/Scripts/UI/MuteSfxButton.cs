using SnakeMaze.SO.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace SnakeMaze.UI
{
    public class MuteSfxButton : MonoBehaviour
    {
        [SerializeField] private BusAudioSO sfxBusAudioSo;
        [SerializeField] private Sprite muteSprite;
        [SerializeField] private Sprite unMuteSprite;

        private bool _isMutted;


        private Image _image;
        private Button _button;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            SetInitialSprite();
        }

        private void SetInitialSprite()
        {
            _isMutted = PlayerPrefs.GetInt("VolumeSFXGroup") == 1;
            SetSprite(_isMutted);
        }

        private void SetSprite(bool value)
        {
            var sprite = value ? muteSprite : unMuteSprite;
            _image.sprite = sprite;
        }

        public void SwitchMute()
        {
            _isMutted = !_isMutted;

            SetSprite(_isMutted);
            sfxBusAudioSo.MuteAudio?.Invoke(_isMutted);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(SwitchMute);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(SwitchMute);
        }
    }
}
