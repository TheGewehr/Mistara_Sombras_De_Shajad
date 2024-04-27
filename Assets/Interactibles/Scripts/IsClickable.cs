using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class IsClickable : MonoBehaviour
{
    Item item = null;
    Dialog dialoge = null;

    private void Start()
    {
        item = GetComponent<Item>();
        dialoge = GetComponent<Dialog>();
    }

    void Update()
    {
        
    }

    public bool ManageInteraction()
    {
        if(item != null) 
        {
            AddToInventory();
            return true; 
        }
        else if(dialoge != null)
        {
            StartDialogue();
            return true;
        }

        return false;
    }

    private bool StartDialogue()
    {
        return true;
    }

    private bool AddToInventory()
    {
        //Debug.Log(" Does the component player works: " + GameObject.Find("Player").GetComponent<Player>().inventory[0].itemName); // peta

        //Debug.Log(" Inventory Lenght: " + GameObject.Find("Player").GetComponent<Player>().inventory.Length);

        for (int i = 0; i < GameObject.Find("Player").GetComponent<Player>().inventory.Length; i++)
        {
            if(GameObject.Find("Player").GetComponent<Player>().inventory[i] == null)
            {
                GameObject.Find("Player").GetComponent<Player>().inventory[i] = item;
                //Debug.Log(" Does the component player works: " + GameObject.Find("Player").GetComponent<Player>().inventory[i].itemName);
                break;
            }
            //Debug.Log(" Does the component player works: " + GameObject.Find("Player").GetComponent<Player>().inventory[i].itemName);
        }        

        //Destroy(gameObject);
        gameObject.SetActive(false);

        return true;
    }
}