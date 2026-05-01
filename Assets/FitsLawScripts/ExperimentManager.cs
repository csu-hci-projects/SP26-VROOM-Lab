using UnityEngine;
public class ExperimentManager : MonoBehaviour
{
    public TrialUIManager trialUIManager;
    public Transform headTransform;
    public ExperimentCSVLogger csvLogger;

    [Header("UI Button Target")]
    public RectTransform targetButton;       // Drag your green Button here
    public RectTransform boardCanvas;        // Drag the Canvas RectTransform here

    [Header("Spawn Zone (normalized 0-1 of canvas height)")]


    [Header("Controller Interaction Components")]
    public Behaviour[] controllerInteractionComponents;

    [Header("Hand Interaction Components")]
    public Behaviour[] handInteractionComponents;

    private float trialStartTime;
    private string trialStartTimestamp;

    public void StartExperiment()
    {
        if (trialUIManager == null) { Debug.LogError("TrialUIManager missing."); return; }
        if (headTransform == null)  { Debug.LogError("Head Transform missing."); return; }
        if (targetButton == null)   { Debug.LogError("Target Button missing."); return; }
        if (boardCanvas == null)    { Debug.LogError("Board Canvas missing."); return; }
        if (csvLogger == null)      { Debug.LogError("CSV Logger missing."); return; }

        StartCurrentTrial();
    }

    public void StartCurrentTrial()
    {
        TrialRow currentTrial = trialUIManager.GetCurrentTrial();

        if (currentTrial == null)
        {
            Debug.Log("No more trials.");
            targetButton.gameObject.SetActive(false);
            csvLogger.SaveToFile();
            trialUIManager.ShowFinalPath(csvLogger.FilePath);
            return;
        }

        ApplyInteractionCondition(currentTrial.Condition);
        PositionButton(currentTrial);

        trialStartTime = Time.time;
        trialStartTimestamp = csvLogger.GetCurrentTimestamp();

        Debug.Log("Started Trial " + currentTrial.TrialNumber);
    }



private void PositionButton(TrialRow trial)
{
    targetButton.gameObject.SetActive(true);

    float size = Mathf.Clamp(trial.Diameter * 0.1f, 0.05f, 0.3f);
    targetButton.sizeDelta = new Vector2(size, size);
    
    float halfW = size * 0.5f;
    float halfH = size * 0.5f;

    float edgePadding = halfW * 0.5f;

    float minX = -0.5f + edgePadding;
    float maxX =  0.5f - edgePadding;

    float minY = -0.5f + edgePadding;
    float maxY =  0.5f - edgePadding;

    targetButton.anchoredPosition = new Vector2(
        Random.Range(minX, maxX),
        Random.Range(minY, maxY)
    );
}
    public void OnTargetSelected()
    {
        TrialRow currentTrial = trialUIManager.GetCurrentTrial();
        if (currentTrial == null) return;

        float movementTime = Time.time - trialStartTime;
        string trialEndTimestamp = csvLogger.GetCurrentTimestamp();

        csvLogger.LogTrial(trialStartTimestamp, trialEndTimestamp,
                           currentTrial, movementTime, true);

        Debug.Log("Selected Trial " + currentTrial.TrialNumber +
                  " | MT = " + movementTime.ToString("F3") + " s");

        trialUIManager.MoveToNextTrial();
        StartCurrentTrial();
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private void ApplyInteractionCondition(string condition)
    {
        if (condition == "ControllerTrigger")
        {
            SetComponentArrayState(controllerInteractionComponents, true);
            SetComponentArrayState(handInteractionComponents, false);
        }
        else if (condition == "HandPointPinch")
        {
            SetComponentArrayState(controllerInteractionComponents, false);
            SetComponentArrayState(handInteractionComponents, true);
        }
        else
        {
            Debug.LogWarning("Unknown condition: " + condition);
            SetComponentArrayState(controllerInteractionComponents, true);
            SetComponentArrayState(handInteractionComponents, false);
        }
    }

    private void SetComponentArrayState(Behaviour[] components, bool state)
    {
        if (components == null) return;
        foreach (Behaviour comp in components)
            if (comp != null) comp.enabled = state;
    }
}