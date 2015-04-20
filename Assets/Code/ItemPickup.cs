using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour {

    public string itemTypeName = "coin";

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    void OnTriggerEnter(Collider collider)
    {
        PlayerInventory playerInventory = collider.gameObject.GetComponent<PlayerInventory>();
        if (playerInventory != null)
        {
            if (playerInventory.TryPickingUp(itemTypeName))
                Destroy(this.gameObject);
        }
    }
}
