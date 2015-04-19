using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VendingMachine : MonoBehaviour {

    private uint m_numPiesLeft;
    private GameObject m_instancedUI;

    public uint InitialPieCountMin = 15;
    public uint InitialPieCountMax = 25;
    public float canBuyFromDistance = 3.0f;

    // Use this for initialization
    void Start ()
    {
        m_numPiesLeft = (uint)Random.Range((int)InitialPieCountMin, (int)InitialPieCountMax);
    }

    // Update is called once per frame
    void Update ()
    {

    }

    void OnMouseEnter()
    {
        if ((Vector3.Distance(transform.position, PlayerController.Instance.gameObject.transform.position)
         < canBuyFromDistance))
        {
            CreateText();
        }
    }

    void OnMouseExit()
    {
        DeleteText();
    }

    void OnMouseUp()
    {
        if (m_numPiesLeft > 0)
        {
            if (PlayerInventory.Instance.TryBuyingOnePie())
                --m_numPiesLeft;
        }
    }

    void CreateText()
    {
        if (m_instancedUI == null)
        {
            m_instancedUI = (GameObject)Object.Instantiate(Resources.Load("Texts/BuyPieMessage"), transform.position, transform.rotation);
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
            RectTransform parentRect = m_instancedUI.GetComponent<RectTransform>();
            parentRect.localPosition = parentRect.localPosition + offsetDir;
        }
    }
    
    void DeleteText()
    {
        if (m_instancedUI != null)
        {
            Object.Destroy(m_instancedUI);
        }
    }
}
