using TMPro;
using UnityEngine;

public class TrialUIManager : MonoBehaviour
{
    public TrialCSVReader csvReader;

    public TMP_Text trialNumberText;
    public TMP_Text conditionText;
    public TMP_Text distanceText;
    public TMP_Text directionText;
    public TMP_Text sizeText;
    public TMP_Text finalPathText;

    private int currentTrialIndex = 0;

    public void InitializeUI()
    {
        currentTrialIndex = 0;
        ShowCurrentTrial();
    }

    public void ShowCurrentTrial()
    {
        if (csvReader == null || csvReader.trials.Count == 0)
        {
            trialNumberText.text = "Trial: No Data";
            conditionText.text = "Condition: No Data";
            distanceText.text = "Distance: No Data";
            directionText.text = "Direction: No Data";
            sizeText.text = "Size: No Data";

            if (finalPathText != null)
                finalPathText.text = "";

            return;
        }

        if (currentTrialIndex >= csvReader.trials.Count)
        {
            trialNumberText.text = "Experiment Complete";
            conditionText.text = "";
            distanceText.text = "";
            directionText.text = "";
            sizeText.text = "";
            return;
        }

        TrialRow currentTrial = csvReader.trials[currentTrialIndex];

        trialNumberText.text = "Trial: " + currentTrial.TrialNumber;
        conditionText.text = "Condition: " + FormatConditionName(currentTrial.Condition);
        distanceText.text = "Distance: " + currentTrial.Amplitude.ToString("F2") + " m";
        directionText.text = "Direction: " + currentTrial.Direction;
        sizeText.text = "Size: " + currentTrial.Diameter.ToString("F2") + " m";

        if (finalPathText != null)
            finalPathText.text = "";
    }

    public void MoveToNextTrial()
    {
        currentTrialIndex++;
        ShowCurrentTrial();
    }

    public TrialRow GetCurrentTrial()
    {
        if (currentTrialIndex < csvReader.trials.Count)
            return csvReader.trials[currentTrialIndex];

        return null;
    }

    public void ShowFinalPath(string path)
    {
        if (finalPathText != null)
        {
            finalPathText.text = "CSV Saved: " + path;
        }
    }

    private string FormatConditionName(string rawCondition)
    {
        switch (rawCondition)
        {
            case "ControllerTrigger":
                return "Controller Trigger";
            case "HandPointPinch":
                return "Point + Pinch";
            default:
                return rawCondition;
        }
    }
}