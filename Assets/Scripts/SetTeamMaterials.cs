using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTeamMaterials : MonoBehaviour
{
    [SerializeField]
    private Renderer[] renderers;

    void Start()
    {
        Team team = GetComponent<ITargetable>().team;
        switch (team)
        {
            case Team.Red:
                SetMaterial(GameController.red);
                break;
            case Team.Blue:
                SetMaterial(GameController.blue);
                break;
        }
    }

    public void SetMaterial(Material material)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = material;
        }
    }
}