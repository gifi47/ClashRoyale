using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    TextMeshProUGUI textGUI;
    // Start is called before the first frame update
    void Start()
    {
        textGUI = GetComponent<TextMeshProUGUI>();
    }

    float time = 0;
    int cnt = 0;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        cnt++;
        if (time >= 1)
        {
            textGUI.text = $"{cnt / time:F2} FPS";
            time = 0;
            cnt = 0;
        }
    }
}
