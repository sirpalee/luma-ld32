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

    void OnTriggerEnter(Collider collider)
    {
        EnemyController enemyController = collider.gameObject.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            // splat
            Destroy(this.gameObject);
        }
        else if (collider.gameObject.tag == "Wall")
        {
            // splat
            Destroy(this.gameObject);
        }
    }
}
