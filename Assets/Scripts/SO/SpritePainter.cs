using System.Collections;
using System.Collections.Generic;
using SnakeMaze.Enums;
using SnakeMaze.Maze;
using UnityEditor;
using UnityEngine;

namespace SnakeMaze
{
[CreateAssetMenu(fileName = "SpritePainter", menuName = "Scriptables/SpritePainter")]
    public class SpritePainter : ScriptableObject
    {
        [SerializeField] private List<GameObject> objects;

        public List<GameObject> Objects
        {
            get => objects;
            set => objects = value;
        }

        public void PaintObject(Vector2 initPos, Directions direction, int amount, Transform father)
        {
            for (int i = 0; i < amount; i++)
            {
                var item = objects[Random.Range(0, objects.Count)];
                var dir = DirectionsActions.DirectionsToVector2(direction);
                Instantiate(item, initPos+dir*i, Quaternion.identity, father); 
            }
        }
        public void PaintObject(Vector2 initPos, Directions direction, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var item = objects[Random.Range(0, objects.Count)];
                var dir=DirectionsActions.DirectionsToVector2(direction);
                Instantiate(item, initPos+dir*i, Quaternion.identity);
            }
        }
    }
}
