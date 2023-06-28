using UnityEngine;
using TMPro;

public class MapLevel : MonoBehaviour
{
    private Episode m_Episode;
    [SerializeField] private TMP_Text text;
    public void LoadLevel()
    {
        LevelSequenceController.Instance.StartEpisode(m_Episode);
    }
    public void SetLevelData(Episode episode, int score)
    {
        m_Episode = episode;
        text.text = $"{score}/3";
    }
}
