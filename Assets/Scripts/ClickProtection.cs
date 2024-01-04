using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickProtection : SingletonBase<ClickProtection>, IPointerClickHandler
{
    private Image blocker;
    public bool m_ShowKillZone;
    private void Start()
    {
        blocker = GetComponent<Image>();
        blocker.enabled = false;
    }
    private Action<Vector2> m_OnClickAction;
   
    public void Activate(Action<Vector2> mouseAction)
    {
        blocker.enabled = true;
        m_ShowKillZone = true;
        m_OnClickAction = mouseAction;

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        blocker.enabled = false;
        m_ShowKillZone = false;
        m_OnClickAction(eventData.pressPosition);
        m_OnClickAction = null;
    }
}
