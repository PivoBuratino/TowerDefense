using UnityEngine;

public class GameRecords : SingletonBase<GameRecords>
{
    public int AllTimeKills { get; private set; }
    public int AllTimeScores { get; private set; }
    public int OverallTime { get; private set; }

    private void Start()
    {
        Load();
    }

#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1) == true)
        {
            ResetStats();
        }
    }
    private void ResetStats()
    {
        PlayerPrefs.DeleteAll();
    }
#endif

    public void Save(PlayerStatistics stats)
    {
        AllTimeKills += stats.numKills;
        AllTimeScores += stats.score;
        OverallTime += stats.time;
    
        PlayerPrefs.SetInt("kills", AllTimeKills);
        PlayerPrefs.SetInt("score", AllTimeScores);
        PlayerPrefs.SetInt("time", OverallTime);
    }
    public void Load()
    {
        AllTimeKills = PlayerPrefs.GetInt("kills");
        AllTimeScores = PlayerPrefs.GetInt("score");
        OverallTime = PlayerPrefs.GetInt("time");
    }    
}
