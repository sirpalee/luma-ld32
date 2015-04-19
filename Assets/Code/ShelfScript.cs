using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShelfScript : MonoBehaviour {

    //[HideInInspector]
    public string containedItem = "None";
    public float searchTime = 5.0f;
    public float searchableFromDistance = 2.5f;

    private GameObject m_instancedUI;
    private RectTransform m_rect;
    private Coroutine m_barCoroutine;

    // Use this for initialization
    void Start ()
    {
        m_barCoroutine = null;
        m_rect = null;
        m_instancedUI = null;
    }

    // Update is called once per frame
    void Update () 
    {
    }

    void OnMouseEnter()
    {
        if ((containedItem != "") && 
            (Vector3.Distance(transform.position, PlayerController.Instance.gameObject.transform.position)
            < searchableFromDistance))
        {
            CreateText();
        }
    }

    void OnMouseDown()
    {
        if (m_barCoroutine == null && m_instancedUI != null)
        {
            m_rect.localScale = new Vector3(0.0f, 1.0f, 1.0f);
            m_barCoroutine = StartCoroutine(AnimateBar());
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
        else if (m_instancedUI != null) DeleteText();
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
        m_barCoroutine = null;
        PlayerController.Instance.RecieveItem(containedItem);
        containedItem = "";
        DeleteText();
    }

    void CreateText()
    {
        if (m_instancedUI == null)
        {
            m_instancedUI = (GameObject)Object.Instantiate(Resources.Load("Texts/SearchMessage"), transform.position, transform.rotation);
            Image image = m_instancedUI.GetComponentInChildren<Image>();
            m_rect = image.gameObject.GetComponent<RectTransform>();
            m_instancedUI.transform.SetParent(transform);
            RectTransform parentRect = m_instancedUI.GetComponent<RectTransform>();
            Vector3 dir = PlayerController.Instance.transform.position - transform.position;
            bool dirX = Mathf.Abs(dir.x) > Mathf.Abs(dir.y);
            Vector3 size = new Vector3(1.0f, 1.0f, 1.0f);
            MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                Bounds bounds = renderer.bounds;
                size.z = bounds.size.z / 2.0f;
                size.x = bounds.size.x / 2.0f;
                size.y = bounds.size.y / 2.0f;
            }

            // TODO use the size of the object to place the message at the right place
            Vector3 offsetDir = new Vector3(0.0f, 0.0f, -size.z);
            if (dirX)
                offsetDir.x = Mathf.Sign(dir.x) * (size.x + 0.2f);
            else
                offsetDir.y = Mathf.Sign(dir.y) * (size.z + 0.2f);
            parentRect.localPosition = parentRect.localPosition + offsetDir;
            m_rect.localScale = new Vector3(0.0f, 1.0f, 1.0f);
        }
    }

    void DeleteText()
    {
        if (m_instancedUI != null)
        {
            m_rect = null;
            Object.Destroy(m_instancedUI);
        }
    }
}
