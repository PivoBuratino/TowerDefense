using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class MapLevel : MonoBehaviour
{
    [SerializeField] private Episode m_Episode;
    [SerializeField] private RectTransform resultPanel;
    [SerializeField] private Image[] resultImages;
    public bool IsComplete { get { return gameObject.activeSelf && resultPanel.gameObject.activeSelf; } }

    private void Awake()
    {
        for (int i = 0; i < resultImages.Length; i++)
        {
            resultImages[i].color = Color.black;
        }
    }

    internal void Initialize()
    {
        var score = MapCompletion.Instance.GetEpisodeScore(m_Episode);
        resultPanel.gameObject.SetActive(score > 0);

        for (int i = 0; i < score; i++)
        {
            resultImages[i].color = Color.white;
        }
    }

    public void LoadLevel()
    {
        LevelSequenceController.Instance.StartEpisode(m_Episode);
    }    
}
