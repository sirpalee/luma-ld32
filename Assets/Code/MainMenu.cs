using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text.RegularExpressions;

public class MainMenu : MonoBehaviour {

	float musicVol;
	float soundVol;
	int musicTog;
	int soundTog;

	String[] soundLibrary = new String[50];

	public void soundLibUpdate(string path) {

		//for (i=0; i<2; i++) {
		//	audios[i] = Resources.Load(soundfiles[i]);
		//}

		DirectoryInfo info = new DirectoryInfo (Application.dataPath+"/"+path);
		FileInfo[] fileEntries = info.GetFiles ();

		int fileCount = 0;

		foreach (FileInfo soundFile in fileEntries) {

			Match match = Regex.Match( soundFile.FullName , @"\.meta$", RegexOptions.IgnoreCase);
			if (match.Success)
				continue;

			if(soundFile.Name.ToString() != null )
				soundLibrary[fileCount] = soundFile.Name;

			if( soundFile.Name == "charcterHit" ) {
				Debug.Log("Here");
			}



			//match = Regex.Match( soundFile.FullName , @".*$", RegexOptions.IgnoreCase);
			//string[] fileParts = Regex.Split(soundFile.FullName,'\\'+Path.DirectorySeparatorChar.ToString());
			//foreach( string file in fileParts ) {
			//	Debug.Log(file);
			//}

			// DEBUG
			soundLibrary [fileCount] = Regex.Replace (soundLibrary [fileCount], @"\..*$", @"");
			AudioClip sound = Resources.Load("Resources/Sounds/"+soundLibrary[fileCount], typeof(AudioClip)) as AudioClip;
			if (sound != null)
				Debug.Log ("Here: "+sound.length);

			//Debug.Log ("Sounds/"+soundLibrary[fileCount]);


			fileCount++;

			//Debug.Log ("Sounds/"+soundLibrary[10]);
			//Debug.Log(soundLibrary.Length);
			//Debug.Log(soundFile.Name.ToString()  );
		}





		// DEBUG
		//Debug.Log(fileCount);

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
		// Update Sound Library
		this.soundLibUpdate ("Sounds");

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