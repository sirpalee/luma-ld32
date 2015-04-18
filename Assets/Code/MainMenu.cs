using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public void ChangeScene (int sceneInt) {
		Application.LoadLevel (sceneInt);
	}

	public void ExitGame () {
		Application.Quit ();
	}
}

// EOF