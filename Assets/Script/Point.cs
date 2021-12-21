using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    public int x;
    public int y;
    public int r = 213;

    public bool hasdoor = false;
    public List<GameObject> list = new List<GameObject>();
    public bool isSatisfy = true;
    public int fansNum = 0;

    public Point(int x,int y) {
        this.x = x;
        this.y = y;
    }

    public Vector3 getPosition(float x2 = 0, float y2 = 0) {
        return new Vector3((x-2)*r + 540 + x2,
            (y-2)* r + 960 + y2,
            0);
    }

    public void addStep(GameObject gameObject) {
        list.Add(gameObject);
        refreshUI();
    }

    public GameObject getLastObject() {
        return list[list.Count - 1];
    }

    public void removeStep() {
        list.RemoveAt(list.Count - 1);
        refreshUI();
    }

    private void refreshUI() {
        if (list.Count == 1) {
            list[0].transform.position = getPosition();
            return;
        }
        int i;

        for (i = 0; i < list.Count; ++i) {
            float x2 = r / 5 * Mathf.Cos(6.28f * i / list.Count);
            float y2 = r / 5 * Mathf.Sin(6.28f * i / list.Count);
            Debug.Log("x2=" + x2 + ",y2=" + y2);
            list[i].transform.position = getPosition(x2, y2);
        }
    }

    public void clearAll() {
        int i;
        for(i=0;i< list.Count; ++i) {
            Object.Destroy(list[i]);
        }
    }
}
