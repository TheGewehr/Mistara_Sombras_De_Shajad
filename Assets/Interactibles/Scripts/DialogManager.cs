using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;

[System.Serializable]
public class Dialog
{
    public string text = "Default Dialog Text";
    public List<Responses> responses;
}

[System.Serializable]
public class ItemCondition
{
    public Item conditionItem;
    public int newDialogIndex;
}

[System.Serializable]
public class Responses
{
    public string responseText = "Default Response Text";
    public int neutralNextDialogIndex = 0;
    public int badNextDialogIndex = 0;
    public int goodNextDialogIndex = 0;
    public bool isFinalResponse = false;
    public float sumToKarma = 0.0f;
    public string nameOfRetreivedItem = null;
    public Item itemAddedToInventory;
}

public class DialogManager : MonoBehaviour
{
    public List<ItemCondition> itemConditions = new List<ItemCondition>();
    public List<Dialog> dialogs = new List<Dialog>();
    private int currentDialogIndex = 0; // 0 es siempre el primero

    public Canvas canvas;
    public TMP_FontAsset customFont;
    public Sprite backgroundSprite;
    private float backgroundWidth = 1077f; // Width of the background
    private float backgroundHeight = 624f; // Height of the background
    private TextMeshProUGUI dialogText;
    private List<Button> responseButtons = new List<Button>();
    private GameObject backgroundObj; // Reference to the background GameObject
    private string nameOfRetreivedItemDialog = null;

    private void Start()
    {
        if (customFont == null)
        {
            customFont = Resources.Load<TMP_FontAsset>("Sprite Comic"); // Ensure the font asset is named "DefaultFont" and placed in Resources folder
        }
        if (backgroundSprite == null)
        {
            backgroundSprite = Resources.Load<Sprite>("UI_Dialogo"); // Ensure the sprite is named "DefaultBackground" and placed in Resources folder
        }

        for(int i = 0; i < itemConditions.Count; i++)
        {
            if (itemConditions[i] != null)
            {
                for (int j = 0; j < GameObject.Find("Player").GetComponent<Player>().inventory.Length; j++)
                {
                    if (GameObject.Find("Player").GetComponent<Player>().inventory[j] == itemConditions[i].conditionItem)
                    {
                        currentDialogIndex = itemConditions[i].newDialogIndex;
                        break;
                    }
                }
            }
        }

        InitializeDialogUI();
    }

    public void InitializeDialogUI()
    {

        //TRYING SHIT
        for (int i = 0; i < itemConditions.Count; i++)
        {
            if (itemConditions[i] != null)
            {
                for (int j = 0; j < GameObject.Find("Player").GetComponent<Player>().inventory.Length; j++)
                {
                    if (GameObject.Find("Player").GetComponent<Player>().inventory[j] == itemConditions[i].conditionItem)
                    {
                        currentDialogIndex = itemConditions[i].newDialogIndex;
                        break;
                    }
                }
            }
        }

        // Destroy previous dialog UI elements if they exist
        if (backgroundObj != null) Destroy(backgroundObj);
        if (dialogText != null) Destroy(dialogText.gameObject);



        foreach (Button button in responseButtons)
        {
            if (button != null)
            {
                Destroy(button.gameObject);
            }
        }
        responseButtons.Clear();

        CreateDialogUI();
        ShowDialog(currentDialogIndex);
    }

    void CreateDialogUI()
    {
        // Create the background GameObject
        backgroundObj = new GameObject("DialogBackground");
        backgroundObj.transform.SetParent(canvas.transform, false);
        Image backgroundImage = backgroundObj.AddComponent<Image>();
        backgroundImage.sprite = backgroundSprite; // Assign the background sprite
        backgroundImage.rectTransform.sizeDelta = new Vector2(backgroundWidth, backgroundHeight); // Set the size of the background
        backgroundImage.rectTransform.anchoredPosition = new Vector2(0, 0); // Position the background
        //backgroundImage.rectTransform.position = new Vector2(0, 0);

        // Create the dialog text GameObject
        GameObject textObj = new GameObject("DialogText");
        textObj.transform.SetParent(backgroundObj.transform, false); // Set the background as the parent
        dialogText = textObj.AddComponent<TextMeshProUGUI>();
        dialogText.fontSize = 24;
        dialogText.color = Color.white;
        dialogText.alignment = TextAlignmentOptions.Center;
        dialogText.rectTransform.anchoredPosition = new Vector2(0, 150);
        dialogText.rectTransform.sizeDelta = new Vector2(600, 200);
        dialogText.font = customFont;  // Assigning the custom font to dialog text

        // Create the response buttons
        for (int i = 0; i < 4; i++)
        {
            GameObject buttonObj = new GameObject("ResponseButton" + i);
            buttonObj.transform.SetParent(backgroundObj.transform, false); // Set the background as the parent
            Button button = buttonObj.AddComponent<Button>();
            TextMeshProUGUI buttonText = buttonObj.AddComponent<TextMeshProUGUI>();
            button.targetGraphic = buttonText;
            buttonText.text = "Option " + (i + 1);
            buttonText.fontSize = 20;
            buttonText.color = Color.white;
            buttonText.alignment = TextAlignmentOptions.Center;
            buttonText.font = customFont;  // Assigning the custom font to button text
            buttonObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,  - 50 * i * 3.5f);
            buttonObj.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 60);
            responseButtons.Add(button);
        }
    }

    public void ShowDialog(int index)
    {
        if (index < 0 || index >= dialogs.Count)
        {
            Debug.Log("Dialog index out of range: " + index);
            return;
        }

        Dialog currentDialog = dialogs[index];
        dialogText.text = currentDialog.text;

        for (int i = 0; i < responseButtons.Count; i++)
        {
            if (i < currentDialog.responses.Count)
            {
                responseButtons[i].gameObject.SetActive(true);
                var response = currentDialog.responses[i];
                responseButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = response.responseText;

                bool finalResponse = response.isFinalResponse;
                float karmaVariation = response.sumToKarma;

                responseButtons[i].onClick.RemoveAllListeners();
                responseButtons[i].onClick.AddListener(() => HandleResponse(response, finalResponse, karmaVariation));
            }
            else
            {
                responseButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void HandleResponse(Responses response, bool isFinalResponse, float karmaVariation)
    {
        GameObject.Find("Player").GetComponent<Player>().AddKarma(karmaVariation);

        int nextDialogIndex;
        var playerKarma = GameObject.Find("Player").GetComponent<Player>().GetKarma();
        if (playerKarma == KarmaType.NEUTRAL)
        {
            nextDialogIndex = response.neutralNextDialogIndex;
        }
        else if (playerKarma == KarmaType.GOOD)
        {
            nextDialogIndex = response.goodNextDialogIndex;
        }
        else // Assuming BAD Karma
        {
            nextDialogIndex = response.badNextDialogIndex;
        }

        nameOfRetreivedItemDialog = response.nameOfRetreivedItem;

        if (isFinalResponse)
        {
            foreach (Button button in responseButtons)
            {
                if (button != null)
                {
                    Destroy(button.gameObject);
                }
            }
            Destroy(dialogText.gameObject);
            if (backgroundObj != null)
            {
                Destroy(backgroundObj);
            }
            this.enabled = false;
            GameObject.Find("Player").GetComponent<FollowClick>().enabled = true;
            GetComponent<IsClickable>().enabled = true;
            currentDialogIndex = nextDialogIndex;
        }
        else
        {
            ShowDialog(nextDialogIndex);
        }

        //if (nameOfRetreivedItemDialog != null)
        //{
        //    for (int i = 0; i < GameObject.Find("Player").GetComponent<Player>().inventory.Length; i++)
        //    {
        //        if (GameObject.Find("Player").GetComponent<Player>().inventory[i].itemName == nameOfRetreivedItemDialog && GameObject.Find("Player").GetComponent<Player>().inventory[i] != null)
        //        {
        //            //GameObject.Find("Player").GetComponent<Player>().inventory[i].itemName = "Default";
        //            //Destroy(GameObject.Find("Player").GetComponent<Player>().inventory[i]);
        //            break;
        //        }
        //    }                   
        //    //gameObject.SetActive(false);
        //}

        if(response.itemAddedToInventory != null)
        {
            for (int i = 0; i < GameObject.Find("Player").GetComponent<Player>().inventory.Length; i++)
            {
                if (GameObject.Find("Player").GetComponent<Player>().inventory[i] == null)
                {
                    GameObject.Find("Player").GetComponent<Player>().inventory[i] = response.itemAddedToInventory;                    
                    break;
                }
            }

            //gameObject.SetActive(false);
        }        
    }
}
