using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShelfScript : MonoBehaviour {

    [HideInInspector]
    public string ContainedItem = "None";

    private Canvas m_canvas;
    private RectTransform m_rect;

    // Use this for initialization
    void Start () {
        m_canvas = GetComponentInChildren<Canvas>();
        
        if (m_canvas != null)
            m_canvas.enabled = false;
        else
            throw new System.NotSupportedException();

        Image image = GetComponentInChildren<Image>();

        if (image == null)
            throw new System.NotSupportedException();
        else
            m_rect = image.gameObject.GetComponent<RectTransform>();

        m_rect.width = 0.0f;
    }

    // Update is called once per frame
    void Update () {
        if (m_canvas.enabled)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // TODO add item to inventory
            }
        }
    }

    public void SetCanvas(bool state)
    {
        m_canvas.enabled = state;
    }
}
