using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
public class VoiceManager : MonoBehaviour
{
    public AudioMixerGroup MixerGroupMaster, MixerGroupMicrophone;
    public float loudness = 0;
    public int loudnessMax;
    public float _sensitivity = 100;
    public AudioSource _audio;
    public Slider slider;
    public Image Image;
    void Start()
    {
     
        _audio.clip = Microphone.Start(null, true, 10, 44100);
        _audio.loop = true;
        _audio.mute = false;
        while (!(Microphone.GetPosition(null) > 0))
        {
            _audio.Play();
        }
      
    }

    // Update is called once per frame
   
    void Update()
    {
        if (loudness > 0)

            _audio.outputAudioMixerGroup = MixerGroupMicrophone;

        else

            _audio.outputAudioMixerGroup = MixerGroupMaster;

        loudness = GetAveragedVolume() * _sensitivity;
        Image.fillAmount = loudness / 100;
       
    }
    float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        _audio.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }

    public void SetSound(float soundAmount)
    {
        PlayerPrefs.SetFloat("Loud", soundAmount);
    }
   
}
