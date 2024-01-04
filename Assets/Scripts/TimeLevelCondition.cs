using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeLevelCondition : MonoBehaviour, ILevelCondition
{
    [SerializeField] private float timeLimit = 4f;
    [SerializeField] private TMP_Text gameClock;    
    public bool IsCompleted => Time.time > timeLimit;
    private void Start()
    {
        timeLimit += Time.time;   
    }
    private void Update()
    {
        if (!TDLevelController.ClockStopped)
        {
            gameClock.text = ((int)(timeLimit - Time.time)).ToString();
        }
    }    
}
