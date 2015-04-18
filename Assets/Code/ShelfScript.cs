using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShelfScript : MonoBehaviour {

    //[HideInInspector]
    public string containedItem = "None";
    public float searchTime = 5.0f;

    private Canvas m_canvas;
    private RectTransform m_rect;
    private Coroutine m_barCoroutine;

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

        m_rect.localScale = new Vector3(0.0f, 1.0f, 1.0f);

        m_barCoroutine = null;
    }

    // Update is called once per frame
    void Update () 
    {
    }

    void OnMouseEnter()
    {
        if (containedItem != "None")
            m_canvas.enabled = true;
    }

    void OnMouseDown()
    {
        if (m_barCoroutine == null)
        {
            if (containedItem != "None")
            {
                m_rect.localScale = new Vector3(0.0f, 1.0f, 1.0f);
                m_barCoroutine = StartCoroutine(AnimateBar());
            }
        }
    }
    
    void OnMouseExit()
    {
        if (m_barCoroutine != null)
        {
            StopCoroutine(m_barCoroutine);
            m_rect.localScale = new Vector3(0.0f, 1.0f, 1.0f);
            m_barCoroutine = null;
        }
        m_canvas.enabled = false;
    }
    
    
    void OnMouseUp()
    {
        if (m_barCoroutine != null)
        {
            StopCoroutine(m_barCoroutine);
            m_rect.localScale = new Vector3(0.0f, 1.0f, 1.0f);
            m_barCoroutine = null;
        }
    }

    public IEnumerator AnimateBar()
    {
        m_rect.localScale = new Vector3(0.0f, 1.0f, 1.0f);
        while (m_rect.localScale.x < 1.0f)
        {
            m_rect.localScale = Vector3.MoveTowards(m_rect.localScale, new Vector3(1.0f, 1.0f, 1.0f), Time.fixedDeltaTime);
            yield return null;
        }
        // Give item to player
        m_barCoroutine = null;
        PlayerController.Instance.RecieveItem(containedItem);
        containedItem = "None";
        m_canvas.enabled = false;
    }
}
