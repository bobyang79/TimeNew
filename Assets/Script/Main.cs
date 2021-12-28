using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {
    Point[][] point = new Point[maxSzie][];
    int currentTime = 0;
    Point currentPoint;
    public bool isPositive = true;
    static int maxSzie = 5;

    public Canvas mCanvas;
    public GameObject stepRed;
    public GameObject stepBlue;
    public GameObject chessMain;
    public GameObject FakeChess;
    public GameObject heart;
    public GameObject[] gbBullds = new GameObject[20];
    public GameObject[] gbFans = new GameObject[20];
    public GameObject winWindow;
    public GameObject loseWindow;
    public Toast toast;
    public Text txDay, tvTime;
    public Tutorial tutorial;
    public ToastAutoDestory toastAutoDestory;

    private GameClass currentClass;

    public List<Path> pathList = new List<Path>();
    public MainPerson[] mainChess;
    Path currentPath;

    enum GameState {Plan, Start, win, lose};
    GameState currentState = GameState.Plan;

    List<GameObject> allChest = new List<GameObject>();
    public static int thisClass = 2;
    public static int MaxClass = 20;
    public static string keyClass = "classLevel";
    void Start() {
        int i, j;
        thisClass = PlayerPrefs.GetInt(keyClass, 1);
        //thisClass = 5;
        for (i = 0; i < maxSzie; ++i) {
            point[i] = new Point[maxSzie];
            for (j = 0; j < maxSzie; ++j) {
                point[i][j] = new Point(i, j);
                //0,1  1,1  2,1  3,1  4,1
                //0,0  1,0  2,0  3,0  4,0
            }
        }

        
        startNewGame(thisClass);
        
        Debug.Log("Start");
    }
    void updateTime(int t) {
        tvTime.text = "0" + t + ":00";
    }

    public void startNewGame(int classLevel) {
        int i, j;
        thisClass = classLevel;
        currentClass = new GameClass(classLevel);

        hideWindow();

        currentState = GameState.Plan;
        pathList = new List<Path>();
        currentTime = 0;
        txDay.text = "<i>Day" + classLevel+"</i>";
        updateTime(0);
        for (i = 0; i < maxSzie; ++i) {
            for (j = 0; j < maxSzie; ++j) {
                point[i][j].clearAll();
                point[i][j] = new Point(i, j);
            }
        }

        for(i=0;i< mainChess.Length; ++i) {
            if(mainChess[i]!=null)
                Destroy(mainChess[i].gameObject);
        }

        isPositive = true;

        currentClass.readFromClass(point, out currentPoint);
        //currentPoint = point[currentClass.gameStart.x][currentClass.gameStart.y];


        createPath(true);
        
        createChess();
        FakeChess.transform.SetSiblingIndex(50);
        createAStep();

        showTutorialIfNeed();
    }

    public void TimeGo(int time) {
        int i, j;
        for (i = 0; i < pathList.Count; ++i) {
            for (j = 0; j < pathList[i].timeList.Count; ++j) {

                if (pathList[i].timeList[j] == time) {
                    if (j == 0) { // Create New One
                        GameObject person = Instantiate(chessMain, mCanvas.transform);
                        mainChess[i] = person.GetComponent<MainPerson>();
                        mainChess[i].setPoint(pathList[i].pointlist[j]);
                        mainChess[i].isPositive = (i % 2) == 0;
                    } else { // Move To next
                        mainChess[i].setNextPoint(pathList[i].pointlist[j]);
                    }

                    pathList[i].destorystep(j-1);
                }
            }

        }
    }

    private void showLoves(Point p) {
        GameObject gbHeart = Instantiate(heart, mCanvas.transform);
        gbHeart.transform.position = p.getPosition(60f,60f);
    }
    private void showTutorialIfNeed() {
        Debug.Log("class = " + thisClass);
        if (thisClass == 1) {
            tutorial.showTutorial(0);
        }
        else if (thisClass == 3) {
            tutorial.showTutorial(3);
        }
        else if (thisClass == 11) {
            tutorial.showTutorial(4);
        } else {
            tutorial.hideTutorial();
        }
    }

    public void changeClass(int num) {
        
        thisClass += num;
        if (thisClass < 0)
            thisClass = 0;

        startNewGame(thisClass);
        showTutorialIfNeed();


    }

    void checkCurrentResult(int currentTime) {
        int i;
        for(i=0;i<mainChess.Length;++i) {
            if(mainChess[i] == null) {
                continue;
            }

            Point target = mainChess[i].nextPoint;
            Debug.Log("i=" + i + ",time=" + currentTime + ",x=" + target.x + ",y=" + target.y+ ",fansNum="+ target.fansNum);
            if (target.fansNum == 1) { // 帶去喝咖啡

                if (mainChess[i].bringFan != 0) {//後宮起火
                    showLose(LoseWindow.LoseType.girlsInSamePlace);
                    return;
                }
                if(!mainChess[i].isPositive) {
                    createDestoryToast(target);
                    continue;
                }

                target.finishTheFans();
                mainChess[i].addFans(1);
            } else if (target.fansNum == 2) { //帶咖啡去
                if (mainChess[i].bringFan != 0) {//後宮起火
                    showLose(LoseWindow.LoseType.girlsInSamePlace);
                    return;
                }
                if(mainChess[i].bringThing == 1) {//帶咖啡
                    target.finishTheFans();
                    mainChess[i].finishThings();
                    showLoves(target);
                }

            } else if (target.buildingNum == 2 && currentTime <= 5) { // Coffee
                if (mainChess[i].bringFan == 1) {
                    mainChess[i].finishFans();
                    showLoves(target);
                }
                if(mainChess[i].bringThing == 0) {
                    mainChess[i].addThings(1);//add Coffee
                }
            } else if (target.buildingNum == 1) { //Door
                if (mainChess[i].bringFan != 0) {// 時空旅行
                    showLose(LoseWindow.LoseType.bringGrilInDoor);
                    return;
                }
            }
        }

        if (inSamePlace()) {
            showLose(LoseWindow.LoseType.youInSamePlace);
            return;
        }

        if (!hasMoreAction(currentTime)) {
            if (winTheGame()) {
                showWin();
            } else {
                showLose(LoseWindow.LoseType.questFailed);
            }
        }

    }

    private void showLose(LoseWindow.LoseType type) {
        currentState = GameState.lose;
        loseWindow.GetComponent<LoseWindow>().showLose(type);
        currentState = GameState.lose;
    }

    private void showWin() {
        loseWindow.SetActive(false);
        currentState = GameState.win;
        if (thisClass < MaxClass) {
            winWindow.GetComponent<WinWindow>().showWin(WinWindow.WinType.hasNext);
            PlayerPrefs.SetInt(keyClass, Mathf.Max(thisClass + 1, PlayerPrefs.GetInt(keyClass)));
        }
        else {
            winWindow.GetComponent<WinWindow>().showWin(WinWindow.WinType.lastClass);
        }
    }
    private bool hasMoreAction(int time) {
        int i, j;
        for (i = 0; i < pathList.Count; ++i) {
            for (j = 0; j < pathList[i].timeList.Count; ++j) {
                if (pathList[i].timeList[j] >= time) { // 有更後面的時間, 回傳沒事
                    return true;
                }
            }
        }
        return false;
    }

    private void showWindow(bool isWin) {

        if (isWin) {
            winWindow.GetComponent<WinWindow>().showWin(WinWindow.WinType.hasNext);
        }
    }

    private bool inSamePlace() {
        int i, j;
        for (i = 0; i < mainChess.Length - 1; ++i) {
            if (mainChess[i] == null) continue;
            if (mainChess[i].nextPoint.hasdoor() == true) continue;
            for (j=i+1;j< mainChess.Length; ++j) {
                if (mainChess[j] == null) continue;
                if (mainChess[i].inSamePoint(mainChess[j].nextPoint)) {
                    return true;
                }
            }
        }
        return false;
    }

    private bool winTheGame() {
        int i, j;
        for (i = 0; i < point.Length; ++i) {
            for (j = 0; j < point[i].Length; ++j) {
                if(point[i][j].fansNum != 0) {
                    return false;
                }
            }
        }
        for(i=0;i< mainChess.Length; ++i) {
            if(mainChess[i].bringFan != 0) {
                return false;
            }
        }
        return true;
    }

    IEnumerator runResult() {
        int i;
        for (i = 0; i <= 9; ++i) {
            Debug.Log("runResult"+i+ ",currentState="+ currentState);
            if (currentState != GameState.Start) {
                yield return null;
            } else {
                updateTime(i);
                TimeGo(i);
                yield return new WaitForSeconds(0.5f);
                checkCurrentResult(i);
                yield return new WaitForSeconds(0.5f);
            }
        }

        if(currentState == GameState.Start) {
            if (winTheGame()) {
                showWin();
            }
            else {
                showLose(LoseWindow.LoseType.questFailed);
            }
        }

        yield return null;
    }

    public void clickStart() {
        int i,j;
        if(currentState != GameState.Plan) {
            return;
        }
        currentState = GameState.Start;

        for (i=1;i<pathList.Count; i+=2) {
            pathList[i].reverseList();
        }

        mainChess = new MainPerson[pathList.Count];

        StartCoroutine(runResult());
        FakeChess.SetActive(false);
    }

    private void createChess() {
        FakeChess.SetActive(true);
        FakeChess.transform.position = currentPoint.getPosition();
        int i, j;

        for(i=0;i< allChest.Count; ++i) {
            Destroy(allChest[i]);
        }

        for(i=0;i<point.Length;++i) {
            for (j = 0; j < point[i].Length; ++j) {
                if (point[i][j].buildingNum != 0) {
                    GameObject building = Instantiate(gbBullds[point[i][j].buildingNum], mCanvas.transform);
                    building.transform.position = point[i][j].getPosition();
                    point[i][j].chess = building;

                    allChest.Add(building);
                } else if (point[i][j].fansNum != 0) {
                    
                    GameObject fans = Instantiate(gbFans[point[i][j].fansNum], mCanvas.transform);
                    fans.transform.position = point[i][j].getPosition();
                    point[i][j].chess = fans;

                    allChest.Add(fans);
                }
            }
        }
    }

    void createPath(bool isPositive) {
        Path path = new Path(isPositive);
        pathList.Add(path);
        currentPath = path;
        
    }

    public void clickMove(int num) {
        if (currentState != GameState.Plan) {
            return;
        }

        int[] xMove = new int[] { 0, 1, 0, -1 };
        int[] yMove = new int[] { 1, 0, -1, 0 };

        int tX = currentPoint.x + xMove[num];
        int tY = currentPoint.y + yMove[num];


        if (tX < 0 || tX > maxSzie || tY < 0 || tY > maxSzie) {
            toast.showToast("無法穿越地圖");
            return;
        }
        if(isPositive && currentTime == 9 ) {
            toast.showToast("已經9點了\n無法再規劃行程");
            return;
        }
        if(!isPositive && currentTime == 0) {
            toast.showToast("已經12點了\n無法再逆時行程");
            return;
        }
        currentPoint = point[tX][tY];
        FakeChess.transform.position = new Vector3(currentPoint.getPosition().x, currentPoint.getPosition().y, 1);

        moveOneTime();

        createAStep();

        if (currentPoint.hasdoor()) {
            createPath(isPositive);
            isPositive = !isPositive;
        }
        
        Debug.Log("clickMove:" + currentPoint.x + "," + currentPoint.y + " time=" + currentTime);
    }

    private void createAStep() {
        GameObject step = Instantiate(isPositive ? stepRed : stepBlue, mCanvas.transform);
        currentPoint.addStep(step);
        step.transform.SetSiblingIndex(70);
        step.transform.Find("Text").GetComponent<Text>().text = "" + currentTime;
        currentPath.addPoint(currentPoint, currentTime, step);
    }

    private void createDestoryToast(Point point) {
        GameObject obj = Instantiate(toastAutoDestory.gameObject, mCanvas.transform);
        obj.transform.position = point.getPosition();
        obj.transform.SetSiblingIndex(300);
    }

    private void moveOneTime() {
        if (isPositive) {
            currentTime++;
        }
        else {
            currentTime--;
        }
        updateTime(currentTime);
    }

    private void moveBackOneTime() {
        if (isPositive) {
            currentTime--;
        }
        else {
            currentTime++;
        }
    }

    public void clickRestart() {
        startNewGame(thisClass);
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
        if(currentState == GameState.Plan)
            startNewGame(thisClass);
    }

    private void hideWindow() {
        winWindow.SetActive(false);
        loseWindow.SetActive(false);
    }
}
