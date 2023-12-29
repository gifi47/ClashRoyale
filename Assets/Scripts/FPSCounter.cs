using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;

    int cnt = 0;
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cnt++;
        time += Time.deltaTime;
        if (time > 1.0)
        {
            text.text = $"FPS {cnt / time:F1}";
            cnt = 0;
            time = 0;
        }
    }
}
