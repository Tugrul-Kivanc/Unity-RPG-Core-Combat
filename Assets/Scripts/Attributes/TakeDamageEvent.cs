using System;
using UnityEngine.Events;

namespace RPG.Attributes
{
    public partial class Health
    {
        [Serializable] public class TakeDamageEvent : UnityEvent<float> { }
    }
}