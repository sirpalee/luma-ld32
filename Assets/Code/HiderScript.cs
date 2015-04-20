using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
public class HiderScript : MonoBehaviour {

    // Use this for initialization
    void Start () {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update () {

    }
}
