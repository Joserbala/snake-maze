using System.Collections.Generic;
using SnakeMaze.Utils;

namespace SnakeMaze.Enums
{
    public enum MazeSkinEnum 
    {
        Default, Space, Skin3
    }

    public enum SnakeSkinEnum
    {
        Default, Astronaut,  Skin3
    }

    public enum AudioSkinEnum
    {
        Default
    }

    public static class SkinEnumUtils
    {
        public static SnakeSkinEnum StringToSnakeEnumById(string value)
        {
            SnakeSkinEnum skin;
            switch (value)
            {
                case Constants.AstronautSnakeSkin:
                    skin = SnakeSkinEnum.Astronaut;
                    break;
                case Constants.Skin3SnakeSkin:
                    skin = SnakeSkinEnum.Skin3;
                    break;
                default: skin = SnakeSkinEnum.Default;
                    break;
            }

            return skin;
        }
        public static SnakeSkinEnum StringToSnakeEnum(string value)
        {
            SnakeSkinEnum skin;
            switch (value)
            {
                case "Astronaut":
                    skin = SnakeSkinEnum.Astronaut;
                    break;
                case "Skin3":
                    skin = SnakeSkinEnum.Skin3;
                    break;
                default: skin = SnakeSkinEnum.Default;
                    break;
            }

            return skin;
        }
        public static MazeSkinEnum StringToMazeEnumById(string value)
        {
            MazeSkinEnum skin;
            switch (value)
            {
                case Constants.SpaceMazeSkin:
                    skin = MazeSkinEnum.Space;
                    break;
                case Constants.Skin3MazeSkin:
                    skin = MazeSkinEnum.Skin3;
                    break;
                default: skin = MazeSkinEnum.Default;
                    break;
            }

            return skin;
        }
        public static MazeSkinEnum StringToMazeEnum(string value)
        {
            MazeSkinEnum skin;
            switch (value)
            {
                case "Space":
                    skin = MazeSkinEnum.Space;
                    break;
                case "Skin3":
                    skin = MazeSkinEnum.Skin3;
                    break;
                default: skin = MazeSkinEnum.Default;
                    break;
            }

            return skin;
        }
    }
}
