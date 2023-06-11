using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManagementOverlay : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonUse;

    [SerializeField]
    private GameObject buttonInfo;

    [SerializeField]
    private GameObject textCardName;


    public bool DisplayButtonUse 
    { 
        get 
        { 
            return buttonUse.activeSelf; 
        } 
        set 
        {
            buttonUse.SetActive(value);
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


}
