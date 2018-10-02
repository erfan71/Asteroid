using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceCraftHUDManager : MonoBehaviour {

    public Image healthBar;
	
    public void SetHealthBarVar(float percentage)
    {
        Vector2 ancherMax = healthBar.rectTransform.anchorMax;
        Vector2 ancherPos = healthBar.rectTransform.anchoredPosition;
        healthBar.rectTransform.anchorMax = new Vector2(percentage, ancherMax.y);
        healthBar.rectTransform.anchoredPosition = ancherPos;
    }
}
