using UnityEngine;

namespace World
{
    public class Billboard : MonoBehaviour
    {
        private void Update()
        {
            if (Camera.main != null)
            {
                transform.LookAt(Camera.main.transform);
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180, 0);
            }
        }
    }
}