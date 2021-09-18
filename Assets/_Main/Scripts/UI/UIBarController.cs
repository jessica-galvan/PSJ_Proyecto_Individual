using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBarController : MonoBehaviour
{
    [SerializeField] private GameObject bar;
    [SerializeField] private Image barImage;

    public bool IsVisible { get; private set; }

    public void UpdateLifeBar(int currentHealth, int maxHealth)
    {
        if (barImage != null)
            barImage.fillAmount = (float)currentHealth / maxHealth;
    }

    public void IsBarVisible(bool value)
    {
        bar.SetActive(value);
        IsVisible = value;
    }
}
