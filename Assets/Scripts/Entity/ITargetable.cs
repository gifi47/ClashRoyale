using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetable
{
    Team targetTeam { get; set; }
    Team team { get; set; }
    public void AddEffect(IEffect effect);
    public void AddDelayedEffect(IDelayedEffect effect);
    public void ReceiveDamage(float damageValue);
}
