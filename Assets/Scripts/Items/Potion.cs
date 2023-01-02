using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] private float _upgradeTime;
    [SerializeField] private float _upgradePower;

    public float UpgradeTime => _upgradeTime;
    public float UpgradePower => _upgradePower;

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("Potion");
    //    Destroy(gameObject);
    //}
}
