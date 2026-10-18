
using UnityEngine;
using System.Collections;

public class ManageTargetHealth : MonoBehaviour
{
    public int health = 3;
    public GameObject explosion;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage()
    {
        health--;

        if (health <= 0)
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.AddScore(10);
            }

            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(Flash());
        }
    }

    IEnumerator Flash()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
}
