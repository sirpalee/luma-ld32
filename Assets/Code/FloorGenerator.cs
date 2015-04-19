using UnityEngine;
using System.Collections;

public class FloorGenerator : MonoBehaviour {

    public GameObject tileToUse = null;

    // Use this for initialization
    void Start () {
        foreach (SpriteRenderer child in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            Destroy(child.gameObject);
        }

        MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
        Bounds bounds = renderer.bounds;
        const float tileSize = 1.28f;
        if (tileToUse == null)
            tileToUse = (GameObject)Resources.Load("Level/FloorSprite");
        int gridSizeX = (int)(bounds.size.x / tileSize) + (Mathf.Repeat(tileSize, bounds.size.x) > 0.01f ? 1 : 0);
        int gridSizeY = (int)(bounds.size.y / tileSize) + (Mathf.Repeat(tileSize, bounds.size.y) > 0.01f ? 1 : 0);
        for (int x = 0; x < gridSizeX; ++x)
        {
            float xx = bounds.min.x + gameObject.transform.position.x + (float)x * tileSize + tileSize / 2.0f;
            for (int y = 0; y < gridSizeY; ++y)
            {
                float yy =  bounds.min.y + gameObject.transform.position.y + (float)y * tileSize + tileSize / 2.0f;
                GameObject tile = (GameObject)GameObject.Instantiate(tileToUse, new Vector3(xx, yy, 0.0f), Quaternion.identity);
                tile.transform.SetParent(gameObject.transform);
            }
        }
    }

    // Update is called once per frame
    void Update () {

    }
}
