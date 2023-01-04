using StatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStatsController : MonoBehaviour
    {
        [SerializeField] private StatsContainer _statsContainer;

        private List<Stat> _baseStats;
        private List<Stat> _currentStats;

        private Dictionary<StatType, StatModificator> _activeBuffs = new Dictionary<StatType, StatModificator>();

        private void Start()
        {
            _baseStats = _statsContainer.Stats;
            _currentStats = new List<Stat>();
            for(int i = 0; i < _baseStats.Count; i++)
            {
                _currentStats.Add(_baseStats[i].GetCopy());
            }
        }

        public float GetStatValue(StatType statType)
        {
            if (TryGetStat(statType, out Stat stat))
            {
                return stat.Value;
            }
            return 0;
        }

        public void SetStatValue(StatType statType, float newValue) 
        {
            if(newValue < 0)
            {
                newValue = 0;
            }
            if (TryGetStat(statType, out Stat stat))
            {
                stat.SetValue(newValue);
            }
            Debug.LogError($"{statType} = {newValue}");
        }

        private void Update()
        {
            if (_activeBuffs.Count == 0)
            {
                return;
            }
            var expiredBuffsKeys = new List<StatType>();
            foreach (var keyValuePair in _activeBuffs)
            {
                StatType statType = keyValuePair.Key;
                StatModificator statModificator = keyValuePair.Value;

                if (statModificator.StartTime + statModificator.Duration > Time.time)
                {
                    continue;
                }
                var oldValue = GetStatValue(statType);
                SetStatValue(statType, oldValue - statModificator.Value);
                expiredBuffsKeys.Add(statType);
            }

            foreach (var expiredBuffsKey in expiredBuffsKeys)
            {
                _activeBuffs.Remove(expiredBuffsKey);
            }
        }

        public void AddValueToStat(StatType statType, float value, float duration)
        {
            float newValue = GetStatValue(statType) + value;
            if (duration <= 0)
            {
                SetStatValue(statType, newValue);
                return;
            }

            if (_activeBuffs.TryGetValue(statType, out StatModificator oldModificator))
            {

                if (oldModificator.Value > value)
                {
                    return;
                }
                if (oldModificator.Value == value)
                {
                    oldModificator.AddDuration(duration);
                    return;
                }
            }

            SetStatValue(statType, newValue);
            StatModificator statModificator = new StatModificator(duration, value, Time.time);
            _activeBuffs[statType] = statModificator;
        }

        public void MultiplyStat(StatType statType, float multiplier, float duration)
        {
            var oldValue = GetStatValue(statType);
            var currentlyAddedValue = _activeBuffs.TryGetValue(
                statType, out StatModificator statModificator) ? statModificator.Value : 0;

            var valueToAdd = (oldValue - currentlyAddedValue) * multiplier;
            AddValueToStat(statType, valueToAdd, duration);
        }

        private class StatModificator
        {
            public float Duration { get; private set; }
            public float Value { get; }
            public float StartTime { get; }

            public StatModificator(float duration, float value, float startTime)
            {
                Duration = duration;
                Value = value;
                StartTime = startTime;
            }

            public void AddDuration(float duration)
            {
                Duration += duration;
            }
        }


        private bool TryGetStat(StatType statType, out Stat stat)
        {
            stat = _currentStats.Find(st => st.StatType == statType);

            if (stat != null)
            {
                return true;
            }

            Debug.LogError($" There is no stat with stat type {statType}");
            return false;
        }
    }
}
