using UnityEngine;
using System.Collections;

public class SimpleSound : MonoBehaviour {

    private AudioSource m_audioSource;

    // Use this for initialization
    void Start () {
        m_audioSource = gameObject.GetComponent<AudioSource>();
        m_audioSource.Play();
    }

    // Update is called once per frame
    void Update ()
    {
        if (!m_audioSource.isPlaying)
            Destroy(this.gameObject);
    }
}
