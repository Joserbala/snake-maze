using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeMaze.Structs
{
    public struct SnakeSkin
    {
        public SnakeBody Body;
        public SnakeHead Head;
        public SnakeTail Tail;

        public SnakeSkin(SnakeBody body, SnakeHead head, SnakeTail tail)
        {
            Body = body;
            Head = head;
            Tail = tail;
        }

        public void SetSkinProperties(SnakeBody body, SnakeHead head, SnakeTail tail)
        {
            Body = body;
            Head = head;
            Tail = tail;
        }

        public void SetBodySprites(Sprite vertical, Sprite horizontal, Sprite corner)
        {
            Body.SetAllSprites(vertical, horizontal, corner);
        }

        public void SetHeadSprites(Sprite vertical, Sprite horizontal)
        {
            Head.SetAllSprites(vertical, horizontal);
        }

        public void SetTailSprites(Sprite vertical, Sprite horizontal)
        {
            Tail.SetAllSprites(vertical, horizontal);
        }

        public void SetAllSprites(Sprite headVertical, Sprite headHorizontal,
            Sprite bodyVertical, Sprite bodyHorizontal, Sprite bodyCorner,
            Sprite tailVertical, Sprite tailHorizontal)
        {
            SetHeadSprites(headVertical, headHorizontal);
            SetBodySprites(bodyVertical, bodyHorizontal, bodyCorner);
            SetTailSprites(tailVertical, tailHorizontal);
        }
    }

    public struct SnakeBody
    {
        public Sprite Vertical;
        public Sprite Horizontal;
        public Sprite Corner;

        public SnakeBody(Sprite vertical, Sprite horizontal, Sprite corner)
        {
            Vertical = vertical;
            Horizontal = horizontal;
            Corner = corner;
        }

        public void SetAllSprites(Sprite vertical, Sprite horizontal, Sprite corner)
        {
            Vertical = vertical;
            Horizontal = horizontal;
            Corner = corner;
        }
    }

    public struct SnakeHead
    {
        public Sprite Vertical;
        public Sprite Horizontal;

        public SnakeHead(Sprite vertical, Sprite horizontal)
        {
            Vertical = vertical;
            Horizontal = horizontal;
        }

        public void SetAllSprites(Sprite vertical, Sprite horizontal)
        {
            Vertical = vertical;
            Horizontal = horizontal;
        }
    }

    public struct SnakeTail
    {
        public Sprite Vertical;
        public Sprite Horizontal;

        public SnakeTail(Sprite vertical, Sprite horizontal)
        {
            Vertical = vertical;
            Horizontal = horizontal;
        }

        public void SetAllSprites(Sprite vertical, Sprite horizontal)
        {
            Vertical = vertical;
            Horizontal = horizontal;
        }
    }
}