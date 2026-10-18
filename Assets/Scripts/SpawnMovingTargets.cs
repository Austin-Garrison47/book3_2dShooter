
using UnityEngine;

public class SpawnMovingTargets : MonoBehaviour
{
    public GameObject target;

    void Start()
    {
        float spawnTime = 60f / PreferencesManager.spawnRate;
        InvokeRepeating("SpawnTarget", 0f, spawnTime);
    }

    void SpawnTarget()
    {
        float x = Random.Range(-7f, 7f);

        Instantiate(target, new Vector3(x, 6f, 0f),
            Quaternion.identity);
    }
}
