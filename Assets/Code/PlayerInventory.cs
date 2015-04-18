using UnityEngine;
using System.Collections;

public class PlayerInventory : MonoBehaviour {

    private static PlayerInventory instance = null;
    
    PlayerInventory()
    {
        if (instance != null)
            throw new System.NotSupportedException();
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
    private uint m_numberOfDollars = 0;

    public uint maxNumberOfPies = 5;
    public uint maxNumberOfDollars = 100;

    public uint NumberOfPies
    {
        get
        {
            return m_numberOfPies;
        }
    }

    public uint NumberOfDollars
    {
        get
        {
            return m_numberOfDollars;
        }
    }

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {

    }

    bool TryBuyingOnePie()
    {
        if ((m_numberOfDollars > 0) && (m_numberOfPies < maxNumberOfPies))
        {
            --m_numberOfDollars;
            ++m_numberOfPies;
            return true;
        }
        else return false;
    }

    bool TryPickingUpOneDollar()
    {
        if (m_numberOfDollars < maxNumberOfDollars)
        {
            ++m_numberOfDollars;
            return true;
        }
        else return false;
    }
}
