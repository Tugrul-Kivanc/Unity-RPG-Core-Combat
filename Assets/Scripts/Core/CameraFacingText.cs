using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class CameraFacingText : MonoBehaviour
    {
        private void LateUpdate()
        {
            // Option 1
            // transform.LookAt(Camera.main.transform);

            // Option 2
            // transform.rotation = Camera.main.transform.rotation;

            // Option 3
            transform.forward = Camera.main.transform.forward;
        }
    }
}
