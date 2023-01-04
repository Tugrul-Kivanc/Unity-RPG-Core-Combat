using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using TMPro;
using UnityEngine;

namespace RPG.Combat
{
    public class TargetHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        TextMeshProUGUI healthText;
        // Start is called before the first frame update
        void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
            healthText = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            if (fighter.GetTarget() != null)
            {
                healthText.text = fighter.GetTarget().GetHealth().ToString();
            }
            else
            {
                healthText.text = "No Target";
            }
        }
    }
}
