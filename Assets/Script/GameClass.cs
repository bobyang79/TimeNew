using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameClass : MonoBehaviour
{
    public Point gameStart;
    public List<Point> doorList;
    public List<Point> []fansList;

    public GameClass(int classNum) {

        doorList = new List<Point>();
        fansList = new List<Point>[2];
        int i,j;
        for (i=0;i< fansList.Length; ++i) {
            fansList[i] = new List<Point>();
        }

        string path = "Assets/Resources/class/class"+classNum+".txt";
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        string s = reader.ReadToEnd();
        Debug.Log(s);
        string []s2 = s.Split('\n');
        Debug.Log("size="+s2.Length);
        for (i = 0; i < s2.Length; ++i) {
            string []s3 = s2[i].Split(',');
            for (j = 0; j < s3.Length; ++j) {
                int x = j;
                int y = s2.Length - i - 1;
                Point point = new Point(x, y);
                switch (s3[j]) {
                    case "S":
                        gameStart = point;
                        Debug.Log("Game start ="+j);
                        break;
                    case "D":
                        doorList.Add(point);
                        break;
                    case "1"://帶去咖啡店的
                        fansList[1].Add(point);
                        break;
                }
                Debug.Log("s3:"+ s3[j]);
            }
        }
        reader.Close();
    }

    public void readFromClass(Point[][] point, Point currentPoint) {
        currentPoint.x = gameStart.x;
        currentPoint.y = gameStart.y;

        int i,j;
        for(i=0;i< doorList.Count;++i) {
            Point p = doorList[i];
            point[p.x][p.y].hasdoor = true;
        }

        for (i = 1; i < fansList.Length; ++i) {//粉絲1,2,3,4,5 代表不同類型
            for (j = 0; j < fansList[i].Count; ++j) {
                Point p = fansList[i][j];
                point[p.x][p.y].fansNum = i;
                point[p.x][p.y].isSatisfy = false;
            }
        }
    }
}
