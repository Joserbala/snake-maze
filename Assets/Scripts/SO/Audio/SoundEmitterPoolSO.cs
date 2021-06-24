using System;
using System.Collections.Generic;
using UnityEngine;
using SnakeMaze.Audio;

namespace SnakeMaze.SO.Audio
{
    [CreateAssetMenu(fileName = "SoundEmitterPool",menuName = "Scriptables/Audio/SoundEmitterPoolSO")]
    public class SoundEmitterPoolSO : ScriptableObject
    {
        [SerializeField] private SoundEmitter prefab;
        private Stack<SoundEmitter> _avalible = new Stack<SoundEmitter>();
        private bool _hasBeenPrewarmed;
        
        private Transform _root;
        private Transform _parent;

        public Transform Root
        {
            get
            {
                if (_root == null)
                {
                    _root = new GameObject(name).transform;
                    _root.SetParent(_parent);
                }
                return _root;
            }
        }

        public void SetParent(Transform value)
        {
            _parent = value;
            Root.SetParent(_parent);
        }

        public void PreWarm(int amount)
        {
            if (_hasBeenPrewarmed) return;

            for (int i = 0; i < amount; i++)
            {
                _avalible.Push(Create());
            }
            _hasBeenPrewarmed = true;
        }

        public SoundEmitter Create()
        {
            var member =  Instantiate(prefab,Root.transform);
            member.gameObject.SetActive(false);
            return member;
        }
        
        public SoundEmitter Request()
        {
            var member = _avalible.Count > 0 ? _avalible.Pop() : Create();
            if (member == null)
                member = Create();
            member.gameObject.SetActive(true);
            return member;
        }

        public void Return(SoundEmitter member)
        {
            member.transform.SetParent(Root.transform);
            member.gameObject.SetActive(false);
            _avalible.Push(member);
        }

        private void OnDisable()
        {
            _avalible.Clear();
            _hasBeenPrewarmed = false;
            if(_root!=null)
                Destroy(_root.gameObject);
        }
    }
}
