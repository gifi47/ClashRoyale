using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    Red,
    Blue 
}

public enum EntityTypeInt
{
    Warrior,
    Archer
}

public static class GameController
{
    public static Dictionary<Team, List<GameObject>> entities { get; } = new Dictionary<Team, List<GameObject>>();
    public static Material red;
    public static Material blue;
    public static GameObject redDefaultTarget;
    public static GameObject blueDefaultTarget;
    
    // add all teams
    public static void Initialize()
    {
        foreach (Team teams in (Team[])Enum.GetValues(typeof(Team)))
        {
            entities.Add(teams, new List<GameObject>());
        }
        red = (Material)Resources.Load("Materials/Red");
        blue = (Material)Resources.Load("Materials/Blue");
    }

    // register
    public static void AddEntity(GameObject entity, Team team)
    {
        entities[team].Add(entity);
    }
}
