using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShelfScript : MonoBehaviour {

    //[HideInInspector]
    public string containedItem = "empty";
    public float searchTime = 5.0f;
    public float searchableFromDistance = 2.5f;

    private GameObject m_instancedUI;
    private RectTransform m_rect;
    private Coroutine m_barCoroutine;

    private float m_approximateObjectRadius = 0.0f;

    // Use this for initialization
    void Start ()
    {
        m_barCoroutine = null;
        m_rect = null;
        m_instancedUI = null;

        BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            m_approximateObjectRadius = Vector3.Distance(boxCollider.bounds.min, boxCollider.bounds.max) / 2.0f;
        }
        else
        {
            SphereCollider sphereCollider = gameObject.GetComponent<SphereCollider>();
            m_approximateObjectRadius = sphereCollider.radius;
        }
        m_approximateObjectRadius += searchableFromDistance;
    }

    // Update is called once per frame
    void Update () 
    {
    }

    void OnMouseEnter()
    {
        if ((containedItem != "") && 
            (Vector3.Distance(transform.position, PlayerController.Instance.gameObject.transform.position)
            < m_approximateObjectRadius))
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
        if (PlayerInventory.Instance.TryPickingUp(containedItem))
        {
            containedItem = "";
            DeleteText();
        }
    }

    void CreateText()
    {
        if (m_instancedUI == null)
        {
            m_instancedUI = (GameObject)Object.Instantiate(Resources.Load("Texts/SearchMessage"), transform.position + new Vector3(0.0f, 0.6f, 0.0f) , Quaternion.identity);
            Image image = m_instancedUI.GetComponentInChildren<Image>();
            m_rect = image.gameObject.GetComponent<RectTransform>();
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
