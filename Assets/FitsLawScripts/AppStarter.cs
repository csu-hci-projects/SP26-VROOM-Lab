using UnityEngine;

public class AppStarter : MonoBehaviour
{
    public TrialCSVReader csvReader;
    public TrialUIManager trialUIManager;
    public ExperimentManager experimentManager;

    void Start()
    {
        csvReader.LoadCSV();
        trialUIManager.InitializeUI();
        experimentManager.StartExperiment();
    }
}