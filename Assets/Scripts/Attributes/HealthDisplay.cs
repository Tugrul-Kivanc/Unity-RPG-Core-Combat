using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        TextMeshProUGUI healthText;
        // Start is called before the first frame update
        void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            healthText = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            healthText.text = health.GetHealth().ToString();
        }
    }
}
