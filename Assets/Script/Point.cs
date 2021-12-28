using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    public int x;
    public int y;
    

    public List<GameObject> stepList = new List<GameObject>();
    public bool isSatisfy = true;
    public int fansNum = 0;
    public int buildingNum = 0;

    public GameObject chess;

    public bool hasdoor() {
        return buildingNum == 1;
    }

    public Point(int x,int y) {
        this.x = x;
        this.y = y;
        fansNum = 0;
        buildingNum = 0;
       
    }

    public Vector3 getPosition(float x2 = 0, float y2 = 0) {
        int r = 213;
        float rate = Mathf.Min(Screen.width / 1080f, Screen.height / 1920f);
        return new Vector3((x-2)*r*rate + Screen.width/2 + x2,
            (y-2)* r * rate+ Screen.height/2 + y2,
            0);
    }

    public void addStep(GameObject gameObject) {
        stepList.Add(gameObject);
        refreshUI();
    }

    public GameObject getLastObject() {
        return stepList[stepList.Count - 1];
    }

    public void removeStep() {
        stepList.RemoveAt(stepList.Count - 1);
        refreshUI();
    }

    private void refreshUI() {
        if (stepList.Count == 1) {
            stepList[0].transform.position = getPosition();
            return;
        }
        int i;
        float rate = Mathf.Min(Screen.width / 1080f, Screen.height / 1920f);
        float r = 42 * rate;
        for (i = 0; i < stepList.Count; ++i) {
            float x2 = r * Mathf.Cos(6.28f * i / stepList.Count);
            float y2 = r * Mathf.Sin(6.28f * i / stepList.Count);
            Debug.Log("x2=" + x2 + ",y2=" + y2);
            stepList[i].transform.position = getPosition(x2, y2);
        }
    }

    public void clearAll() {
        int i;
        for(i=0;i< stepList.Count; ++i) {
            Object.Destroy(stepList[i]);
        }
    }

    public void finishTheFans() {
        fansNum = 0;
        chess.SetActive(false);
    }
}
