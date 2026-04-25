using System;

using System.Globalization;
using System.IO;
using System.Text;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class ExperimentCSVLogger : MonoBehaviour
{
    public string outputFileName = "Fittslaw_Outputfile.csv";

    private string filePath;
    private StringBuilder csvBuilder = new StringBuilder();

    public string FilePath => filePath;

    void Awake()
    {
        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        string folderPath = Path.Combine(desktopPath, "VroomData");

        if(!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        filePath = Path.Combine(folderPath, outputFileName);

        csvBuilder.AppendLine(
            "TrialStartTimestamp,TrialEndTimestamp,TrialNumber,Condition,Amplitude,Diameter,Direction,Repetition,MovementTime,Hit,Miss,ID_Shannon,Throughput,ErrorRateTrial"
        );
    }

    public void LogTrial(
        string trialStartTimestamp,
        string trialEndTimestamp,
        TrialRow trial,
        float movementTime,
        bool hit
    )
    {
        float idShannon = ComputeShannonID(trial.Amplitude, trial.Diameter);
        float throughput = movementTime > 0f ? idShannon / movementTime : 0f;

        int hitValue = hit ? 1 : 0;
        int missValue = hit ? 0 : 1;
        float errorRateTrial = hit ? 0f : 100f;

        string row =
            $"{trialStartTimestamp}," +
            $"{trialEndTimestamp}," +
            $"{trial.TrialNumber}," +
            $"{trial.Condition}," +
            $"{trial.Amplitude.ToString(CultureInfo.InvariantCulture)}," +
            $"{trial.Diameter.ToString(CultureInfo.InvariantCulture)}," +
            $"{trial.Direction}," +
            $"{trial.Repetition}," +
            $"{movementTime.ToString("F4", CultureInfo.InvariantCulture)}," +
            $"{hitValue}," +
            $"{missValue}," +
            $"{idShannon.ToString("F4", CultureInfo.InvariantCulture)}," +
            $"{throughput.ToString("F4", CultureInfo.InvariantCulture)}," +
            $"{errorRateTrial.ToString("F1", CultureInfo.InvariantCulture)}";

        csvBuilder.AppendLine(row);
    }

    public void SaveToFile()
    {
        File.WriteAllText(filePath, csvBuilder.ToString());
        Debug.Log("Output CSV saved at: " + filePath);
    }

    private float ComputeShannonID(float amplitude, float diameter)
    {
        return Mathf.Log((amplitude / diameter) + 1f, 2f);
    }

    public string GetCurrentTimestamp()
    {
        return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
    }
}