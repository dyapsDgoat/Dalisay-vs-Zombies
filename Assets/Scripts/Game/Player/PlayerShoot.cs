using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private float _bulletSpeed;

    [SerializeField]
    private Transform _gunOffset;

    [SerializeField]
    private float _timeBetweenShots;

    private bool _fireContinuously;
    private bool _fireSingle;
    private float _lastFireTime;

    void Update()
    {
        if (_fireContinuously || _fireSingle)
        {
            float timeSinceLastFire = Time.time - _lastFireTime;

            if (timeSinceLastFire >= _timeBetweenShots)
            {
                FireBullet();

                _lastFireTime = Time.time;
                _fireSingle = false;
            }
        }
    }

    private void FireBullet()
    {
       GameObject bullet = Instantiate(_bulletPrefab, _gunOffset.position, _gunOffset.rotation);
    Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

    // Use the local up direction of the _gunOffset
    rigidbody.velocity = _bulletSpeed * _gunOffset.up;
    }

    private void OnFire(InputValue inputValue)
    {
        _fireContinuously = inputValue.isPressed;

        if (inputValue.isPressed)
        {
            _fireSingle = true;
        }
    }

    public void applyBuff(float amount) {
        _timeBetweenShots = amount;
    }

    public void RevertBuff() {
        _timeBetweenShots = 1;
    }
}
