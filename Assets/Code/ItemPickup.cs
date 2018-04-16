using UnityEngine;

public class ItemPickup : MonoBehaviour {
    public string itemTypeName = "coin";

    private void OnTriggerEnter(Collider colliderIn) {
        var playerInventory = colliderIn.gameObject.GetComponent<PlayerInventory>();
        if (playerInventory != null && playerInventory.TryPickingUp(itemTypeName)) {
            Destroy(gameObject);
        }
    }
}