using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public GameObject Puntaje;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            Puntaje.GetComponent<Puntaje>().SaveDataToFile();
            SceneManager.LoadScene(3);
        }

        
    }
}
