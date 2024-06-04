using System.Collections;
using UnityEngine;
using PrashantSingh.Utilities.Serialization;

namespace Utilities
{
    public static class DataManager 
    {
        // Initializes the project data file
        public static void Initalize()
        {
            PlayerPreferencesKeys.Initialize();
        }

        // Deletes the projects data file
        public static void Delete()
        {
            PlayerPreferences.DeleteAll();
        }

        // Verifies the projects data file, re-creating them if necessary
        public static void Verify()
        {

        }

        // Reloads the projects data file
        public static void ReloadData()
        {
            Delete();
            Initalize();
        }
    }
}