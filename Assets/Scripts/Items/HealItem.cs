using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Items
{
    public class HealItem : MonoBehaviour
    {
        [SerializeField] private float _upgradeHp;

        public float UpgradeHp => _upgradeHp;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("HealItem");
            Destroy(gameObject);
        }
    }
}
