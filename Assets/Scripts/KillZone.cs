using UnityEngine;
using UnityEngine.UI;

public class KillZone : MonoBehaviour
{
    [SerializeField] Image circleSprite;
    private float scale;

    private void Start()
    {
        scale = 2.7f * Abilities.Instance.KillRadius;
        circleSprite = GetComponent<Image>();
        circleSprite.enabled = false;        
    }
    private void Update()
    {
        if (ClickProtection.Instance.m_ShowKillZone)
        {            
            circleSprite.enabled = true;
            circleSprite.transform.localScale = new Vector3(scale, scale, scale);
            circleSprite.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }    
        else circleSprite.enabled = false;
    }
}
