using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour {
    private static PlayerInventory instance;

    private PlayerInventory() {
        instance = this;
    }

    public static PlayerInventory Instance {
        get { return instance; }
    }

    public uint maxNumberOfPies = 5;
    public uint maxNumberOfDollars = 100;
    public uint piesPerDollar = 2;

    [HideInInspector] public bool hasItemInRange;

    private readonly Dictionary<string, uint> _items = new Dictionary<string, uint>() {
        {"pie", 10},
        {"dollar", 5},
        {"chocolate", 0},
        {"icecream", 0},
        {"sundaecup", 0},
        {"cherry", 0}
    };

    public uint NumberOfPies {
        get { return _items["pie"]; }
    }

    public uint NumberOfDollars {
        get { return _items["dollar"]; }
    }

    public uint GetItemCount(string itemTypeName) {
        return _items.ContainsKey(itemTypeName) ? _items[itemTypeName] : 0;
    }

    private void UpdateItemCount() {
        var itemCounter = (ItemCounter) Object.FindObjectOfType<ItemCounter>();
        if (itemCounter != null) {
            itemCounter.UpdateItems(this);
        }
    }

    private void Start() {
        hasItemInRange = false;
        UpdateItemCount();
    }

    public bool TryThrowingPie() {
        if (_items["pie"] > 0) {
            --_items["pie"];
            UpdateItemCount();
            return true;
        }

        return false;
    }

    public bool TryPickingUp(string itemTypeName) {
        if (itemTypeName == "coin") {
            if (_items["dollar"] < maxNumberOfDollars) {
                ++_items["dollar"];
                UpdateItemCount();
                return true;
            }

            return false;
        }

        if (itemTypeName == "pie") {
            if (_items["pie"] < maxNumberOfPies) {
                ++_items["pie"];
                UpdateItemCount();
                return true;
            }

            return false;
        }

        if (itemTypeName == "vending") {
            if (_items["dollar"] > 0 && _items["pie"] < maxNumberOfPies - piesPerDollar + 1) {
                --_items["dollar"];
                _items["pie"] += 2;
                UpdateItemCount();
                return true;
            }

            return false;
        }

        if (itemTypeName == "empty") {
            return true;
        }

        ++_items[itemTypeName];
        UpdateItemCount();
        return true;
    }

    public bool IsReadyForMagicSmoothie() {
        return _items["chocolate"] > 2 && _items["icecream"] > 1 && _items["sundaecup"] > 0 &&
               _items["cherry"] > 0;
    }
}