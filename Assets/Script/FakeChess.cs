using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FakeChess : MonoBehaviour
{
    // Start is called before the first frame update
    Texture2D positive;
    Texture2D negative;
    public Main main;
    float timer = 0;
    void Start()
    {
        positive = Resources.Load<Texture2D>("icons/main");
        negative = Resources.Load<Texture2D>("icons/mainB");
    }
    /*
    public void updateUI(bool isPositive) {
        if(isPositive) {
            gameObject.GetComponent<Raw>
        } else {
            
        }
    }*/

    void Update() {
        timer += Time.deltaTime;
        float r = 1.0f + 0.1f * Mathf.Sin(timer * 3);
        gameObject.transform.localScale = new Vector3(r , r, 1);
        if(main.isPositive) {
            gameObject.GetComponent<RawImage>().texture = positive;
        }else {
            gameObject.GetComponent<RawImage>().texture = negative;

        }
    }
}
