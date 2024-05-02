using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import the TMPro namespace if using TextMeshPro for UI

[System.Serializable]
public class Dialog
{
    public string text;
    public List<Responses> responses;
}

[System.Serializable]
public class Responses
{
    public string responseText;
    public int nextDialogIndex;
}

public class DialogManager : MonoBehaviour
{
    public List<Dialog> dialogs = new List<Dialog>();
    public int currentDialogIndex = 0;

    public Canvas canvas; // Reference to the Canvas
    private TextMeshProUGUI dialogText; // Using TextMeshPro for better text appearance
    private List<Button> responseButtons = new List<Button>(); // A list to store buttons dynamically

    private void Start()
    {
        CreateDialogUI();
        ShowDialog(currentDialogIndex);
    }

    void CreateDialogUI()
    {
        // Create the dialog text component dynamically
        GameObject textObj = new GameObject("DialogText");
        textObj.transform.SetParent(canvas.transform, false);
        dialogText = textObj.AddComponent<TextMeshProUGUI>();
        dialogText.fontSize = 24;
        dialogText.color = Color.black;
        dialogText.alignment = TextAlignmentOptions.Center;
        dialogText.rectTransform.anchoredPosition = new Vector2(0, 100);
        dialogText.rectTransform.sizeDelta = new Vector2(600, 200);

        // Create buttons based on the maximum number of responses we might need
        for (int i = 0; i < 4; i++) // Assume max 4 responses for example
        {
            GameObject buttonObj = new GameObject("ResponseButton" + i);
            buttonObj.transform.SetParent(canvas.transform, false);
            Button button = buttonObj.AddComponent<Button>();
            TextMeshProUGUI buttonText = buttonObj.AddComponent<TextMeshProUGUI>();
            button.targetGraphic = buttonText;
            buttonText.text = "Option " + (i + 1);
            buttonText.fontSize = 20;
            buttonText.color = Color.white;
            buttonText.alignment = TextAlignmentOptions.Center;
            buttonObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -100 - 50 * i);
            buttonObj.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 40);
            button.onClick.AddListener(() => Debug.Log("Clicked Button " + i)); // Placeholder listener
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

        currentDialogIndex = index;
        Dialog currentDialog = dialogs[index];
        dialogText.text = currentDialog.text;

        // Update and show buttons based on current dialog responses
        for (int i = 0; i < responseButtons.Count; i++)
        {
            if (i < currentDialog.responses.Count)
            {
                responseButtons[i].gameObject.SetActive(true);
                responseButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentDialog.responses[i].responseText;
                int nextDialogIndex = currentDialog.responses[i].nextDialogIndex;
                responseButtons[i].onClick.RemoveAllListeners();
                responseButtons[i].onClick.AddListener(() => ShowDialog(nextDialogIndex));
            }
            else
            {
                responseButtons[i].gameObject.SetActive(false);
            }
        }
    }
}
