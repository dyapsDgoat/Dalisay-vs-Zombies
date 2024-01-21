using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
   [SerializeField]
   private float _bonusHealth;

   public void IncreaseHealth(){
		HealthController _healthControl = gameObject.GetComponent<HealthController>();

		if (_healthControl != null)
        {
            _healthControl.IncreaseEnemyHealth(_bonusHealth);
            Debug.Log("Current Health is now:" + _healthControl._currentHealth);
        }
        else
        {
            Debug.LogError("HealthController component not found on the GameObject.");
        }
   }
}
