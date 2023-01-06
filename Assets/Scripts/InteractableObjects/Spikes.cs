using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _damageDelay;

    private float _lastDamageTime;

    private Health _health;



    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.tag == "Player")
        {
            _health = collider.GetComponent<Health>();
            _health.TakeDamage(_damage);
            _lastDamageTime = Time.time;
        }
    }

    private void Update()
    {
        if (Time.time - _lastDamageTime > _damageDelay && _health != null)
        {
            Debug.Log(Time.time - _lastDamageTime);
            _health.TakeDamage(_damage);
            _lastDamageTime = Time.time;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        //PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        //PlayerMovement playerMovement = collider.GetComponent<PlayerMovement>();
        if (collider.tag == "Player")
        {
            _health = null;
        }
    }
}
