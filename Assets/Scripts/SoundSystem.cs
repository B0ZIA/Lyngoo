using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSystem : MonoBehaviour
{
    public SoundData effectData;
    public SoundData speakingData;

    [SerializeField]
    private Slider effectSoundSlider;
    [SerializeField]
    private Slider speakingSoundSlider;

    [SerializeField]
    private AudioSource buttonSource;
    [SerializeField]
    private AudioSource speakingSource;



    private void Awake()
    {
        LoadSoundData();
    }

    private void LoadSoundData()
    {
        effectSoundSlider.value = effectData.volume;
        speakingSoundSlider.value = speakingData.volume;
    }

    public void SaveSoundData()
    {
        effectData.volume = effectSoundSlider.value;
        speakingData.volume = speakingSoundSlider.value;
    }

    public void ButtonEffect_Play()
    {
        buttonSource.volume = effectSoundSlider.value;

        buttonSource.Play();
    }

    public void SpeakingEffect_Play()
    {
        speakingSource.volume = speakingSoundSlider.value;

        speakingSource.Play();
    }

    public void SpeakingEffect_Play(AudioClip clip)
    {
        speakingSource.clip = clip;
        SpeakingEffect_Play();
    }
}
