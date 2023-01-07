using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Health healthComponent;
        [SerializeField] private RectTransform foreground;
        private void Update()
        {
            foreground.localScale = new Vector3(healthComponent.GetHealthFraction(), 1f, 1f);
        }
    }
}
