using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityController : MonoBehaviour
{
    // Start is called before the first frame update
    private HealthController _healthController;

    private void Awake()
    {
        _healthController = GetComponent<HealthController>();
    }

    public void StartInvincibility(float invincibilityDuration)
    {
        StartCoroutine(InvincibilityCoroutine(invincibilityDuration));
    }

    private IEnumerator InvincibilityCoroutine(float invincibilityDuration)
    {
        _healthController.IsInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        _healthController.IsInvincible = false;
    }
}
