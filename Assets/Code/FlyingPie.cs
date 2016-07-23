using UnityEngine;
using System.Collections;

public class FlyingPie : MonoBehaviour {
    public Vector3 direction;
    public float speed;
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    void FixedUpdate()
    {
        transform.position = transform.position + speed * direction * Time.fixedDeltaTime;
    }

    void SpawnSplat()
    {
        Instantiate(Resources.Load("Splat"), new Vector3(transform.position.x, 0.0f, transform.position.z),
                    Quaternion.identity);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider collider)
    {
        EnemyController enemyController = collider.gameObject.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            SpawnSplat();
            enemyController.HitByAPie();
        }
        else if (collider.gameObject.tag == "Wall")
        {
            SpawnSplat();
        }
    }
}
