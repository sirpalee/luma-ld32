using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    [System.Serializable]
    public struct ItemWeight {
        public string itemTypeName;
        public float weight;

        public ItemWeight(string _itemTypeName, float _weight)
        {
            itemTypeName = _itemTypeName;
            weight = _weight;
        }
    };

    public ItemWeight[] itemWeightsForShelves = new ItemWeight[3]
    {
        new ItemWeight("coin", 0.5f),
        new ItemWeight("pie", 0.2f),
        new ItemWeight("", 2.0f)
    };

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

    public GameManager()
    {
        instance = this;
    }

    [HideInInspector]
    public float gameStartTime;

    // Use this for initialization
    void Start () {
        gameStartTime = Time.time;

        // distributing the objects
        ShelfScript[] shelves = Object.FindObjectsOfType<ShelfScript>();

        int numShelves = shelves.Length;
        if (numShelves < 6) // we need at least 6 shelves
            throw new System.NotSupportedException("Less than 6 shelves available");

        List<ShelfScript> shelvesList = new List<ShelfScript>();

        foreach (ShelfScript each in shelves)
            shelvesList.Add(each);

        foreach (string baseItem in new string[]
        {
            "chocolate", "chocolate", "chocolate",
            "icecream", "icecream", "sundaecup"
        })
        {
            int id = Random.Range(0, numShelves--);
            shelvesList[id].containedItem = baseItem;
            shelvesList.RemoveAt(id);
        }

        float totalWeight = 0.0f;
        foreach (ItemWeight item in itemWeightsForShelves)
        {
            totalWeight += item.weight;
        }

        while (numShelves > 0)
        {
            int id = Random.Range(0, numShelves--);
            string baseItem = "";
            float chance = Random.Range(0.0f, totalWeight);
            foreach (ItemWeight itemWeight in itemWeightsForShelves)
            {
                baseItem = itemWeight.itemTypeName;
                if ((chance -= itemWeight.weight) < 0.0f) break;
            }
            shelvesList[id].containedItem = baseItem;
            shelvesList.RemoveAt(id);
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.Escape))
            Application.LoadLevel("GameOver");
    }
}

// EOF