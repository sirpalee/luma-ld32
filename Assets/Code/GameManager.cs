using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [System.Serializable]
    public struct ItemWeight {
        public string itemTypeName;
        public float weight;

        public ItemWeight(string _itemTypeName, float _weight) {
            itemTypeName = _itemTypeName;
            weight = _weight;
        }
    }

    public ItemWeight[] itemWeightsForShelves = {
        new ItemWeight("coin", 0.5f),
        new ItemWeight("pie", 0.2f),
        new ItemWeight("", 2.0f)
    };

    private static GameManager instance = null;

    public static GameManager Instance {
        get {
            if (instance == null) {
                var obj = new GameObject();
                instance = obj.AddComponent<GameManager>();
                obj.name = "GameManager";
            }

            return instance;
        }
    }

    public GameManager() {
        instance = this;
    }

    [HideInInspector] public float gameStartTime;

    [HideInInspector] public Vector3 playerPosition = new Vector3(0.0f, 0.0f, 0.0f);

    private void Start() {
        gameStartTime = Time.time;

        // distributing the objects
        var shelves = FindObjectsOfType<ShelfScript>();

        var numShelves = shelves.Length;
        if (numShelves < 6) // we need at least 6 shelves
            return;
        // throw new System.NotSupportedException("Less than 6 shelves available");

        var shelvesList = new List<ShelfScript>();

        foreach (var each in shelves) {
            shelvesList.Add(each);
        }

        foreach (var baseItem in new string[] {
            "chocolate", "chocolate", "chocolate",
            "icecream", "icecream", "sundaecup"
        }) {
            var id = Random.Range(0, numShelves--);
            shelvesList[id].containedItem = baseItem;
            shelvesList.RemoveAt(id);
        }

        var totalWeight = 0.0f;
        foreach (var item in itemWeightsForShelves) {
            totalWeight += item.weight;
        }

        while (numShelves > 0) {
            var id = Random.Range(0, numShelves--);
            var baseItem = "";
            var chance = Random.Range(0.0f, totalWeight);
            foreach (var itemWeight in itemWeightsForShelves) {
                baseItem = itemWeight.itemTypeName;
                if ((chance -= itemWeight.weight) < 0.0f) break;
            }

            shelvesList[id].containedItem = baseItem;
            shelvesList.RemoveAt(id);
        }
    }

    // Update is called once per frame
    private void Update() {
        if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene("GameOver");
    }
}

// EOF