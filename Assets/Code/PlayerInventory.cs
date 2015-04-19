using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour {

    private static PlayerInventory instance = null;
    
    PlayerInventory()
    {
        instance = this;
    }
    
    public static PlayerInventory Instance
    {
        get
        {
            return instance;
        }
    }

    private uint m_numberOfPies = 0;
    private uint m_numberOfDollars = 5;

    public uint maxNumberOfPies = 5;
    public uint maxNumberOfDollars = 100;

    [HideInInspector]
    public bool hasItemInRange = false;

    private Dictionary<string, uint> m_items = new Dictionary<string, uint>()
    {
        {"pie", 0},
        {"dollar", 0},
        {"chocolate", 0},
        {"icecream", 0},
        {"sundaecup", 0},
        {"cherry", 0}
    };

    public uint NumberOfPies
    {
        get
        {
            return m_items["pie"];
        }
    }

    public uint NumberOfDollars
    {
        get
        {
            return m_items["dollar"];
        }
    }

    public uint GetItemCount(string itemTypeName)
    {
        if (m_items.ContainsKey(itemTypeName))
            return m_items[itemTypeName];
        else
            return 0;
    }

    // Use this for initialization
    void Start ()
    {
        hasItemInRange = false;
    }

    // Update is called once per frame
    void Update ()
    {

    }

    public bool TryPickingUp(string itemTypeName)
    {
        if (itemTypeName == "coin")
        {
            if (m_items["dollar"] < maxNumberOfDollars)
            {
                ++m_items["dollar"];
                return true;
            }
            else return false;
        }
        else if (itemTypeName == "pie")
        {
            if (m_items["pie"] < maxNumberOfPies)
            {
                ++m_items["pie"];
                return true;
            }
            else return false;
        }
        else if (itemTypeName == "vending")
        {
            if ((m_items["dollar"] > 0) && (m_items["pie"] < maxNumberOfPies))
            {
                --m_items["dollar"];
                ++m_items["pie"];
                return true;
            }
            else return false;
        }
        else if (itemTypeName == "empty")
            return true;
        else // store item in a list
        {
            ++m_items[itemTypeName];
            return true;
        }
    }
}
