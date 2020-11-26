using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource trackSource = null;
    [SerializeField] private AudioSource sfxSource = null;
    [SerializeField] private AudioClip Track = null;
    public Slider slider = null;

    private float musicVolume = 1.0f;

    public static AudioController instance = null;

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        trackSource.clip = Track;
        trackSource.Play();
        musicVolume = PlayerPrefs.GetFloat("volume");
        trackSource.volume = musicVolume;
        slider.value = musicVolume;
    }

    void Update()
    {
        trackSource.volume = musicVolume;
        PlayerPrefs.SetFloat("volume", musicVolume);
    }

    private void Initialize()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }

    public void PlaySound(AudioClip clip)
    {
        sfxSource.volume = musicVolume;
        sfxSource.PlayOneShot(clip);
    }
}
