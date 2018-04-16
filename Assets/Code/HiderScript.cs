using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
public class HiderScript : MonoBehaviour {
    private void Start () {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}
