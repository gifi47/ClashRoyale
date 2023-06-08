using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Building : MonoBehaviour, ITargetable
{
    [SerializeField]
    private BuildingType stats;

    [SerializeField]
    private Projectile projectile;

    [SerializeField]
    private Vector3 offset;

    // =====================================
    // =====================================

    // CURRENT STATS VALUES
    protected float currentHealth;
    protected float currentAttackDelay;
    protected float currentAttackEndDelay;
    protected float currentAttackRange;
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
    //protected NavMeshAgent agent;
    //protected Animator animator;

    protected void ResetStats()
    {
        currentHealth = stats.baseHealth;
        currentAttackDelay = stats.baseAttackDelay;
        currentAttackEndDelay = stats.baseAttackEndDelay;
        currentAttackRange = stats.baseAttackRange;
        currentDamage = stats.baseDamage;
    }

    void Start()
    {
        ResetStats();
        if (team == Team.Blue)
            GetComponent<MeshRenderer>().material = GameController.blue;
        else
            GetComponent<MeshRenderer>().material = GameController.red;
        state = State.Search;
        //agent = GetComponent<NavMeshAgent>();
        //animator = GetComponent<Animator>();
    }

    public void AddEffect(IEffect effect)
    {
        effect.OnStart();
    }

    public void AddDelayedEffect(IDelayedEffect effect)
    {
        StartCoroutine(effect.OnDelay());
    }

    public void ReceiveDamage(float damageValue)
    {
        float newHealth = currentHealth - damageValue;
        if (newHealth <= 0)
        {
            GameController.entities[team].Remove(this.gameObject);
            Destroy(this.gameObject);
        }
        else
        {
            currentHealth = newHealth;
        }
    }

    void Update()
    {
        switch (state)
        {
            case State.Search:
                if (UpdateTarget())
                {
                    state = State.Wait;
                    //agent.isStopped = true;
                    //animator.SetBool("isWalk", false);
                    //animator.SetBool("isAttack", true);
                    Invoke("AttackDelay", currentAttackDelay);
                }
                break;
            case State.Wait:
                break;
            case State.Attack:
                if (Attack())
                {
                    state = State.Wait;
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
        // check if a current target in attack radius
        if (currentTarget != null)
        {
            if (Vector3.Distance(currentTarget.transform.position, this.transform.position) < currentAttackRange){
                return true;
            }
            currentTarget = null;
        }

        // find closest entity and make it current target
        float minDist = currentAttackRange;
        foreach (GameObject entity in GameController.entities[targetTeam])
        {
            float dist = Vector3.Distance(entity.transform.position, this.transform.position);
            if ((dist < currentAttackRange) && (dist < minDist))
            {
                currentTarget = entity;
                minDist = dist;
            }
        }
        return (currentTarget != null);
    }

    protected virtual bool Attack()
    {
        // if target already dead or escaped attack radius -> cancel attack
        if ((currentTarget == null) || (Vector3.Distance(currentTarget.transform.position, this.transform.position) > currentAttackRange))
        {
            return false;
        }

        Projectile.Launch(Instantiate(projectile, this.transform.position + offset, Quaternion.identity), 
            currentTarget, 1.6f, new EffectDamage(this, currentTarget.GetComponent<ITargetable>(), currentDamage));
        

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