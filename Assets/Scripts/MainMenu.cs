using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEditor;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    private void Start()
    {
        continueButton.interactable = FileHandler.HasFile(MapCompletion.filename);
    }
    
    public void NewGame()
    {
        FileHandler.Reset(MapCompletion.filename);
        FileHandler.Reset(Upgrades.filename);
        if (MapCompletion.Instance) MapCompletion.Instance.ResetProgress();
        if (Upgrades.Instance) Upgrades.Instance.ResetProgress();
        SceneManager.LoadScene(1);
    }

    public void Continue()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
