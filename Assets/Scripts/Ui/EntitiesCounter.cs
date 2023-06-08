using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntitiesCounter : MonoBehaviour
{
    TextMeshProUGUI textGUI;
    // Start is called before the first frame update
    void Start()
    {
        textGUI = GetComponent<TextMeshProUGUI>();
    }

    void FixedUpdate()
    {
        textGUI.text = $"{GameController.entities[Team.Red].Count + GameController.entities[Team.Blue].Count} ENTITIES";
    }
}
