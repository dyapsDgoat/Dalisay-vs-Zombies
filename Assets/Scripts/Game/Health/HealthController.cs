using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    public float _currentHealth;

    [SerializeField]
    public float _maximumHealth;

    private ScoreController _scoreController;

    public bool IsInvincible { get; set; }

    public UnityEvent OnDied;

    public UnityEvent OnDamaged;

    public UnityEvent OnHealthChanged;

    public UnityEvent OnBreakPointReached;

    public float RemainingHealthPercentage
    {
        get
        {
            return _currentHealth / _maximumHealth;
        }
    }

    public void Awake() 
    {
        _scoreController = FindObjectOfType<ScoreController>();
    }

    public void Start() 
    {
        if(_scoreController != null) 
        {
            _scoreController.OnScoreChanged.AddListener(CheckScore);
        } else 
        {
            Debug.Log("Score Controller doesn't exist");
        }
    }

    public void CheckScore()
    {
        int currScore = _scoreController.Score;

        if(currScore % 500 == 0)
        {
            OnBreakPointReached.Invoke();
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (_currentHealth == 0)
        {
            return;
        }

        if (IsInvincible)
        {
            return;
        }

        _currentHealth -= damageAmount;

        OnHealthChanged.Invoke();

        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }

        if (_currentHealth == 0)
        {
            OnDied.Invoke();
        }
        else
        {
            OnDamaged.Invoke();
        }
    }

    public void AddHealth(float amountToAdd)
    {
        if (_currentHealth == _maximumHealth)
        {
            return;
        }

        _currentHealth += amountToAdd;

        OnHealthChanged.Invoke();

        if (_currentHealth > _maximumHealth)
        {
            _currentHealth = _maximumHealth;
        }
    }

    public void IncreaseEnemyHealth(float amountToAdd) {
        _currentHealth += amountToAdd;
        _maximumHealth += amountToAdd;

        OnHealthChanged.Invoke();
    }
}
