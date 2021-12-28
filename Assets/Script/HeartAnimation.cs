using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeartAnimation : MonoBehaviour
{
    float timer = 0;
    float sx, sy;
    void Start() {
        sx = gameObject.transform.position.x;
        sy = gameObject.transform.position.y;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        gameObject.GetComponent<RawImage>().color = new Color(1, 1, 1, 2.0f - timer);
        if (timer > 1.5f) {
            Object.Destroy(gameObject);
            return;
        }
        float x = sx + timer * 60f + 30 * Mathf.Sin(6.28f * timer);
        float y = sy + timer * 150f;
        gameObject.transform.position = new Vector3(x, y, 0);
        
    }
}
