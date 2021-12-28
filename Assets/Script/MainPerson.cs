using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPerson : MonoBehaviour
{
    public int bringThing = 0;
    public int bringFan = 0;

    float timer = 0;

    public static float timeMove = 0.5f;

    public Point currentPoint, nextPoint;
    public bool isPositive = true;
    public MainPerson(bool isPos) {
        isPositive = isPos;
    }

    public void setPoint(Point p) {
        gameObject.transform.position = p.getPosition();
        nextPoint = p;
        currentPoint = p;
    }

    public void setNextPoint(Point p) {
        timer = 0;
        if(nextPoint != null)
            currentPoint = nextPoint;
        nextPoint = p;
    }

    public void addFans(int num) {
        bringFan = num;
        GameObject child = gameObject.transform.Find("fans").gameObject;
        child.GetComponent<RawImage>().texture = Resources.Load<Texture2D>("icons/fans" + num);
        child.SetActive(true);
    }

    public void addThings(int num) {
        bringThing = num;
        GameObject child = gameObject.transform.Find("things").gameObject;
        //child.GetComponent<RawImage>().texture = Resources.Load<Texture2D>("icons/fans" + num);
        child.SetActive(true);
    }

    public void finishFans() {
        bringFan = 0;
        GameObject child = gameObject.transform.Find("fans").gameObject;
        child.SetActive(false);
    }

    public void finishThings() {
        bringFan = 0;
        GameObject child = gameObject.transform.Find("things").gameObject;
        child.SetActive(false);
    }
        
    public void doTheAction() {

    }

    public bool inSamePoint(Point p) {
        if (nextPoint.x == p.x && nextPoint.y == p.y)
            return true;
        return false;
    }

    // Update is called once per frame
    void Update() {
       // Debug.Log("time=" + timer);
        if (currentPoint == null || nextPoint == null) {
            //Debug.Log("reutnr null");
            return;
        }
        timer += Time.deltaTime;
        

        if(timer > timeMove) {
            timer = timeMove;
        }

        float r = (timeMove - timer) / timeMove;
        float x = currentPoint.getPosition().x * r + nextPoint.getPosition().x * (1 - r);
        float y = currentPoint.getPosition().y * r + nextPoint.getPosition().y * (1 - r);

        gameObject.transform.position = new Vector3(x, y, 0);
    }
}
