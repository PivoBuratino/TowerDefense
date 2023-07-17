using System;
using UnityEngine;

public class MapCompletion : SingletonBase<MapCompletion>
{
    public const string filename = "completion.dat";    

    [Serializable]
    private class EpisodeScore
    {
        public Episode episode;
        public int score;
    }

    [SerializeField] private EpisodeScore[] completionData;
    [SerializeField] private int totalScore;
    public int TotalScore { get { return totalScore; } }
    private new void Awake()
    {
        base.Awake();
        Saver<EpisodeScore[]>.TryLoad(filename, ref completionData);
        CalculateTotalScore();
    }

    internal void ResetProgress()
    {
        foreach (var episodeScore in completionData)
        {
            episodeScore.score = 0;
        }
    }

    public void CalculateTotalScore()
    {
        totalScore = 0;
        foreach (var episodeScore in completionData)
        {
            totalScore += episodeScore.score;
        }
    }
    public int GetEpisodeScore(Episode m_Episode)
    {
        foreach (var data in completionData)
        {
            if (data.episode == m_Episode)
            {
                return data.score;
            }            
        }
        return 0;
    }

    public static void SaveEpisodeResult(int levelScore)
    {
        if (Instance)
        {
            Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
        }
        else
        {
            Debug.Log($"Episode complete with score {levelScore}");
        }
    }   

    public bool TryIndex(int id, out Episode episode, out int score)
    {
        if (id >= 0 && id < completionData.Length)
        {
            episode = completionData[id].episode;
            score = completionData[id].score;
            return true;
        }
        episode = null;
        score = 0;
        return false;
    }

    private void SaveResult(Episode currentEpisode, int levelScore)
    {
       foreach (var item in completionData)
        {
            if (item.episode == currentEpisode)
            {
                if (levelScore > item.score)
                { 
                    item.score = levelScore;
                    Saver<EpisodeScore[]>.Save(filename, completionData);
                }       
            }
        }
    }    
}
