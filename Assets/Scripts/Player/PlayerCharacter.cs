using StatSystem;
using UnityEngine;

namespace Player
{
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] private PlayerStatsController _playerStatsController;
        [SerializeField] private PlayerUI _playerUI;

       
        public PlayerStatsController PlayerStatsController => _playerStatsController;
        public float Hp { get; private set; }

        private void Start()
        {
            Hp = _playerStatsController.GetStatValue(StatType.HitPoint);
            _playerUI.UpdateHpUi(Hp);
        }
        public void TakeDamage(float damage)
        {
            Hp -= damage;
            if(Hp <=0)
            {
                Hp = 0;
                Die();
            }
        }

        public void Heal(float healValue) 
        {
            Hp += healValue;
            if(Hp < _playerStatsController.GetStatValue(StatType.HitPoint))
            {
                Hp = _playerStatsController.GetStatValue(StatType.HitPoint);
            }
        }

        public void Die()
        {
            
        }
    }
}
