using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Health healthComponent;
        [SerializeField] private RectTransform foreground;
        [SerializeField] private Canvas rootCanvas;

        private void Start()
        {
            rootCanvas.enabled = false;
        }

        public void UpdateHealthBar(float value)
        {
            if (Mathf.Approximately(healthComponent.GetHealthFraction(), 0f))
            {
                rootCanvas.enabled = false;
                return;
            }

            rootCanvas.enabled = true;
            foreground.localScale = new Vector3(healthComponent.GetHealthFraction(), 1f, 1f);
        }
    }
}
