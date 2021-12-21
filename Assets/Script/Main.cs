using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {
    Point[][] point = new Point[maxSzie][];
    int currentTime = 0;
    Point currentPoint = new Point(0,0);
    bool isPositive = true;
    static int maxSzie = 5;

    public Canvas mCanvas;
    public GameObject stepRed;
    public GameObject stepBlue;
    public GameObject chessMain;
    public GameObject gbDoor;
    public GameObject[] gbFans = new GameObject[20];

    private GameClass currentClass;

    public List<Path> pathList = new List<Path>();
    Path currentPath;
    void Start() {
        int i, j;
        for (i = 0; i < maxSzie; ++i) {
            point[i] = new Point[maxSzie];
            for (j = 0; j < maxSzie; ++j) {
                point[i][j] = new Point(i, j);
                //0,1  1,1  2,1  3,1  4,1
                //0,0  1,0  2,0  3,0  4,0
            }
        }
        currentClass = new GameClass(1);
        startNewGame();

        Debug.Log("Start");
    }

    void startNewGame() {
        pathList = new List<Path>();
        currentTime = 0;
        int i, j;
        for (i = 0; i < maxSzie; ++i) {
            for (j = 0; j < maxSzie; ++j) {
                point[i][j].clearAll();
                point[i][j] = new Point(i, j);
            }
        }

        currentClass.readFromClass(point, currentPoint);
        createChess();

        createPath(true);
        currentPath.addPoint(currentPoint, 0);
        isPositive = true;
    }

    private void createChess() {
        chessMain.transform.position = currentPoint.getPosition();
        int i, j;
        for(i=0;i<point.Length;++i) {
            for (j = 0; j < point[i].Length; ++j) {
                if (point[i][j].hasdoor) {
                    GameObject door = Instantiate(gbDoor, mCanvas.transform);
                    door.transform.position = point[i][j].getPosition();
                } else if (point[i][j].fansNum != 0) {
                    Debug.Log("create Fan");
                    GameObject fans = Instantiate(gbFans[point[i][j].fansNum], mCanvas.transform);
                    fans.transform.position = point[i][j].getPosition();
                }
            }
        }
    }

    // Update is called once per frame
    void Update() {

    }

    void createPath(bool isPositive) {
        Path path = new Path(isPositive);
        pathList.Add(path);
        currentPath = path;
        
    }

    public void clickMove(int num) {
        int[] xMove = new int[] { 0, 1, 0, -1 };
        int[] yMove = new int[] { 1, 0, -1, 0 };

        int tX = currentPoint.x + xMove[num];
        int tY = currentPoint.y + yMove[num];


        if (tX < 0 || tX > maxSzie || tY < 0 || tY > maxSzie) {
            return;
        }
        if(isPositive && currentTime == 9 ) {
            return;
        }
        if(!isPositive && currentTime == 0) {
            return;
        }
        currentPoint = point[tX][tY];

        moveOneTime();

        GameObject step = Instantiate(isPositive ? stepRed : stepBlue, mCanvas.transform);
        currentPoint.addStep(step);
        step.transform.Find("Text").GetComponent<Text>().text = "" + currentTime;

        currentPath.addPoint(currentPoint, currentTime);

        if (currentPoint.hasdoor) {
            createPath(isPositive);
            isPositive = !isPositive;
        }
        
        Debug.Log("clickMove:" + currentPoint.x + "," + currentPoint.y + " time=" + currentTime);
    }

    private void moveOneTime() {
        if (isPositive) {
            currentTime++;
        }
        else {
            currentTime--;
        }
    }

    private void moveBackOneTime() {
        if (isPositive) {
            currentTime--;
        }
        else {
            currentTime++;
        }
    }

    public void clickBack() {
        /*
        if((pathList.Count == 1) && pathList[0].list.Count == 1) {
            return;
        }

        if (currentPoint.hasdoor) {
            isPositive = !isPositive;
        }
        if (currentPath.isEmpty()) {
            pathList.RemoveAt(pathList.Count - 1);
            currentPath = pathList[pathList.Count - 1];
        }

        moveBackOneTime();
        Destroy(currentPoint.getLastObject());
        currentPoint.removeStep();
        currentPath.popLast();

        if (currentPath.isEmpty()) {
            pathList.RemoveAt(pathList.Count - 1);
            currentPath = pathList[pathList.Count - 1];
        }
        currentPoint = currentPath.getLastPoint();

        Debug.Log("path=" + currentPath.list.Count + ",point=" + currentPoint.x + "," + currentPoint.y);*/
        startNewGame();
    }
}
