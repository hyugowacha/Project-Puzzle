using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TImeUI : MonoBehaviour
{
    [SerializeField] private Slider timeSlider;

    void Update()
    {
        if(GameManager.gameManager != null)
        {
            timeSlider.maxValue = GameManager.gameManager.gameTime;
            timeSlider.value = GameManager.gameManager.remainingTime;
        }
    }
}
