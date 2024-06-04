using System.Collections;
using UnityEngine;
using PrashantSingh.Utilities.Singleton;
using System;

namespace Models
{
    [Serializable]
    public class PlayerManager : SerializableSingleton<PlayerManager>
    {
        #region Initializations

        // Initializations an instance of the class
        protected PlayerManager()
        {
            Save();
        }

        #endregion
    }
}