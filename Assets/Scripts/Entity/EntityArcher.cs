using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityArcher : Entity
{
    [SerializeField]
    private Projectile projectile;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float projectileSpeed = 3.6f;

    [SerializeField]
    private SkinnedMeshRenderer[] teamColorsBodyParts;

    private new void Start()
    {
        base.Start();
        foreach (SkinnedMeshRenderer meshRenderer in teamColorsBodyParts) {
            if (team == Team.Red)
                meshRenderer.material = GameController.red;
            else if (team == Team.Blue)
                meshRenderer.material = GameController.blue;
        }
        animator.SetFloat("attackStartDelay", 1 / (currentAttackDelay * 0.8f));
    }

    private new void Update()
    {
        base.Update();
    }

    protected override bool Attack()
    {
        if ((currentTarget == null) || (Vector3.Distance(currentTarget.transform.position, this.transform.position) > currentAttackRange))
        {
            return false;
        }

        Projectile.Launch(Instantiate(projectile, this.transform.position + offset, Quaternion.identity),
            currentTarget, projectileSpeed, new EffectDamage(this, currentTarget.GetComponent<ITargetable>(), currentDamage));

        return true;
    }
}
