using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESCMenuButtons : MonoBehaviour
{
    [SerializeField] private string mainMenu;

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        
    }
}
