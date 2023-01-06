using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPotion : MonoBehaviour
{
    [SerializeField] private float _upgradeTime;
    [SerializeField] private float _upgradePower;
    private PlayerMovement playerMovement;

    public float UpgradeTime => _upgradeTime;
    public float UpgradePower => _upgradePower;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.AddJumping(_upgradePower, _upgradeTime);

            Debug.Log("Potion");
            Destroy(gameObject);
        }

    }
}
