using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EntitySpawner : MonoBehaviour
{
    public bool spawnOnStart = false;

    [SerializeField]
    protected GameObject entityPrefab;

    [SerializeField]
    protected Team team;

    protected void Start()
    {
        if (spawnOnStart) SpawnEntity();
    }

    public GameObject SpawnEntity()
    {
        GameObject entity = Instantiate(entityPrefab, this.transform.position, Quaternion.identity); // spawn in world
        ITargetable target = entity.GetComponent<ITargetable>();
        target.team = team;
        if (target.team == Team.Red)
            target.targetTeam = Team.Blue;
        else
            target.targetTeam = Team.Red;
        GameController.AddEntity(entity, team);
        return entity;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
        //Gizmos.DrawIcon(transform.position, "Assets/Sprites/OtherSprites/JumperTargetIcon.png", true);
    }
#endif
}
