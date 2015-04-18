using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager instance = null;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject();
                instance = obj.AddComponent<GameManager>();
                obj.name = "GameManager";
            }

            return instance;
        }
    }

    // public variables for shit
    public float gameStartTime;

    // Use this for initialization
    void Start () {
        gameStartTime = Time.time;
    }

    // Update is called once per frame
    void Update () {

    }
}
