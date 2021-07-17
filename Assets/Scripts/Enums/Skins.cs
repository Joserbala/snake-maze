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
        public static SnakeSkinEnum StringToSnakeEnum(string value)
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
        public static MazeSkinEnum StringToMazeEnum(string value)
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
    }
}
