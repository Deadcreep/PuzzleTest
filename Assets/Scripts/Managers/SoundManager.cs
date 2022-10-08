using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	private const string PREFS_NAME = "VolumeValue";
	public float Volume { get => _audioSource.volume; private set => _audioSource.volume = value; }
	[SerializeField] private AudioSource _audioSource;

	public event Action VolumeChanged;

	private void Awake()
	{
		if (PlayerPrefs.HasKey(PREFS_NAME))
		{
			Volume = PlayerPrefs.GetFloat(PREFS_NAME);
		}
	}

	public void SetVolume(float value)
	{
		_audioSource.volume = value;
		PlayerPrefs.SetFloat(PREFS_NAME, value);
		VolumeChanged?.Invoke();
	}
}