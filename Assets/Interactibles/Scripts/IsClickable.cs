using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class IsClickable : MonoBehaviour
{
   
    private void Start()
    {
        
    }

    void Update()
    {
        
    }

    public bool AddToInventory()
    {
        // add to inventory

        Destroy(gameObject);

        return true;
    }
}
