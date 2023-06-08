using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;

public class EntityWarrior : Entity
{
    [SerializeField]
    private TextMeshPro textMeshPro;

    [SerializeField]
    private SkinnedMeshRenderer[] teamColorsBodyParts;

    private new void Start()
    {
        base.Start();
        if (textMeshPro == null)
            textMeshPro = GetComponentInChildren<TextMeshPro>();
        ParentConstraint healthTextConstraint = textMeshPro.gameObject.GetComponent<ParentConstraint>();
        textMeshPro.gameObject.transform.parent = null;
        ConstraintSource constraintSource = new ConstraintSource();
        constraintSource.weight = 1;
        constraintSource.sourceTransform = this.gameObject.transform;
        healthTextConstraint.AddSource(constraintSource);
        
        healthTextConstraint.constraintActive = true;

        healthTextConstraint.SetTranslationOffset(0, new Vector3(0, 0.65f, 0));
        textMeshPro.text = $"{currentHealth}/{stats.maxHealth}";
        foreach (SkinnedMeshRenderer meshRenderer in teamColorsBodyParts)
        {
            if (team == Team.Red)
                meshRenderer.material = GameController.red;
            else if (team == Team.Blue)
                meshRenderer.material = GameController.blue;
        }
    }

    public override void ReceiveDamage(float damageValue)
    {
        float newHealth = currentHealth - damageValue;
        if (newHealth <= 0)
        {
            GameController.entities[team].Remove(this.gameObject);
            Destroy(textMeshPro.gameObject);
            Destroy(this.gameObject);
        }
        else
        {
            currentHealth = newHealth;
            textMeshPro.text = $"{currentHealth}/{stats.maxHealth}";
        }
    }

    private new void Update()
    {
        base.Update();
    }
}
