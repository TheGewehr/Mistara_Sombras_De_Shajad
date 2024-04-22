using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class IsClickable : MonoBehaviour
{
    Item item = null;
    //Dialog dialog = null;

    private void Start()
    {
        item = GetComponent<Item>();
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
        //else if(dialog != null)
        //{
        //     return true;
        //}

        return false;
    }

    private bool StartDialogue()
    {
        return true;
    }

    private bool AddToInventory()
    {
        // add to inventory

        //Debug.Log(" Does the component player works: " + GameObject.Find("Player").GetComponent<Player>().inventory[1].itemName); // peta

        //for (int i = 0; i < 10; i++)
        //{
        //    if(GameObject.Find("Player").GetComponent<Player>().inventory[i].itemName == null)
        //    {
        //        GameObject.Find("Player").GetComponent<Player>().inventory[i] = item;
        //        break;
        //        //break;
        //    }            
        //}

        Destroy(gameObject);

        return true;
    }
}
