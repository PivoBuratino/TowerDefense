using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDisplayController : MonoBehaviour
{
    [SerializeField] private MapLevel[] levels;
    [SerializeField] private BranchLevel[] branchLevels;
    private void Start()
    {
        var drawLevel = 0;

        var score = 1;

        while (score != 0 && drawLevel < levels.Length)
        {
            levels[drawLevel].Initialize();
            drawLevel++;
            if (score == 0) break;
        }

        for (int i = drawLevel; i < levels.Length; i++)
        {
            levels[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < branchLevels.Length; i++)
        {          
            branchLevels[i].TryActivate();
        }
    }
}
