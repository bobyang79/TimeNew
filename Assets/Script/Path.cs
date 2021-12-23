using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Point> pointlist = new List<Point>();
    public List<int> timeList = new List<int>();
    public List<GameObject> stepList = new List<GameObject>();
    bool isPositive;

    public Path(bool ispos) {
        isPositive = ispos;
    }
    public void addPoint(Point point, int T, GameObject step) {
        pointlist.Add(point);
        timeList.Add(T);
        stepList.Add(step);
    }

    public void popLast() {
        pointlist.RemoveAt(pointlist.Count - 1);
        timeList.RemoveAt(pointlist.Count - 1);
        stepList.RemoveAt(pointlist.Count - 1);
    }

    public bool isEmpty() {
        return pointlist.Count == 0;
    }

    public Point getLastPoint() {
        return pointlist[pointlist.Count - 1];
    }

    public void reverseList() {
        List<Point> list2 = new List<Point>();
        List<int> timeList2 = new List<int>();
        List<GameObject> stepList2 = new List<GameObject>();
        int i;
        for (i = pointlist.Count-1; i >=0; --i) {
            list2.Add(pointlist[i]);
            timeList2.Add(timeList[i]);
            stepList2.Add(stepList[i]);
        }
        pointlist = list2;
        timeList = timeList2;
        stepList = stepList2;
    }

    public void destorystep(int index) {
        if (index >= 0 && stepList[index] != null)
            Destroy(stepList[index]);
    }
}
