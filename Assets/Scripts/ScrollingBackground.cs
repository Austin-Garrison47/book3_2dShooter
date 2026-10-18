
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    void Update()
    {
        GetComponent<Renderer>().material.mainTextureOffset =
            new Vector2(0, 0.5f * Time.time);
    }
}
