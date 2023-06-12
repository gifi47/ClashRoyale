using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected GameObject target;
    protected float speed = 0f;
    protected IEffect effect;

    public static void Launch(Projectile projectile, GameObject target, float speed)
    {
        projectile.target = target;
        projectile.speed = speed;
        projectile.enabled = true;
        projectile.effect = new EffectDamage(null, target.GetComponent<ITargetable>(), 999);
    }

    public static void Launch(Projectile projectile, GameObject target, float speed, IEffect effect)
    {
        projectile.target = target;
        projectile.speed = speed;
        projectile.enabled = true;
        projectile.effect = effect;
    }

    void FixedUpdate()
    {
        if (target == null) 
        {
            OnLoseTarget();
            return; 
        }
        if (Vector3.Distance(target.transform.position, this.transform.position) < speed * Time.fixedDeltaTime) 
        {
            OnImpact();
            return;
        }
        Vector3 direction = (target.transform.position - this.transform.position).normalized;
        this.transform.rotation = Quaternion.LookRotation(direction);
        this.transform.position += direction * speed * Time.fixedDeltaTime;
    }

    protected virtual void OnLoseTarget()
    {
        Destroy(this.gameObject);
    }

    protected virtual void OnImpact()
    {
        target.GetComponent<ITargetable>().AddEffect(effect);
        Destroy(this.gameObject);
    }
}
