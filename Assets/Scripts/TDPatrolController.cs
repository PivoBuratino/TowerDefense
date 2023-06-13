using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TDPatrolController : AIController
{
    private Path m_Path;
    private int pathIndex;
    [SerializeField] private UnityEvent OnEndPath;
    internal void SetPath(Path newPath)
    {
        m_Path = newPath;
        pathIndex = 0;
        SetPatrolBehaviour(m_Path[pathIndex]);
    }
    protected override void GetNewPoint()
    {
        pathIndex += 1;
        if (m_Path.Length > pathIndex) SetPatrolBehaviour(m_Path[pathIndex]);
        else
        {
            OnEndPath.Invoke();
            Destroy(gameObject);
        }
    }
}
