using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider slider;

    private float corruption;

    /*
    TO DO:
    - mix light and dark according to slider of corruption value
    - master volume (slider and mute button)
    */

    private void Awake() {
        corruption = 0.001f;
        slider.onValueChanged.AddListener(GetValue);
    }

    private void Start() {
        mixer.SetFloat("MasterVolume", 16f);
    }

    private void Update() {
        CorruptionMix();
    }

    private void CorruptionMix() {

        mixer.SetFloat("DarkVolume", Mathf.Log10(corruption)*30f);
        mixer.SetFloat("LightVolume", Mathf.Log10(1-corruption)*30f);

        // mixer.SetFloat("DarkVolume", (Mathf.Sin(-1*Mathf.PI*(corruption+0.5f))-1)*20f);
        // mixer.SetFloat("LightVolume", (Mathf.Sin(Mathf.PI*(corruption+0.5f))-1)*20f);
    }

    private void GetValue(float value){
        corruption = value; //from 0 (pure) to 1 (corrupt)

        if (corruption == 0){
            corruption += 0.001f; //since taking log of corruption later
        }

        if (corruption == 1){
            corruption -= 0.01f;
        }
    }

}
