using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    Instant,
    Status
}

public interface IEffect
{
    ITargetable source { get; }
    ITargetable target { get; }
    public void OnStart();
}

public interface IDelayedEffect : IEffect
{
    float delay { get; }
    public IEnumerator OnDelay();

}

public class EffectDamage : IEffect
{
    public ITargetable source { get; }
    public ITargetable target { get; }

    public float damageValue = 0;

    public EffectDamage(ITargetable source, ITargetable target, float damageValue)
    {
        this.source = source;
        this.target = target;
        this.damageValue = damageValue;
    }

    public void OnStart()
    {
        target.ReceiveDamage(damageValue);
    }
}

public class EffectDelayedDamage : IDelayedEffect
{
    public ITargetable source { get; }
    public ITargetable target { get; }

    public float delay { get; }

    public float damageValue = 0;

    public EffectDelayedDamage(ITargetable source, ITargetable target, float damageValue, float delay)
    {
        this.source = source;
        this.target = target;
        this.damageValue = damageValue;
        this.delay = delay;
    }

    public void OnStart()
    {
        target.AddDelayedEffect(this);
    }

    public IEnumerator OnDelay()
    {
        yield return new WaitForSeconds(delay);
        target.ReceiveDamage(damageValue);
    }
}
