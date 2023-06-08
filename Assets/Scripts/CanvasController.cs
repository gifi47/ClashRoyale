using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    SpawnController spawnController;

    [SerializeField]
    TMP_InputField inputField;


    // Start is called before the first frame update
    void Start()
    {
    }

    public void SpawnEntity()
    {
        try
        {
            string[] rezult = inputField.text.Split(' ');
            Vector3 position = new Vector3(Convert.ToSingle(rezult[0]), Convert.ToSingle(rezult[1]), Convert.ToSingle(rezult[2]));
            spawnController.SpawnEntity(EntityTypeInt.Warrior, position, Team.Blue);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}
