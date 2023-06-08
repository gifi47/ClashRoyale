using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityContiniousSpawner : EntitySpawner
{
    public float delay = 2f;

    private float time = 0f;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > delay)
        {
            SpawnEntity();
            time = 0;
        }
    }
}
