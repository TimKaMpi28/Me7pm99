using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float period;
    private float currTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime; 
        if (currTime > period && FPSceneController.instance.CanSpawn())
        {
            Spawn();
            currTime = 0f;
        }
    }

    private void Spawn()
    {
        Instantiate(enemy, transform.position, transform.rotation);
        FPSceneController.instance.enemiesCount++;
    }
}
