using System.Collections;
using UnityEngine;
using System;
using PrashantSingh.Utilities.Singleton;
using PrashantSingh.Utilities.Audio;

namespace Models
{
    [Serializable]
    public class SettingsManager : SerializableSingleton<SettingsManager>
    {
        #region Properties

        // Whether Music is enabled
        public bool MusicEnabled
        {
            get;
            private set;
        }

        // Whether SFX is enabled
        public bool SFXEnabled
        {
            get;
            private set;
        }

        // A volume multilayer for music
        public float MusicVolumeMultiplier
        { 
            get;
            private set;
        }

        // A volume multiplier for sfx
        public float SFXVolumeMultiplier
        {
            get;
            private set;
        }

        // The localizated language index
        public int LocalizedLanguage
        {
            get;
            private set;
        }

        #endregion

        #region Initialization

        // Initialization as instance of the class
        protected SettingsManager()
        {
            MusicEnabled = SFXEnabled = true;
            MusicVolumeMultiplier = SFXVolumeMultiplier = 1;
            LocalizedLanguage = -1;
            Save();
        }

        #endregion

        #region Method

        // Debug Only: Overriding ToString to return something meaningfull
        // A string representation of the object
        override public string ToString()
        {
            return string.Format("musicEnabled = {0}, sfxEnabled = {1}, musicVolumeMultiplier = {2}, sfxVolumeMultiplier = {3}, localizedLanguage = {4}", MusicEnabled, SFXEnabled, MusicVolumeMultiplier, SFXVolumeMultiplier, LocalizedLanguage);
        }

        // Setter for musicEnabled. Saves settings and update AudioManager
        public void SetMusicEnabled(bool enabled)
        {
            MusicEnabled = enabled;
            Save();
            AudioManager.instance.Toggle_backgroundMusic(enabled);
        }

        // Toggles whether the music is enabled
        public void ToggleMusicEnabled()
        {
            SetMusicEnabled(!MusicEnabled);
        }

        // Setter for sfxEnabled
        public void SetSFXEnabled(bool enabled)
        {
            SFXEnabled = enabled;
            Save();
        }

        // Toggle whether sfx is enabled
        public void ToggleSFXEnabled()
        {
            SetSFXEnabled(!SFXEnabled);
        }

        // sets the localized language
        public void SetLocalizedLanguage(int localizedLanguage)
        {
            this.LocalizedLanguage = localizedLanguage;
            Save();
        }

        #endregion

        #region Booleans

        // An enum of the SettingManger various boolean variables
        public enum BooleanVariable
        {
            MusicEnabled,
            SFXEnabled
        }

        // Gets the value of a Boolean Variable
        /// <param name = "variable"> The boolean variable</param>
        public bool GetBooleanVariableValue(BooleanVariable variable)
        {
            switch (variable)
            {
                case BooleanVariable.MusicEnabled:
                    return MusicEnabled;
                case BooleanVariable.SFXEnabled:
                    return SFXEnabled;
                default:
                    Debug.LogErrorFormat("{0} is not created in the switch Statement", variable);
                    return false;
            }
        }

        // Set a Boolean Variable to a given Value
        /// <param name = "variable"> The boolean variable</param>
        /// <param name = "value"> The boolean variable</param>
        public void SetBooleanVariableValue(BooleanVariable variable, bool value)
        {
            switch (variable)
            {
                case BooleanVariable.MusicEnabled:
                    SetMusicEnabled(value);
                    break;
                case BooleanVariable.SFXEnabled:
                    SetSFXEnabled(value);
                    break;
            }
        }

        // Toggles a Boolean Variable
        /// <param name = "variable"> The boolean variable</param>
        public void ToggleBooleanVariableValue(BooleanVariable variable)
        {
            switch (variable)
            {
                case BooleanVariable.MusicEnabled:
                    ToggleMusicEnabled();
                    break;
                case BooleanVariable.SFXEnabled:
                    ToggleSFXEnabled();
                    break;
            }
        }

        #endregion
    }
}