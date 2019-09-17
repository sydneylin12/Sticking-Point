using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceController : MonoBehaviour
{
    /// <summary>
    /// The xp slider GameObject.
    /// </summary>
    private Slider xpSlider;

    /// <summary>
    /// The xp slider fill color.
    /// </summary>
    private Image xpSliderFill;

    /// <summary>
    /// The player's game data.
    /// </summary>
    private GameData gameData;

    /// <summary>
    /// Called before every frame.
    /// </summary>
    void Start()
    {
        gameData = GameObject.Find("Game Data").GetComponent<GameData>();
        xpSlider = GameObject.Find("XP Bar").GetComponent<Slider>();
        xpSliderFill = GameObject.Find("SliderFill").GetComponent<Image>();
        xpSliderFill.color = Color.green;
        xpSlider.value = CalculateXpSliderValue();
    }

    /// <summary>
    /// Updates XP bar value every frame and slowly increases it.
    /// </summary>
    public IEnumerator GrowXpBar()
    {
        float newValue = CalculateXpSliderValue();
        if (newValue == 0) //new ly leveled up
        {
            while (xpSlider.value < 0.9999f)
            {
                xpSlider.value += 0.002f;
                yield return new WaitForSeconds(0.002f);
            }
        }
        else if (newValue < 1 && newValue > 0)
        {
            while (xpSlider.value < newValue)
            {
                xpSlider.value += 0.002f;
                yield return new WaitForSeconds(0.002f);
            }
        }
        xpSlider.value = newValue;
        yield return new WaitForSeconds(0.002f);
    }

    /// <summary>
    /// Calculates the xp bar value.
    /// </summary>
    /// <returns>The xp slider value as a float.</returns>
    public float CalculateXpSliderValue()
    {
        return (float) gameData.CurrentXp / gameData.XpLeft;
    }
}
