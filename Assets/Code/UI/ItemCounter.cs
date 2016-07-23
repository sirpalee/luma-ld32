using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ItemCounter : MonoBehaviour {

    private DummyItem[] m_items;

    // Use this for initialization
    void Start ()
    {
        m_items = GetComponentsInChildren<DummyItem>();
        foreach (DummyItem dummyItem in m_items)
        {
            Text text = dummyItem.gameObject.GetComponentInChildren<Text>();
            text.text = "0 X";
        }
        PlayerInventory playerInventory = (PlayerInventory)Object.FindObjectOfType<PlayerInventory>();
        if (playerInventory != null)
            UpdateItems(playerInventory);
    }

    // Update is called once per frame
    void Update () {

    }

    public void UpdateItems(PlayerInventory playerInventory)
    {
        if (m_items == null)
            return;
        foreach (DummyItem dummyItem in m_items)
        {
            uint itemCount = playerInventory.GetItemCount(dummyItem.itemTypeName);
            Text text = dummyItem.gameObject.GetComponentInChildren<Text>();
            text.text = System.Convert.ToString(itemCount) + " X";
        }
    }
}
