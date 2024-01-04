using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class NextWaveGUI : MonoBehaviour
{
    [SerializeField] private TMP_Text bonusAmount;

    private EnemyWaveManager manager;
    private float timeToNextWave;
  
    void Awake()
    {
        manager = FindObjectOfType<EnemyWaveManager>();
        EnemyWave.OnWavePrepare += (float time) =>
        {            
            timeToNextWave = time;
        };
    }
    private void Update()
    {
        var bonus = (int)timeToNextWave;
        if (bonus < 0) bonus = 0;
        
        bonusAmount.text = bonus.ToString();
        timeToNextWave -= Time.deltaTime;
    }

    public void CallWave()
    {
        manager.ForceNextWave();
    }
}
