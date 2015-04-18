using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

	float timer	= 10;
	
	void Update () {
		
		timer -= Time.deltaTime;
		
		if (timer < 0) {
			Application.LoadLevel("MainMenu");
		}
		
		if (Input.anyKey && timer < 7) {
			Application.LoadLevel("MainMenu");
		}
	}

}

// EOF