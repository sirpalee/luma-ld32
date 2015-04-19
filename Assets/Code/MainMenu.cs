using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	float musicVol;
	float soundVol;
	int musicTog;
	int soundTog;

	public void ChangeScene (int sceneInt) {
		Application.LoadLevel (sceneInt);
	}

	public void ExitGame () {
		Application.Quit ();
	}

	public void VolumeSlider ( float slider ) {
		soundVol = slider;
		PlayerPrefs.SetFloat("Sound", soundVol);
	}

	public void MusicSlider ( float slider ) {
		musicVol = slider;
		PlayerPrefs.SetFloat("Music", musicVol);
	}

	public void VolumeTog ( bool active ) {
		if (active == true) {
			soundTog = 1;
		} else {
			soundTog = 0;
		}

		PlayerPrefs.SetInt("SoundTog", soundTog);
	}

	public void MusicTog ( bool active ) {
		if (active == true) {
			musicTog = 1;
		} else {
			musicTog = 0;
		}

		PlayerPrefs.SetInt("MusicTog", musicTog);
	}

	void Start () {
		if (PlayerPrefs.HasKey ("Music")) {
			musicVol = PlayerPrefs.GetFloat("Music");
		} else {
			musicVol = 1;
			PlayerPrefs.SetFloat("Music", musicVol);
		}

		if (PlayerPrefs.HasKey ("Sound")) {
			soundVol = PlayerPrefs.GetFloat("Sound");
		} else {
			soundVol = 1;
			PlayerPrefs.SetFloat("Sound", soundVol);
		}

		if (PlayerPrefs.HasKey ("SoundTog")) {
			soundTog = PlayerPrefs.GetInt("SoundTog");
		} else {
			soundTog = 1;
			PlayerPrefs.SetInt("SoundTog", soundTog);
		}

		if (PlayerPrefs.HasKey ("MusicTog")) {
			musicTog = PlayerPrefs.GetInt("MusicTog");
		} else {
			musicTog = 1;
			PlayerPrefs.SetInt("MusicTog", musicTog);
		}

		// Restore values in UI
		GameObject temp = GameObject.Find ("SoundSlider");
		if (temp != null) {
			Slider tempSlide = temp.GetComponent<Slider>();
			if(tempSlide != null) {
				tempSlide.value = soundVol;
			}
		}

		temp = GameObject.Find ("MusicSlider");
		if (temp != null) {
			Slider tempSlide = temp.GetComponent<Slider>();
			if(tempSlide != null) {
				tempSlide.value = musicVol;
			}
		}

		temp = GameObject.Find ("SoundToggle");
		if (temp != null) {
			Toggle tempTog = temp.GetComponent<Toggle>();
			if(tempTog != null) {
				if( soundTog > 0) {
					tempTog.isOn = true;
				} else {
					tempTog.isOn = false;
				}
			}
		}

		temp = GameObject.Find ("MusicToggle");
		if (temp != null) {
			Toggle tempTog = temp.GetComponent<Toggle>();
			if(tempTog != null) {
				if( musicTog > 0) {
					tempTog.isOn = true;
				} else {
					tempTog.isOn = false;
				}
			}
		}
	}

	void Update () {
		//Debug.Log ("Music: " + musicVol);
		//Debug.Log ("Sound: " + soundVol);
	}
}

// EOF