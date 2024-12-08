using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance;

	private Dictionary<string, AudioClip> audioClips;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
			LoadAudioClips();
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void LoadAudioClips()
	{
		AudioClip[] clips = Resources.LoadAll<AudioClip>("Audio");
		audioClips = new Dictionary<string, AudioClip>();
		foreach (AudioClip clip in clips)
		{
			audioClips.Add(clip.name, clip);
			Debug.Log("Загружен аудиоклип: " + clip.name);
		}
	}

	public void PlaySound(string clipName)
	{
		if (audioClips.ContainsKey(clipName))
		{
			Debug.LogWarning("Централизованное воспроизведение звуков не реализовано.");
		}
		else
		{
			Debug.LogWarning("Аудиоклип с именем " + clipName + " не найден!");
		}
	}

	public void PlaySound(string clipName, Vector3 position)
	{
		if (audioClips.ContainsKey(clipName))
		{
			AudioClip clip = audioClips[clipName];
			AudioSource.PlayClipAtPoint(clip, position);
		}
		else
		{
			Debug.LogWarning("Аудиоклип с именем " + clipName + " не найден!");
		}
	}

	public void PlayPickupSound(Vector3 position)
	{
		string[] pickupSounds = { "ПоднятияПредмета1", "ПоднятияПредмета2" };
		int randomIndex = Random.Range(0, pickupSounds.Length);
		PlaySound(pickupSounds[randomIndex], position);
	}
}
