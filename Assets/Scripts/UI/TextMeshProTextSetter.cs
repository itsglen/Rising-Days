using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMeshProTextSetter : MonoBehaviour
{
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(object eventData)
    {
        Debug.Log(eventData);
        text.text = eventData.ToString();
    }

}
