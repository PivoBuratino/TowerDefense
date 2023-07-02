using System;
using UnityEngine;

public class MapCompletion : SingletonBase<MapCompletion>
{
    const string filename = "completion.dat";

    [Serializable]
    private class EpisodeScore
    {
        public Episode episode;
        public int score;
    }
    private new void Awake()
    {
        base.Awake();
        Saver<EpisodeScore[]>.TryLoad(filename, ref completionData);
    }
    public static void SaveEpisodeResult(int levelScore)
    {
        Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
    }

    [SerializeField] private EpisodeScore[] completionData;
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
