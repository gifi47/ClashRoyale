using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextFading : MonoBehaviour
{
    TextMeshProUGUI text;
    Material mat;
    public float maxFontSize = 30;
    float minFontSize;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        mat = new Material(text.fontSharedMaterial);
        minFontSize = text.fontSize;
    }

    float time;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float xd = Mathf.Abs(Mathf.Sin(time * 0.3f));
        text.fontMaterial = mat;
        text.fontMaterial.SetFloat("_OutlineSoftness", xd);
        text.fontSize = minFontSize + xd * 4;
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("BattleArena");
        }
    }
}
