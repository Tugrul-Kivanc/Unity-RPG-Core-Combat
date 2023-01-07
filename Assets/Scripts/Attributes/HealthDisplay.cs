using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        private Health health;
        private TextMeshProUGUI healthText;
        // Start is called before the first frame update
        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            healthText = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        private void Update()
        {
            //healthText.text = health.GetHealth().ToString() + "/" + health.GetMaxHealth().ToString();
            healthText.text = string.Format("{0:0}/{1:0}", health.GetHealth(), health.GetMaxHealth());
        }
    }
}
