using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] private Text damageText;
        public void SetText(float damageAmount)
        {
            damageText.text = damageAmount.ToString();
            damageText.text = string.Format("{0:0}", damageAmount);
        }
    }
}
