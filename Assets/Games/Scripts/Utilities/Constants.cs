using System.Collections;
using UnityEngine;

namespace Utilities
{
    // A struct of game Constants
    public class Constants 
    {
        // The number of pieces types
        public const int NUMBER_TYPE_PIECES = 6;
    }

    // A struct of animation duration constants
    public struct AnimationDuration
    {
        // The duration of the loading Scene
        public const float LOADING_SCENE = 3.5f;
    }

    // A struct of game store variable
    public struct GameStore
    {

    }

    // A struct of scene build indeces
    public struct SceneBuildIndeces
    {
        // The loading Scene
        public const int LoadingScene = 0;
        // The menu Scene
        public const int MenuScene = 1;
        // The Game Scene
        public const int GameScene = 2;

    }

    // A struct of tags
    public struct Tags
    {

    }

    // A struct of colors used in the game
    public struct GameColors
    {
        // A light Gray color used as the lighter color for sqaures on the chess board
        public static Color cellLight
        {
            get
            {
                return new Color(0.8f, 0.8f, 0.8f);
            }
        }

        // A Dark Gray color used as the darker color for sqaures on the chess board
        public static Color cellDark
        {
            get
            {
                return new Color(0.6f, 0.6f, 0.6f);
            }
        }

        // A Green color used to highlight possible moves
        public static Color cellHightLight
        {
            get
            {
                return new Color(0.2f, 0.8f, 0.2f);
            }
        }

        // A red color used when there is no possible moves
        public static Color cellNoMoves
        {
            get
            {
                return new Color(0.8f, 0.2f, 0.2f);
            }
        }
    }
}