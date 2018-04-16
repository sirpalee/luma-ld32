using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SimpleSound : MonoBehaviour {
    private AudioSource m_audioSource;

    private void Start() {
        m_audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Update() {
        if (!m_audioSource.isPlaying)
            Destroy(gameObject);
    }
}