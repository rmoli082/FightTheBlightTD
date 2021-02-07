using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class take care of scaling the UI image that is used as a health bar, based on the ratio sent to it.
/// It is a singleton so it can be called from anywhere (e.g. PlayerController SetHealth)
/// </summary>
public class UIXpBar : Singleton<UIXpBar>
{
	public Image xpBar;

	float xpOriginalSize;

    void Start()
	{
		xpOriginalSize = xpBar.rectTransform.rect.width;
		if (HeroManager.Instance.GetHeroXP() == 0)
        {
			SetXpValue(0);
        }
	}

	public void SetXpValue(float value)
	{
		xpBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, xpOriginalSize * value);
	}

}
