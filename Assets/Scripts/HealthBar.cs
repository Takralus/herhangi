using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    private int _maxLives = 5; 
    private int _currentLives = 5; 
    private float _maxHealth = 100f; 
    private float _healthPerLife; 

    [SerializeField] private Image _healthBarFill; 
    [SerializeField] private TextMeshProUGUI _healthText; 
    [SerializeField] private Gradient _colorGradient; 

    void Start()
    {
        _healthPerLife = _maxHealth / _maxLives; 
        _currentLives = _maxLives;
        UpdateHealthBar();
    }

    public void UpdateHealth(int livesLost)
    {
        _currentLives -= livesLost;
        _currentLives = Mathf.Clamp(_currentLives, 0, _maxLives); 
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float currentHealth = _currentLives * _healthPerLife;
        float targetFillAmount = currentHealth / _maxHealth;

        _healthBarFill.fillAmount = targetFillAmount;
        _healthBarFill.color = _colorGradient.Evaluate(targetFillAmount);

        _healthText.text = "Health: " + currentHealth.ToString("F0");
    }
}