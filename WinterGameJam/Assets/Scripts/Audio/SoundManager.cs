using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance;

	private Dictionary<string, AudioClip> audioClips;

	private List<AudioSource> audioSourcePool = new List<AudioSource>();
	public int poolSize = 10;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
			LoadAudioClips();
			InitializeAudioSourcePool();
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
			if (!audioClips.ContainsKey(clip.name))
			{
				audioClips.Add(clip.name, clip);
				Debug.Log("Загружен аудиоклип: " + clip.name);
			}
			else
			{
				Debug.LogWarning("Дублирующийся аудиоклип: " + clip.name);
			}
		}
	}

	void InitializeAudioSourcePool()
	{
		for (int i = 0; i < poolSize; i++)
		{
			GameObject audioObj = new GameObject("PooledAudioSource_" + i);
			audioObj.transform.parent = this.transform;
			AudioSource source = audioObj.AddComponent<AudioSource>();
			source.playOnAwake = false;
			source.spatialBlend = 1.0f;
			audioSourcePool.Add(source);
		}
	}

	AudioSource GetAvailableAudioSource()
	{
		foreach (var source in audioSourcePool)
		{
			if (!source.isPlaying)
			{
				return source;
			}
		}

		GameObject audioObj = new GameObject("PooledAudioSource_Extra");
		audioObj.transform.parent = this.transform;
		AudioSource newSource = audioObj.AddComponent<AudioSource>();
		newSource.playOnAwake = false;
		newSource.spatialBlend = 1.0f;
		audioSourcePool.Add(newSource);
		return newSource;
	}

	public void PlaySound(string clipName, Vector3 position)
	{
		if (audioClips.ContainsKey(clipName))
		{
			AudioClip clip = audioClips[clipName];
			AudioSource source = GetAvailableAudioSource();
			source.transform.position = position;
			source.clip = clip;
			source.Play();
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
	public void PlayFootstepSound(Vector3 position)
	{
		string[] pickupSounds = { "Step1", "Step2", "Step3", "Step4", "Step5", };
		int randomIndex = Random.Range(0, pickupSounds.Length);
		PlaySound(pickupSounds[randomIndex], position);
	}

	public void PlayFootstep(string clipName, Vector3 position)
	{
		if (audioClips.ContainsKey(clipName))
		{
			PlaySound(clipName, position);
		}
		else
		{
			Debug.LogWarning("Аудиоклип шага с именем " + clipName + " не найден!");
		}
	}

	public void PlayLongFootstep(string clipName, Vector3 position)
	{
		if (audioClips.ContainsKey(clipName))
		{
			PlaySound(clipName, position);
		}
		else
		{
			Debug.LogWarning("Аудиоклип длинного шага с именем " + clipName + " не найден!");
		}
	}
}

