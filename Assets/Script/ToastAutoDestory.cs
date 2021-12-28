using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToastAutoDestory : MonoBehaviour {
    // Start is called before the first frame update
    Vector3 startVector;
    float timer = 1;
    Color color;

    public void setPosition(Vector3 v) {
        startVector = v;
    }

    void Start() {
        startVector = gameObject.transform.position;
        color = GetComponent<Text>().color;
        color.a = 0;
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        gameObject.transform.position = new Vector3(startVector.x, startVector.y + timer * 30, 0);
        gameObject.GetComponent<Text>().color = new Color(color.r, color.g, color.b, 1 - timer / 5);
        if(timer >= 5f) {
            Object.Destroy(gameObject);
        }
    }
}
