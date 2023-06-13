using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultPanelController : SingletonBase<ResultPanelController>
{
    [SerializeField] private TMP_Text m_Kills;
    [SerializeField] private TMP_Text m_Score;
    [SerializeField] private TMP_Text m_Time;

    [SerializeField] private TMP_Text m_Result;

    [SerializeField] private TMP_Text m_ButtonNextText;

    [SerializeField] LevelConditionScore levelMission;

    private bool m_Success;
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowResults(PlayerStatistics levelResults, bool success)
    {
        gameObject.SetActive(true);
        m_Success = success;

        if (success == true && LevelController.Instance.LevelTime < levelMission.BonusTime)
        {
            levelResults.score = (int)(levelResults.score * levelMission.MultiplyScore);
            m_Time.text = "Time: " + levelResults.time.ToString() + " - X" + levelMission.MultiplyScore + " bonus";
        }
        else m_Time.text = "Time elapsed: " + levelResults.time.ToString();

        m_Kills.text = "Enemies killed: " + levelResults.numKills.ToString();
        m_Score.text = "Score points: " + levelResults.score.ToString();        

        m_Result.text = success ? "Win" : "Lose";
        m_ButtonNextText.text = success ? "Next" : "Restart";

        Time.timeScale = 0;
    }
    public void OnButtonNextAction()
    {
        gameObject.SetActive(false);

        Time.timeScale = 1;

        if(m_Success)
        {
            LevelSequenceController.Instance.AdvanceLevel();
        }
        else
        {
            LevelSequenceController.Instance.RestartLevel();
        }
    }
}
