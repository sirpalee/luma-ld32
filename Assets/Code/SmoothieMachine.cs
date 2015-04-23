using UnityEngine;
using System.Collections;

public class SmoothieMachine : MonoBehaviour {
    private GameObject m_instancedUI;
    public float canBuyFromDistance = 3.0f;

    // Use this for initialization
    void Start ()
    {
        m_instancedUI = null;
    }

    // Update is called once per frame
    void Update ()
    {

    }

    void OnMouseOver()
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
        if (PlayerInventory.Instance.IsReadyForMagicSmoothie())
        {
            Application.LoadLevel("Winner");
        }
    }
    
    void CreateText()
    {
        if (m_instancedUI == null)
        {
            m_instancedUI = (GameObject)Object.Instantiate(Resources.Load("Texts/MakeSmoothieMessage"), transform.position + new Vector3(0.0f, 0.6f, 0.0f), Quaternion.Euler(90.0f, 0.0f, 0.0f));
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
