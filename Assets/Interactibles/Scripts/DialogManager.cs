using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Dialog
{
    public string text = "Default Dialog Text";
    public List<Responses> responses;
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
}

public class DialogManager : MonoBehaviour
{
    public List<Dialog> dialogs = new List<Dialog>();
    private int currentDialogIndex = 0; // 0 es siempre el primero

    public Canvas canvas;
    private TextMeshProUGUI dialogText;
    private List<Button> responseButtons = new List<Button>();

    private void Start()
    {
        InitializeDialogUI();
    }

    public void InitializeDialogUI()
    {
        // Destroy previous dialog UI elements if they exist
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
        GameObject textObj = new GameObject("DialogText");
        textObj.transform.SetParent(canvas.transform, false);
        dialogText = textObj.AddComponent<TextMeshProUGUI>();
        dialogText.fontSize = 24;
        dialogText.color = Color.black;
        dialogText.alignment = TextAlignmentOptions.Center;
        dialogText.rectTransform.anchoredPosition = new Vector2(0, 100);
        dialogText.rectTransform.sizeDelta = new Vector2(600, 200);

        for (int i = 0; i < 4; i++)
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
            this.enabled = false;
            GameObject.Find("Player").GetComponent<FollowClick>().enabled = true;
            GetComponent<IsClickable>().enabled = true;
            currentDialogIndex = nextDialogIndex;
        }
        else
        {
            ShowDialog(nextDialogIndex);
        }
    }

}
