using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

    [SerializeField]
    private GameObject[] entities;
    //[SerializeField]
    //private Dictionary<EntityType, GameObject> entities = new Dictionary<EntityType, GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        GameController.Initialize();
    }

    float delay = 0.8f;
    float time = 0;

    // Update is called once per frame
    void Update()
    {
        if (time > delay)
        {
            SpawnEntity(EntityTypeInt.Warrior, new Vector3(Random.Range(-9, 9), 3, Random.Range(-4, 14)), Team.Blue);
            time = 0;
        }
        time+= Time.deltaTime;
    }

    // spawn in world and register
    public void SpawnEntity(EntityTypeInt entityTypeInt, Vector3 position, Team team)
    {
        GameObject entity = Instantiate(entities[(int)entityTypeInt], position, Quaternion.identity); // spawn in world
        GameController.AddEntity(entity, team); // register as a valid target
    }
}
