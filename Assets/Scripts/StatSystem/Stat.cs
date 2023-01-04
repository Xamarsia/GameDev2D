using System;
using UnityEngine;

namespace StatSystem
{
    [Serializable]
    public class Stat 
    {
        [SerializeField] private StatType _statType;
        [SerializeField] private float _value;

        public float Value => _value;
        public StatType StatType => _statType;


        public Stat(StatType statType, float value)
        {
            _statType = statType;
            _value = value;
        }

        public void SetValue(float value)
        { 
            _value = value; 
        }

        public Stat GetCopy()
        {
            return new Stat(_statType, _value);
        }
    }
}

