using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuPanel : MonoBehaviour
{
    public void OnButtonShowPause()
    {
        //print("pause hit");
        gameObject.SetActive(true);
        Time.timeScale = 0;        
    }

    public void OnButtonContinue()
    {
        Time.timeScale = 1;

        gameObject.SetActive(false);
    }

    public void OnButtonMainMenu()
    {
        Time.timeScale = 1;

        gameObject.SetActive(false);

        SceneManager.LoadScene(LevelSequenceController.MainMenuSceneNickname);
    }
}
