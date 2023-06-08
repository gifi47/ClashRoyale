using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    [SerializeField]
    private EntitySpawner blueKingPosition;

    [SerializeField]
    private EntitySpawner redKingPosition;

    [SerializeField]
    private EntitySpawner blueArcher;

    [SerializeField]
    private EntitySpawner blueWarrior;

    [SerializeField]
    private EntitySpawner blueWarriorStrong;

    [SerializeField]
    private EntitySpawner redArcher;

    [SerializeField]
    private EntitySpawner redWarrior;

    [SerializeField]
    private EntitySpawner redWarriorStrong;



    private void Awake()
    {
        GameController.Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameController.redDefaultTarget = blueKingPosition.SpawnEntity();
        GameController.blueDefaultTarget = redKingPosition.SpawnEntity();
        entityContiniousSpawners = FindObjectsOfType<EntityContiniousSpawner>();
    }

    private EntityContiniousSpawner[] entityContiniousSpawners;

    public void ChangeSpawnDelay(float value)
    {
        if (value > 49)
        {
            value -= 48;
        } else if (value > 32)
        {
            value = 1 + ((value - 33) / 17);
        } else
        {
            value /= 33;
        }
        for (int i = 0; i < entityContiniousSpawners.Length; i++)
        {
            entityContiniousSpawners[i].delay = value;
        }
    }

    public void SpawnBlueArcher()
    {
        blueArcher.SpawnEntity();
    }

    public void SpawnBlueWarrior()
    {
        blueWarrior.SpawnEntity();
    }

    public void SpawnBlueWarriorStrong()
    {
        blueWarriorStrong.SpawnEntity();
    }

    public void SpawnRedArcher()
    {
        redArcher.SpawnEntity();
    }

    public void SpawnRedWarrior()
    {
        redWarrior.SpawnEntity();
    }

    public void SpawnRedWarriorStrong()
    {
        redWarriorStrong.SpawnEntity();
    }
}
