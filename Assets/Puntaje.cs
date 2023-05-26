using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class Puntaje : MonoBehaviour
{
    private float puntos = 999;

    private TextMeshProUGUI textMesh;

    private string fileName = "Puntajes.json";

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();

    }

    private void Update()
    {
        puntos -= Time.deltaTime;
        textMesh.text = puntos.ToString("0");
    }

    public void SaveDataToFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write(puntos);
            }

            Debug.Log("Data saved to file: " + filePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error saving data to file: " + e.Message);
        }
    }

    


}
