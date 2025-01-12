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

        if (PlayerPrefs.HasKey("CurrentLives"))
        {
            _currentLives = PlayerPrefs.GetInt("CurrentLives");
        }
        else
        {
            _currentLives = _maxLives;
        }

        UpdateHealthBar();
    }

    public void UpdateHealth(int livesLost)
    {
        _currentLives -= livesLost;
        _currentLives = Mathf.Clamp(_currentLives, 0, _maxLives);

        PlayerPrefs.SetInt("CurrentLives", _currentLives);
        PlayerPrefs.Save();

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