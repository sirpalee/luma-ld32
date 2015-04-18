using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VendingMachine : MonoBehaviour {

    private uint m_numPiesLeft;
    private Canvas m_canvas;

    public uint InitialPieCountMin = 15;
    public uint InitialPieCountMax = 25;

    // Use this for initialization
    void Start ()
    {
        m_numPiesLeft = (uint)Random.Range((int)InitialPieCountMin, (int)InitialPieCountMax);
        m_canvas = GetComponentInChildren<Canvas>();

        if (m_canvas != null)
            m_canvas.enabled = false;
        else
            throw new System.NotSupportedException();
    }

    // Update is called once per frame
    void Update ()
    {
        if (m_canvas.enabled)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (m_numPiesLeft > 0)
                {
                    if (PlayerInventory.Instance.TryBuyingOnePie())
                        --m_numPiesLeft;
                }
            }
        }
    }

    public void SetCanvas(bool state)
    {
        m_canvas.enabled = state;
    }

    void OnMouseOver()
    {
        Debug.Log("Test");
    }
}
