using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class GetItemWithItem : MonoBehaviour
{
    public Item itemToHave;
    public Item itemToReceive;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < GameObject.Find("Player").GetComponent<Player>().inventory.Length; i++)
        {
            if (GameObject.Find("Player").GetComponent<Player>().inventory[i] == itemToHave)
            {
                for (int j = 0; i < GameObject.Find("Player").GetComponent<Player>().inventory.Length; j++)
                {
                    if (GameObject.Find("Player").GetComponent<Player>().inventory[j] == null)
                    {
                        GameObject.Find("Player").GetComponent<Player>().inventory[j] = itemToReceive;

                        gameObject.SetActive(false);
                        break;
                    }
                }
            }
        }
    }
}
