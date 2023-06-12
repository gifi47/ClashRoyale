using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ExplosiveProjectile : Projectile
{
    protected float radius = 3;

    [SerializeField]
    protected VisualEffect visualEffect;

    [SerializeField]
    protected GameObject model;

    public static void Launch(ExplosiveProjectile projectile, GameObject target, float speed, float radius)
    {
        projectile.radius = radius;
        projectile.target = target;
        projectile.speed = speed;
        projectile.enabled = true;
        projectile.effect = new EffectDamage(null, target.GetComponent<ITargetable>(), 999);
    }

    protected override void OnLoseTarget()
    {
        OnImpact();
    }

    protected override void OnImpact()
    {
        visualEffect.enabled = true;
        visualEffect.Play();
        for (int i = 0; i < GameController.entities[effect.source.targetTeam].Count; i++)
        {
            GameObject entity = GameController.entities[effect.source.targetTeam][i];
            if (Vector3.Distance(entity.transform.position, this.transform.position) <= radius)
            {
                ITargetable target = entity.GetComponent<ITargetable>();
                target.AddEffect(new EffectDamage(null, target, ((EffectDamage)effect).damageValue));
            }
        }
        Destroy(this.gameObject, 4f);
        model.SetActive(false);
        this.enabled = false;
    }
}
