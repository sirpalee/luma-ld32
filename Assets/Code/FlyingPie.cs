using UnityEngine;
using System.Collections;

public class FlyingPie : MonoBehaviour {
    public Vector3 direction;
    public float speed;

    private void FixedUpdate() {
        transform.position = transform.position + speed * direction * Time.fixedDeltaTime;
    }

    private void SpawnSplat() {
        Instantiate(Resources.Load("Splat"), new Vector3(transform.position.x, 0.0f, transform.position.z),
            Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider collider) {
        var enemyController = collider.gameObject.GetComponent<EnemyController>();
        if (enemyController != null) {
            SpawnSplat();
            enemyController.HitByAPie();
        } else if (collider.gameObject.CompareTag("Wall")) {
            SpawnSplat();
        }
    }
}