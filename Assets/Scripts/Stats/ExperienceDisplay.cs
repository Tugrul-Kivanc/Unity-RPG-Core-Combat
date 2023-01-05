using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        private Experience experience;
        private TextMeshProUGUI experienceText;
        // Start is called before the first frame update
        private void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
            experienceText = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        private void Update()
        {
            experienceText.text = experience.GetExperiencePoints().ToString();
        }
    }
}
