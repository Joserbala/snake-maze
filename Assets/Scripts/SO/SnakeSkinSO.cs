using System;
using System.Collections;
using System.Collections.Generic;
using SnakeMaze.Maze;
using SnakeMaze.Structs;
using UnityEngine;

namespace SnakeMaze.SO
{
    [CreateAssetMenu(fileName = "SnakeSkin", menuName="Scriptables/SnakeSkin")]
    public class SnakeSkinSO : ScriptableObject
    {
        [SerializeField] private Sprite bodyVertical;
        [SerializeField] private Sprite bodyHorizontal;
        [SerializeField] private Sprite bodyCorner;

        [SerializeField] private Sprite headVertical;
        [SerializeField] private Sprite headHorizontal;

        [SerializeField] private Sprite tailVertical;
        [SerializeField] private Sprite tailHorizontal;

        private SnakeSkin _snakeSkin = new SnakeSkin();

        public SnakeSkin SnakeSkin => _snakeSkin;

        private void OnEnable()
        {
            InitSnakeSkin();
        }

        public void InitSnakeSkin()
        {
            _snakeSkin.SetAllSprites(headVertical, headHorizontal,
                bodyVertical, bodyHorizontal, bodyCorner,
                tailVertical, tailHorizontal);
        }
    }
}