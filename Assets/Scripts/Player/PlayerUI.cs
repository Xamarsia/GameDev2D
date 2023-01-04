using TMPro;
using StatSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _hpText;
        [SerializeField] private PlayerStatsController _statsController;

        private void Start()
        {
            _slider.maxValue = Mathf.CeilToInt(_statsController.GetStatValue(StatType.HitPoint));  //0.5 = 1
        }

        public void UpdateHpUi(float value)
        {
            int hp = Mathf.CeilToInt(value); //0.5 = 1
            _slider.value = hp;
            _hpText.text = hp.ToString();
        }

    }
}
