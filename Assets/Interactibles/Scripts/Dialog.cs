using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

// A single node in the dialogue

public class DialogNode
{
    private int index = -1;
    public float AddToKarma = 0.0f;
    public string Text; // The dialogue text
    public DialogNode[] Choices; // Indices of the next nodes this node can connect to
}


public class Dialog : MonoBehaviour
{

    public DialogNode Dialogue;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
