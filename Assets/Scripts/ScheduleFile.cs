using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScheduleFile
{
    private const string jsonFileName = "schedule";
    private static string dataPath = Path.Combine(Application.persistentDataPath, jsonFileName);

    public static void SaveToFile(ScheduleDataset saves)
    {
        string json = JsonUtility.ToJson(saves);
        using StreamWriter file = new StreamWriter(dataPath);

        file.WriteLine(json);
        file.Close();
    }

    public static ScheduleDataset LoadSave()
    {
        if (!File.Exists(dataPath))
        {
            return null;
        }

        string lines = File.ReadAllText(dataPath);
        ScheduleDataset saves;

        saves = JsonUtility.FromJson<ScheduleDataset>(lines);
        return saves;
    }
}

[Serializable]
public class ScheduleDataset
{
    public int[] scheduleDataArr;

    public ScheduleDataset(int[] scheduleDataArr)
    {
        this.scheduleDataArr = scheduleDataArr;
    }
}