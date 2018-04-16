using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {
    private float _timer = 10;

    void Update() {
        _timer -= Time.deltaTime;

        if (_timer < 0) {
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.anyKey && _timer < 7) {
            SceneManager.LoadScene("MainMenu");
        }
    }
}