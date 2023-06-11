using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Entity : MonoBehaviour, ITargetable
{
    [SerializeField]
    protected EntityType stats;

    // =====================================
    // =====================================

    // CURRENT STATS VALUES
    protected float currentHealth;
    protected float currentSpeed;
    protected float currentAttackDelay;
    protected float currentAttackEndDelay;
    protected float currentAttackRange;
    protected float currentSearchRange;
    protected float currentDamage;

    // =====================================
    // =====================================

    protected GameObject currentTarget;
    protected List<IEffect> effects = new List<IEffect>();

    public Team targetTeam { get; set; }
    public Team team { get; set; }

    protected enum State
    {
        Search,
        Wait,
        Attack,
    }

    protected State state;
    protected NavMeshAgent agent;
    protected Animator animator;



    protected void ResetStats() 
    {
        currentHealth = stats.baseHealth;
        currentSpeed = stats.baseSpeed;
        currentAttackDelay = stats.baseAttackDelay;
        currentAttackEndDelay = stats.baseAttackEndDelay;
        currentAttackRange = stats.baseAttackRange;
        currentSearchRange = stats.baseSearchRange;
        currentDamage = stats.baseDamage;
    }

    protected void Start()
    {
        ResetStats();
        state = State.Search;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.SetFloat("attackStartDelay", 1/currentAttackDelay);
        animator.SetFloat("attackEndDelay", 1/currentAttackEndDelay);
    }

    public void AddEffect(IEffect effect)
    {
        effect.OnStart();
    }

    public void AddDelayedEffect(IDelayedEffect effect)
    {
        StartCoroutine(effect.OnDelay());
    }

    public virtual void ReceiveDamage(float damageValue)
    {
        float newHealth = currentHealth - damageValue;
        if (newHealth <= 0)
        {
            GameController.entities[team].Remove(this.gameObject);
            Destroy(this.gameObject);
        } else
        {
            currentHealth = newHealth;
        }
    }

    protected void Update()
    {
        switch (state)
        {
            case State.Search:
                if (UpdateTarget())
                {
                    state = State.Wait;
                    agent.isStopped = true;
                    animator.Play("attackStart", 0);
                    //animator.PlayInFixedTime("attackStart", 0, 0.5f);
                    //animator.SetBool("isWalk", false);
                    //animator.SetBool("isAttack", true);
                    Invoke("AttackDelay", currentAttackDelay);
                }
                else
                {
                    Move();
                }
                break;
            case State.Wait:
                break;
            case State.Attack:
                if (Attack())
                {
                    state = State.Wait;
                    //animator.PlayInFixedTime("attackEnd", 0);
                    animator.Play("attackEnd", 0);
                    Invoke("AttackEndDelay", currentAttackEndDelay);
                }
                else
                {
                    state = State.Search;
                    //animator.SetBool("isAttack", false);
                }
                break;
        }
    }

    protected virtual bool UpdateTarget()
    {
        if (currentTarget != null)
        {
            if (Vector3.Distance(currentTarget.transform.position, this.transform.position) <= currentAttackRange)
            {
                return true;
            }
            currentTarget = null;
        }

        float minDist = currentSearchRange;
        foreach (GameObject entity in GameController.entities[targetTeam])
        {
            float dist = Vector3.Distance(entity.transform.position, this.transform.position);
            if ((dist < currentSearchRange) && (dist < minDist))
            {
                currentTarget = entity;
                minDist = dist;
            }
        }
        if (currentTarget == null)
        {
            if (team == Team.Red)
            {
                if (GameController.redDefaultTarget != null)
                    currentTarget = GameController.redDefaultTarget;
            } else
            {
                if (GameController.blueDefaultTarget != null)
                    currentTarget = GameController.blueDefaultTarget;
            }
        }
        else return false;
        return minDist <= currentAttackRange;
    }

    protected void Move()
    {
        if (currentTarget != null)
        {
            //animator.SetBool("isWalk", true);
            animator.Play("walk", 0);
            agent.isStopped = false;
            agent.SetDestination(currentTarget.transform.position);
        }
        else
        {
            //animator.SetBool("isWalk", false);
            animator.Play("idle", 0);
            agent.isStopped = true;
        }
    }

    protected virtual bool Attack()
    {
        if ((currentTarget == null) || (Vector3.Distance(currentTarget.transform.position, this.transform.position) > currentAttackRange))
        {
            return false;
        }

        ITargetable target = currentTarget.GetComponent<ITargetable>();
        target.AddDelayedEffect(new EffectDelayedDamage(this, target, currentDamage, currentAttackEndDelay * 0.5f));
        //GameController.entities[targetTeam].Remove(currentTarget);
        //Destroy(currentTarget);
        //currentTarget = null;

        return true;
    }

    protected void AttackDelay()
    {
        state = State.Attack;
    }

    protected void AttackEndDelay()
    {
        //animator.SetBool("isAttack", false);
        state = State.Search;
    }
}
