using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using PrashantSingh.Utilities.Singleton;
using Models;


namespace PrashantSingh.Utilities.Audio
{
    /// <summary>An AudioManager which supports the playing of audio files by filename.</summary>
    public class AudioManager : MonoSingleton<AudioManager>
    {
		/// <summary>A music audio database.</summary>
		[Tooltip("A music audio database.")]
		[SerializeField] private AudioDatabase _musicDatabase;
		/// <summary>A sound effects audio database.</summary>
		[Tooltip("A sound effects audio database.")]
		[SerializeField] private AudioDatabase _sfxDatabase;
		/// <summary>An AudioSource used to play background music.</summary>
		private AudioSource _background;
		/// <summary>An AudioSource used to play sfx.</summary>
		private AudioSource _sfx;

#if UNITY_EDITOR
		/// <summary>EDITOR ONLY: An array of the database keys.</summary>
		public string[] keys
		{
			get
			{
				Assert.IsNotNull(_musicDatabase); Assert.IsNotNull(_sfxDatabase);
				return (_musicDatabase.keys.Union(_sfxDatabase.keys)).ToArray();
			}
		}
#endif

		/// <summary>Callback when the instance is started.</summary>
		private void Start()
		{
			//get references to the _background and sfx players
			AudioSource[] audioSources = GetComponents<AudioSource>();
			Assert.IsTrue(audioSources.Length == 2);
			_background = audioSources[0]; _sfx = audioSources[1];
		}

		/// <summary>Play a SFX file by key on the SFX AudioSource.</summary>
		public void PlayMusic(AudioManagerKeys key)
		{
			PlayMusic(key.ToString());
		}

		/// <summary>Play a music file by key on the _background AudioSource.</summary>
		public void PlayMusic(string key)
		{
			if (SettingsManager.instance.MusicEnabled)
			{
				AudioDatabase.AudioFile audioFile = _musicDatabase.GetAudioFileForKey(key);
				Assert.IsNotNull(audioFile);

				if (_background.clip == audioFile.clip) { return; } //no need to load
				_background.volume = audioFile.volume * SettingsManager.instance.MusicVolumeMultiplier;
				_background.clip = audioFile.clip;
				_background.loop = true;
				_background.Play();
			}
		}

		/// <summary>Play a SFX file by key on the SFX AudioSource.</summary>
		public void PlaySFX(AudioManagerKeys key)
		{
			PlaySFX(key.ToString());
		}

		/// <summary>Play a SFX file by name on the SFX AudioSource.</summary>
		public void PlaySFX(string filename)
		{
			if (SettingsManager.instance.SFXEnabled)
			{
				AudioDatabase.AudioFile audioFile = _sfxDatabase.GetAudioFileForKey(filename);
				Assert.IsNotNull(audioFile);

				_sfx.volume = audioFile.volume * SettingsManager.instance.SFXVolumeMultiplier;
				_sfx.PlayOneShot(audioFile.clip);
			}
		}

		/// <summary>Toggles the _background music.</summary>
		public void Toggle_backgroundMusic(bool shouldEnabled)
		{
			if (!_background.isPlaying && shouldEnabled)
			{
				_background.Play();
			}
			else if (_background.isPlaying && !shouldEnabled)
			{
				_background.Stop();
			}
		}

		/// <summary>Stops any audio on SFX and voice AudioSources.</summary>
		public void StopSFX()
		{
			if (_sfx.isPlaying) { _sfx.Stop(); }
		}
	}
}