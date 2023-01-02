using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Сandle : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _damageDelay;

    private PlayerMovement _playerMovement;

    private float _lastDamageTime;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        _playerMovement = collider.GetComponent<PlayerMovement>();
        if (_playerMovement != null)
        {
            _playerMovement.TakeDamage(_damage);
            _lastDamageTime = Time.time;
        }
    }

    private void Update()
    {
        if (Time.time - _lastDamageTime > _damageDelay && _playerMovement != null)
        {
            Debug.Log(Time.time - _lastDamageTime);
            _playerMovement.TakeDamage(_damage);
            _lastDamageTime = Time.time;

        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        PlayerMovement playerMovement = collider.GetComponent<PlayerMovement>();
        if (_playerMovement == playerMovement)
        {
            _playerMovement = null;
        }
    }
}
