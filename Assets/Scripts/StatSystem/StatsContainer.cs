using System.Collections.Generic;
using UnityEngine;

namespace StatSystem
{
    [CreateAssetMenu(fileName = "StatsContainer", menuName = "StatsSystem/StatsContainer")]
    public class StatsContainer : ScriptableObject
    {
        [SerializeField] private List<Stat> _stats;

        public List<Stat> Stats => _stats;
    }
}
