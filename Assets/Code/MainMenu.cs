using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    private float soundVol;
    private int soundTog;

    private void Start() {
        // Zero the Listener while we figure out soundSettings
        AudioListener.volume = 0;

        if (PlayerPrefs.HasKey("Sound")) {
            soundVol = PlayerPrefs.GetFloat("Sound");
        } else {
            soundVol = 1;
            PlayerPrefs.SetFloat("Sound", soundVol);
        }

        if (PlayerPrefs.HasKey("SoundTog")) {
            soundTog = PlayerPrefs.GetInt("SoundTog");
        } else {
            soundTog = 1;
            PlayerPrefs.SetInt("SoundTog", soundTog);
        }

        // Restore values in UI
        var temp = GameObject.Find("SoundSlider");
        if (temp != null) {
            var tempSlide = temp.GetComponent<Slider>();
            if (tempSlide != null) {
                tempSlide.value = soundVol;
            }
        }

        temp = GameObject.Find("SoundToggle");
        if (temp != null) {
            Toggle tempTog = temp.GetComponent<Toggle>();
            if (tempTog != null) {
                if (soundTog > 0) {
                    tempTog.isOn = true;
                } else {
                    tempTog.isOn = false;
                }
            }
        }
    }

    void Update() {
        // Set Volume un/mute
        if (soundTog == 1) {
            AudioListener.volume = soundVol;
        } else {
            AudioListener.volume = 0;
        }
    }
}

// EOF