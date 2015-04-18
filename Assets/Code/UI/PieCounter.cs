using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PieCounter : MonoBehaviour {

    private Text m_text;
    
    // Use this for initialization
    void Start () {
        m_text = gameObject.GetComponent<Text>();
    }
    
    // Update is called once per frame
    void Update () {

        PlayerInventory playerInventory = PlayerInventory.Instance;
        if (playerInventory != null)
        {
            string textToDisplay = "Pies : " + System.Convert.ToString(playerInventory.NumberOfPies);
            m_text.text = textToDisplay;
        }
    }
}
