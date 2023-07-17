using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeLevelCondition : MonoBehaviour, ILevelCondition
{
    [SerializeField] private float timeLimit = 4f;
    [SerializeField] private TMP_Text gameClock;
    private TDLevelController levelControl;
    public bool IsCompleted => Time.time > timeLimit;
    private void Start()
    {
        timeLimit += Time.time;
        levelControl = FindObjectOfType<TDLevelController>();

    }
    private void Update()
    {
        if (!levelControl.ClockStopped)
        {
            gameClock.text = ((int)(timeLimit - Time.time)).ToString();
        }
    }    
}
