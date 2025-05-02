using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO; // important pour File, StreamWriter
using System.Collections.Generic;



public class EvaluationManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI errorText;
    public TextMeshProUGUI clickText;

    public CameraTeleport Level;

    private float startTime;
    private bool taskStarted = false;
    private int errorCount = 0;
    private int clickCount = 0;
    private int totalCubesPlaced = 0;
    private int totalCubesToPlace = 3;

    private string userId = "U1"; // sera mis à jour dans Start()


    void Start()
    {
        string filePath = Application.dataPath + "/Resultats/Resultats.csv";
        userId = GetNextAvailableUserId(filePath);
        Debug.Log("Utilisateur assigné : " + userId);
    }


    void Update()
    {
        if (taskStarted)
        {
            float elapsed = Time.time - startTime;
            timeText.text = "Temps : " + elapsed.ToString("F2") + "s";

        }
    }

    public void RegisterClick()
    {
        clickCount++;
        clickText.text = "Clics : " + clickCount;
        

        if (!taskStarted)
        {
            taskStarted = true;
            startTime = Time.time;
        }
    }

    public void RegisterError()
    {
        errorCount++;
        errorText.text = "Erreurs : " + errorCount;
        
    }

    public void RegisterSuccess()
    {
        int levelactuel = Level.level;
        totalCubesPlaced++;
        if (totalCubesPlaced >= totalCubesToPlace)
        {
            taskStarted = false;
            float totalTime = Time.time - startTime;
            timeText.text = "Temps final : " + totalTime.ToString("F2") + "s";
            Debug.Log("Tâche terminée !");
            Debug.Log("Temps : " + totalTime.ToString("F2") + "s");
            Debug.Log("Clics : " + clickCount);
            Debug.Log("Erreurs : " + errorCount);
            
            SaveResultsToCSV(userId, LevelToCondition(levelactuel), totalTime, errorCount, clickCount);
            totalCubesPlaced = 0;
        }
    }

    private void SaveResultsToCSV(string user, string condition, float time, int errors, int clicks)
    {
        string filePath = Application.dataPath + "/Resultats/Resultats.csv";
        bool fileExists = File.Exists(filePath);

        using (StreamWriter writer = new StreamWriter(filePath, true)) // append mode
        {
            // Si le fichier n'existe pas encore, on écrit l'en-tête
            if (!fileExists)
            {
                writer.WriteLine("Utilisateur,Condition,Temps (s),Erreurs,Clics");
            }

            // On écrit la ligne de résultats
            writer.WriteLine($"{user},{condition},{time:F2},{errors},{clicks}");
        }

        Debug.Log($"Résultats sauvegardés dans : {filePath}");
    }

    public void ResetAll()
    {
        errorCount = 0;
        clickCount = 0;
        totalCubesPlaced = 0;
        timeText.text = "Temps : 0.00s";
        clickText.text = "Clics : 0";
        errorText.text = "Erreurs : 0";
        taskStarted = false;
    }

    private string LevelToCondition(int levelNumber)
    {
        switch (levelNumber)
        {
            case 1: return "A";
            case 2: return "B";
            case 3: return "C";
            default: return "X";
        }
    }

    private string GetNextAvailableUserId(string filePath)
    {
        int id = 1;
        HashSet<string> existingUsers = new HashSet<string>();

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);

            // Skip header
            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');
                if (values.Length > 0)
                {
                    existingUsers.Add(values[0]);
                }
            }
        }

        while (existingUsers.Contains("U" + id))
        {
            id++;
        }

        return "U" + id;
    }





}
