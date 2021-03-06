using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameClass : MonoBehaviour
{
    public Point gameStart;
    public List<Point> []doorList;
    public List<Point> []fansList;

    public GameClass(int classNum) {

        doorList = new List<Point>[3];//1 -> Door 2-> Coffee
        fansList = new List<Point>[3];
        int i,j;
        for (i = 0; i < doorList.Length; ++i) {
            doorList[i] = new List<Point>();
        }
        for (i=0;i< fansList.Length; ++i) {
            fansList[i] = new List<Point>();
        }
        string path = "class/class" + classNum ;
        Debug.Log("file=" + path);
        //string path = "/Assets/Resources/class/class"+classNum+".txt";
        //Read the text from directly from the test.txt file
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        //StreamReader reader = new StreamReader(path);
        //string s = reader.ReadToEnd();
        Debug.Log(textAsset);
        string []s2 = textAsset.text.Split('\n');
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
                        doorList[1].Add(point);
                        break;
                    case "C":
                        doorList[2].Add(point);
                        break;
                    case "1"://帶去咖啡店的
                        fansList[1].Add(point);
                        break;
                    case "2"://帶咖啡過去的
                        fansList[2].Add(point);
                        break;
                }
            }
        }
        //reader.Close();
    }

    public void readFromClass(Point[][] point,out Point currentPoint) {
        int i,j;
        currentPoint = point[gameStart.x][gameStart.y];
        for (i = 1; i < doorList.Length; ++i) {//1> Door , 2>Coffee
            for (j = 0; j < doorList[i].Count; ++j) {
                Point p = doorList[i][j];
                point[p.x][p.y].buildingNum = i;
            }
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
