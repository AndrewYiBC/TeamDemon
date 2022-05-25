using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioMixer mixer;
    //[SerializeField] Slider slider;
    [SerializeField] GameObject player;


    private PlayerControls playerController;
    private float corruption;
    private float CDDuration;
    private bool isCorrupt;

    private void Awake() {
        corruption = 0.001f;
        //slider.onValueChanged.AddListener(GetValue);

        playerController = player.GetComponent<PlayerControls>();
        CDDuration = playerController.getCD();
        isCorrupt = playerController.getForm();
    }

    void OnEnable(){
        PlayerControls.OnTransform += Transform;
    }
    
    void OnDisable(){
        PlayerControls.OnTransform -= Transform;
    }

    private void Start() {
        mixer.SetFloat("MasterVolume", 16f);
        CorruptionMix(corruption);
    }


    //will be called everytime a transformation starts
    void Transform(){
        isCorrupt = playerController.getForm();

        if (isCorrupt){
            corruption = 0.99f;
        } else {
            corruption = 0.01f;
        }

        StartCoroutine(GradientSound());
    }

    //coroutine for gradiating the sound
    IEnumerator GradientSound(){
        
        float timeInCD = 0f;
        float currVol =  1-corruption;

        while (timeInCD < CDDuration){
            timeInCD += Time.deltaTime;
            float newVol = Mathf.Lerp(currVol, corruption, timeInCD / CDDuration);
            CorruptionMix(newVol);
            yield return null;
        }
        yield break;
    }

    private void CorruptionMix(float targetVol) {
        mixer.SetFloat("DarkVolume", Mathf.Log10(targetVol)*30f);
        mixer.SetFloat("LightVolume", Mathf.Log10(1-targetVol)*30f);
    }

}
