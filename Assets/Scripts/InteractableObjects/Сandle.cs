using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class Сandle : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _damageDelay;

    private PlayerCharacter _playerCharacter;

    private float _lastDamageTime;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        _playerCharacter = collider.GetComponent<PlayerCharacter>();
        if (_playerCharacter != null)
        {
            _playerCharacter.TakeDamage(_damage);
            _lastDamageTime = Time.time;
        }
    }

    private void Update()
    {
        if (Time.time - _lastDamageTime > _damageDelay && _playerCharacter != null)
        {
            Debug.Log(Time.time - _lastDamageTime);
            _playerCharacter.TakeDamage(_damage);
            _lastDamageTime = Time.time;

        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        PlayerCharacter playerCharacter = collider.GetComponent<PlayerCharacter>();
        if (_playerCharacter == playerCharacter)
        {
            _playerCharacter = null;
        }
    }
}
