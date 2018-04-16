using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeartDisplay : MonoBehaviour {
    public Sprite[] heartTextures;

    private Image[] _images;
    private int _lastDisplayedHealth;

    // Use this for initialization
    private void Start() {
        _images = gameObject.GetComponentsInChildren<Image>();
        _lastDisplayedHealth = -1;
    }

    // Update is called once per frame
    private void Update() {
        // should be done on demand, too tired for that
        if (_lastDisplayedHealth != PlayerController.Instance.remainingHealth) {
            _lastDisplayedHealth = PlayerController.Instance.remainingHealth;

            if (_lastDisplayedHealth < 1) {
                SceneManager.LoadScene("GameOver");
            }

            // quick mindless solution
            if (_lastDisplayedHealth == 0) {
                _images[0].sprite = heartTextures[0];
            } else if (_lastDisplayedHealth == 1) {
                _images[0].sprite = heartTextures[1];
            } else {
                _images[0].sprite = heartTextures[2];
            }

            if (_lastDisplayedHealth == 2) {
                _images[1].sprite = heartTextures[0];
            } else if (_lastDisplayedHealth == 3) {
                _images[1].sprite = heartTextures[1];
            } else if (_lastDisplayedHealth > 3) {
                _images[1].sprite = heartTextures[2];
            } else {
                _images[1].sprite = heartTextures[0];
            }

            if (_lastDisplayedHealth == 4) {
                _images[2].sprite = heartTextures[0];
            } else if (_lastDisplayedHealth == 5) {
                _images[2].sprite = heartTextures[1];
            } else if (_lastDisplayedHealth == 6) {
                _images[2].sprite = heartTextures[2];
            } else {
                _images[2].sprite = heartTextures[0];
            }
        }
    }
}