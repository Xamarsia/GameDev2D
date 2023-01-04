using Player;
using System;
using System.Collections.Generic;
using StatSystem;
using UnityEngine;

namespace InteractableObjects
{
    public class StatsChangingItem : InteractableObject
    {
        [SerializeField] private List<StatModificator> _statsToChange;
        protected override void Interact(PlayerCharacter playerCharacter)
        {
            base.Interact(playerCharacter);
            foreach(var stat in _statsToChange)
            {
                if(stat.IsMultiplier)
                {
                    playerCharacter.PlayerStatsController.MultiplyStat(stat.Stat.StatType, stat.Stat.Value, stat.Duration);
                    continue;
                }
                playerCharacter.PlayerStatsController.AddValueToStat(stat.Stat.StatType, stat.Stat.Value, stat.Duration);
            }
            Destroy(gameObject);
        }

        [Serializable]
        private class StatModificator
        {
            [SerializeField] private float _duration;
            [SerializeField] private bool _isMultiplier;
            [SerializeField] private Stat _stat;

            public float Duration => _duration;
            public bool IsMultiplier => _isMultiplier;
            public Stat Stat => _stat;
        }
    }
}
