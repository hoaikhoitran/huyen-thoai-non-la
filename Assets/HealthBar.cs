using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthBar : MonoBehaviour
{
    public Image fillBar;
    public TextMeshProUGUI valueText;

    public void UpdateBar(int currentValue, int maxValue)
    { 
        if (maxValue <= 0)
        {
            maxValue = 1;
        }

        if (fillBar != null)
        {
            fillBar.fillAmount = (float)currentValue / (float)maxValue;
        }

        if (valueText != null)
        {
            valueText.text = currentValue.ToString() + "/" + maxValue.ToString();
        }
    }

}
