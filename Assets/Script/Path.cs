using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Point> list = new List<Point>();
    List<int> timeList = new List<int>();
    bool isPositive;

    public Path(bool ispos) {
        isPositive = ispos;
    }
    public void addPoint(Point point, int T) {
        list.Add(point);
        timeList.Add(T);
    }

    public void popLast() {
        list.RemoveAt(list.Count - 1);
        timeList.RemoveAt(list.Count - 1);
    }

    public bool isEmpty() {
        return list.Count == 0;
    }

    public Point getLastPoint() {
        return list[list.Count - 1];
    }
}
