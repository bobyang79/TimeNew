using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Toast : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 startVector;
    float timer = 1;
    Color color;
    void Start()
    {
        startVector = gameObject.transform.position;
        color = GetComponent<Text>().color;
        color.a = 0;
    }

    public void showToast(string s) {
        timer = 0;
        GetComponent<Text>().text = s;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        gameObject.transform.position = new Vector3(startVector.x, startVector.y + timer * 30, 0);
        gameObject.GetComponent<Text>().color = new Color(color.r, color.g, color.b, 1 - timer/5);
    }
}
