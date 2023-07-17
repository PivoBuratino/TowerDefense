using UnityEngine.EventSystems;

public class NullBuildSite : BuildSite
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        HideControls();
    }
}
