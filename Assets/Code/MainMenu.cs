using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	
	float soundVol;
	int soundTog;



	void soundLibCreate () {

		//Object[] sounds = Resources.FindObjectsOfTypeAll<AudioClipLoadType;
		Object[] sounds = Resources.LoadAll<AudioClip> ("Audio");

		foreach (Object sound in sounds) {
			//Debug.Log(sound.name);
		}

		Debug.Log (sounds.Length);
	}


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

	public void VolumeTog ( bool active ) {
		if (active == true) {
			soundTog = 1;
		} else {
			soundTog = 0;
		}

		PlayerPrefs.SetInt("SoundTog", soundTog);
	}

	void Start () {
		// Start SoundLib
		this.soundLibCreate ();


		// Zero the Listener while we figure out soundSettings
		AudioListener.volume = 0;

		if (PlayerPrefs.HasKey ("Sound")) {
			soundVol = PlayerPrefs.GetFloat ("Sound");
		} else {
			soundVol = 1;
			PlayerPrefs.SetFloat ("Sound", soundVol);
		}

		if (PlayerPrefs.HasKey ("SoundTog")) {
			soundTog = PlayerPrefs.GetInt ("SoundTog");
		} else {
			soundTog = 1;
			PlayerPrefs.SetInt ("SoundTog", soundTog);
		}

		// Restore values in UI
		GameObject temp = GameObject.Find ("SoundSlider");
		if (temp != null) {
			Slider tempSlide = temp.GetComponent<Slider> ();
			if (tempSlide != null) {
				tempSlide.value = soundVol;
			}
		}

		temp = GameObject.Find ("SoundToggle");
		if (temp != null) {
			Toggle tempTog = temp.GetComponent<Toggle> ();
			if (tempTog != null) {
				if (soundTog > 0) {
					tempTog.isOn = true;
				} else {
					tempTog.isOn = false;
				}
			}
		}
	}

	void Update () {
		// Set Volume un/mute
		if ( soundTog == 1 ) {
			AudioListener.volume = soundVol;
		} else {
			AudioListener.volume = 0;
		}
	}
}

// EOF