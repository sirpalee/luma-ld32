using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemCounter : MonoBehaviour {

    private GameObject[] childObjects;

    // Use this for initialization
    void Start () {
        List<GameObject> childs = new List<GameObject>();

        foreach (DummyItem item in GetComponentsInChildren<DummyItem>())
        {
            childs.Add(item.gameObject);
        }

        childObjects = childs.ToArray();
    }

    // Update is called once per frame
    void Update () {

    }

    void AddItem(string ItemName)
    {
        foreach (GameObject obj in childObjects)
        {
            DummyItem item = obj.GetComponent<DummyItem>();
            if ((item != null) && (item.ItemTypeName == ItemName))
            {
                item.found = true;
                // TODO change texture here
                return;
            }
        }

        throw new System.NotSupportedException();
    }
}
