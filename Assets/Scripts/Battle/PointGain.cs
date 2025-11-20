using UnityEngine;
using TMPro;
using System.Collections;
public class PointGain : MonoBehaviour
{
    bool grow;
    TextMeshPro text;

    public string points;
    public Color textColor;
    void Start()
    {
        textColor = Color.red;
        points = "Stronger than you";
        text = GetComponent<TextMeshPro>();
        text.text = points;
        text.color = textColor;
        grow = true;
        StartCoroutine(startShrinking());
        StartCoroutine(endObject());
    }

    // Update is called once per frame
    void Update()
    {
        if (grow)
            transform.localScale = new Vector3(transform.localScale.x*1.005f, transform.localScale.y*1.005f, transform.localScale.z);
        else
            transform.localScale = new Vector3(transform.localScale.x * 0.99f, transform.localScale.y * 0.99f, transform.localScale.z);
    }

    IEnumerator startShrinking()
    {
        yield return new WaitForSeconds(0.5f);
        grow = false;
    }

    IEnumerator endObject()
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(transform.gameObject);
    }
}
