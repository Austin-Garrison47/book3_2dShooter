
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("target"))
        {
            collision.gameObject.GetComponent<ManageTargetHealth>().TakeDamage();
            Destroy(gameObject);
        }
    }
}
