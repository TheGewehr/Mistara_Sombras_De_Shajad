using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class OpenWithItem : MonoBehaviour
{
    public Item itemToOpen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < GameObject.Find("Player").GetComponent<Player>().inventory.Length; i++)
        {
            if (GameObject.Find("Player").GetComponent<Player>().inventory[i] == itemToOpen)
            {
                gameObject.SetActive(false);
                break;
            }            
        }
    }
}
