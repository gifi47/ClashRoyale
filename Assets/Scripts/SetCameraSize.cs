using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraSize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera cam = GetComponent<Camera>();
        cam.orthographicSize += (2 - (Screen.width / Screen.height));
    }
}
