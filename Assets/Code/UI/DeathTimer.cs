using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeathTimer : MonoBehaviour {

    private Text m_text;

    // Use this for initialization
    void Start () {
        m_text = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {

        PlayerController playerController = PlayerController.Instance;
        if (playerController != null)
        {
            float expectedDeathTime = playerController.expectedDeathTime;
            float timeLeft = expectedDeathTime - Time.time;
            if (timeLeft < 0.0f)
                m_text.text = "";
            else
            {
                int timeLeftInt = (int)timeLeft;
                int secsLeft = timeLeftInt % 60;
                string textToDisplay = "Time Before Death : " + System.Convert.ToString(timeLeftInt / 60)
                    + (secsLeft < 10 ? ":0" : ":") + 
                    System.Convert.ToString(secsLeft);
                m_text.text = textToDisplay;
            }
        }
    }
}
