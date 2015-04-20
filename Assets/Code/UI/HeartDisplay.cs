using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeartDisplay : MonoBehaviour {

    public Sprite[] heartTextures;

    private Image[] m_images;
    private int m_lastDisplayedHealth;

    // Use this for initialization
    void Start () {
        m_images = gameObject.GetComponentsInChildren<Image>();
        m_lastDisplayedHealth = -1;
    }

    // Update is called once per frame
    void Update ()
    {
        // should be done on demand, too tired for that
        if (m_lastDisplayedHealth != PlayerController.Instance.remainingHealth)
        {
            m_lastDisplayedHealth = PlayerController.Instance.remainingHealth;

            if (m_lastDisplayedHealth < 1)
                Application.LoadLevel("GameOver");

            // quick mindles solution

            if (m_lastDisplayedHealth == 0)
                m_images[0].sprite = heartTextures[0];
            else if (m_lastDisplayedHealth == 1)
                m_images[0].sprite = heartTextures[1];
            else
                m_images[0].sprite = heartTextures[2];

            if (m_lastDisplayedHealth == 2)
                m_images[1].sprite = heartTextures[0];
            else if (m_lastDisplayedHealth == 3)
                m_images[1].sprite = heartTextures[1];
            else if (m_lastDisplayedHealth > 3)
                m_images[1].sprite = heartTextures[2];
            else m_images[1].sprite = heartTextures[0];

            if (m_lastDisplayedHealth == 4)
                m_images[2].sprite = heartTextures[0];
            else if (m_lastDisplayedHealth == 5)
                m_images[2].sprite = heartTextures[1];
            else if (m_lastDisplayedHealth == 6)
                m_images[2].sprite = heartTextures[2];
            else m_images[2].sprite = heartTextures[0];
        }
    }
}
