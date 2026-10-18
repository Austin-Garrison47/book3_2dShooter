
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public GameObject bullet;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.Translate(Vector3.left * 0.1f);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.Translate(Vector3.right * 0.1f);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            gameObject.transform.Translate(Vector3.up * 0.1f);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.transform.Translate(Vector3.down * 0.1f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject b = (GameObject)(Instantiate(bullet,
                transform.position + transform.up * 1.5f,
                Quaternion.identity));

            b.GetComponent<Rigidbody2D>().AddForce(transform.up * 1000);
        }
    }
}
