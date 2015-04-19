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

    public void UpdateItems()
    {
    }
}
