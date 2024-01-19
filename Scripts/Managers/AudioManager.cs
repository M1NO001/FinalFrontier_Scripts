using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum SoundType
{
    MainSceneTheme,
    ButtonSound,
    EndBtnSound,
    SettingSound,
    InventorySound,
    PlaySceneTheme,
    CreditSceneTheme
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    private const string SoundNotFoundMessage = "Sound not Found";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }

    public void PlayMusic(SoundType type)
    {
        StopMusic();
        Sound s = BinarySearch(musicSounds, type.ToString());

        if (s == null)
        {
            Debug.Log(SoundNotFoundMessage);
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.loop = true;
            musicSource.Play();           
        }
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }
    public void PlaySFX(SoundType type)
    {
        Sound s = BinarySearch(sfxSounds, type.ToString());
        if (s == null)
        {
            Debug.Log(SoundNotFoundMessage);
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    private Sound BinarySearch(Sound[] array, string name)
    {
        Array.Sort(array, (x, y) => x.name.CompareTo(y.name));

        int left = 0;
        int right = array.Length - 1;

        while (left <= right)
        {
            int middle = (left + right) / 2;

            int comparisonResult = array[middle].name.CompareTo(name);

            if (comparisonResult == 0)
            {
                return array[middle];
            }
            else if (comparisonResult < 0)
            {
                left = middle + 1;
            }
            else
            {
                right = middle - 1;
            }
        }
        return null;
    }
}
