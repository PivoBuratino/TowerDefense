using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

public class BuildSite : MonoBehaviour, IPointerDownHandler
{
    public TowerAsset[] availableTowers;
    public void SetAvailableTowers(TowerAsset[] towers) 
    { 
        if (towers == null || towers.Length == 0)
        {
            Destroy(transform.parent.gameObject);            
        }
        else
        {
            availableTowers = towers;
        }
    }

    public static event Action<BuildSite> OnClickEvent;
    public static void HideControls()
    {
        OnClickEvent(null);
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnClickEvent(this);
        print("нажато buildSite");
    }    
}
