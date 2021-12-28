using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] sprites = new Sprite[5];
    int currentIndex = 0;
    public Image image;
    public Text title;
    public Text desc;

    private string[] titles = { "宏宏", "咖啡店", "曖昧對象", "時空漩渦", "撒驕女友" };
    private int[] classNeed = { 1, 1, 1, 3, 11 };
    private string[] descs = {"從12點出發\n每移動1格為1小時\n需要在9點前完成所有女友的目標即可過關\n",
    "5點關門\n可以帶對象來喝喝咖啡\n\n(11關後)\n可於營業時間購買咖啡給粉絲喝\n",
    "喜歡喝咖啡的對象\n你可以帶她去咖啡店\n她就會很開心給你啾啾",
    "(第3關後)\n\n進入之後會進入正時狀態\n逆時進入後會回到正時\n除了宏宏外的人進到時空漩渦會進入BadEnd\n逆時狀態不能帶人約會喔!!",
    "(第11關後)\n\n不喜歡出門\n想要你幫她外帶咖啡在家恩恩愛愛\n逆時的宏宏也可以攜帶咖啡喔!!"};
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void close() {
        gameObject.SetActive(false);
    }

    public void clickArror(int value) {
        currentIndex += value;
        currentIndex = currentIndex % sprites.Length;
        if(currentIndex < 0) {
            currentIndex += sprites.Length;
        }
        if (Main.thisClass < classNeed[currentIndex]) {
            clickArror(value);
        } else {
            showTutorial(currentIndex);
        }
        
    }

    public void showTutorial(int x) {
        currentIndex = x;
        image.sprite = sprites[x];
        title.text = titles[x];
        desc.text = descs[x];
        gameObject.SetActive(true);
        gameObject.transform.SetSiblingIndex(100);

    }

    public void hideTutorial() {
        gameObject.SetActive(false);
    }
}
