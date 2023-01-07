using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using TMPro;
using UnityEngine;

namespace RPG.Combat
{
    public class TargetHealthDisplay : MonoBehaviour
    {
        private Fighter fighter;
        private TextMeshProUGUI healthText;
        // Start is called before the first frame update
        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
            healthText = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (fighter.GetTarget() == null)
            {
                healthText.text = "No Target";
                return;
            }

            //healthText.text = fighter.GetTarget().GetHealth().ToString() + "/" + fighter.GetTarget().GetMaxHealth().ToString();
            Health target = fighter.GetTarget();
            healthText.text = string.Format("{0:0}/{1:0}", target.GetHealth(), target.GetMaxHealth());
        }
    }
}
