using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class TrialCSVReader : MonoBehaviour
{
    [SerializeField] public TextAsset csvFile;   // Drag the CSV here in Inspector
    public List<TrialRow> trials = new List<TrialRow>();

    public void LoadCSV()
    {
        trials.Clear();

        if (csvFile == null)
        {
            Debug.LogError("CSV file is not assigned in the Inspector.");
            return;
        }

        string[] lines = csvFile.text.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();

            if (string.IsNullOrEmpty(line))
                continue;

            string[] values = line.Split(',');

            if (values.Length < 6)
                continue;

            TrialRow row = new TrialRow
            {
                TrialNumber = int.Parse(values[0]),
                Condition = values[1],
                Amplitude = float.Parse(values[2], CultureInfo.InvariantCulture),
                Diameter = float.Parse(values[3], CultureInfo.InvariantCulture),
                Direction = values[4],
                Repetition = int.Parse(values[5])
            };

            trials.Add(row);
        }

        Debug.Log("Loaded trials: " + trials.Count);
    }
}