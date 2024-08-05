using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Progress;

public enum KarmaType
{
    GOOD,
    NEUTRAL,
    BAD
}

public class Player : MonoBehaviour
{    

    private float karma = 0.495f;
    private KarmaType karmaType = KarmaType.NEUTRAL;

    // Array of items = Inventory
    public Item[] inventory;

    // Start is called before the first frame update
    void Start()
    {
        karma = 0.495f;

        DontDestroyOnLoad(gameObject);

        //for (int i = 0; i < inventory.Length; i++)
        //{
        //    inventory[i] = new Item();  // Create a new Item instance
        //    inventory[i].itemName = "default";
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if(karma > 0.666f)
        {
            karmaType = KarmaType.GOOD;
        }
        else if (karma < 0.333f)
        {
            karmaType = KarmaType.BAD;
        }
        else
        {
            karmaType = KarmaType.NEUTRAL;
        }

        //AddKarma(0.01f);
        //Debug.Log("Karma Value: " + karma + ", Karma Type: " + karmaType);
    }

    public void AddKarma(float value)
    {
        karma += value;

        if (karma > 0.666f)
        {
            karmaType = KarmaType.GOOD;
        }
        else if (karma < 0.333f)
        {
            karmaType = KarmaType.BAD;
        }
        else
        {
            karmaType = KarmaType.NEUTRAL;
        }

        if (karma < 0.0f)
        {
            karma = 0.0f;
        }
        else if (karma > 1.0f)
        {
            karma = 1.0f;
        }
    }

    public KarmaType GetKarma()
    {
       return karmaType;
    }
}
