using System.Collections;
using UnityEngine;
using PrashantSingh.Utilities.Serialization;

namespace Utilities
{
    // Here the projects PlayerPreferences key are declared and initialized
    public static class PlayerPreferencesKeys // PlayerPreferences.Keys
    {
        // Whether the game was Previously launched
        public const string perviouslyLaunched = "previouslyLaunched";

        // The player 1 player type
        public const string player1 = "player1";
        // The player 2 player type
        public const string player2 = "player2";

        //Initializes required key-value pairs to their Initial Values
        public static void Initialize()
        {
            PlayerPreferences.SetBool(perviouslyLaunched, true);
            PlayerPreferences.SetInt(player1, 0);
            PlayerPreferences.SetInt(player2, 0);
        }
       
    }
}