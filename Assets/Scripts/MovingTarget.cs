
using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.down * PreferencesManager.enemySpeed);
    }
}
