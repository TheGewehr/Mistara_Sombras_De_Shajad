using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{
    enum KarmaType
    {
        GOOD,
        NEUTRAL,
        BAD
    }

    private float karma = 0.495f;
    private KarmaType karmaType = KarmaType.NEUTRAL;

    // Array of items = Inventory
    public Item[] inventory = new Item[10];

    // Start is called before the first frame update
    void Start()
    {
        karma = 0.495f;

        //for (int i = 0; i < 10; i++)
        //{
        //    inventory[i].itemName = "Merkava";
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
    }

    void AddKarma(float value)
    {
        karma += value;

        if(karma < 0.0f)
        {
            karma = 0.0f;
        }
        else if (karma > 1.0f)
        {
            karma = 1.0f;
        }
    }
}
