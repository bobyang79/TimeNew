using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnClassChose : MonoBehaviour
{
    public Cover cover;
    private int currentLevel;
    public void setLevel(int x, Cover cov) {
        currentLevel = x;
        gameObject.transform.Find("Text").GetComponent<Text>().text = x+"";
        cover = cov;
        
    }

    public void refreshUI() {
        int level = PlayerPrefs.GetInt(Main.keyClass,1);
        if (level < currentLevel) {
            gameObject.transform.Find("stars").gameObject.SetActive(true);
        } else {
            gameObject.transform.Find("stars").gameObject.SetActive(false);
            gameObject.GetComponent<Button>().interactable = false;
        }
        
    }

    public void click() {
        cover.clickCover(currentLevel);
    }
}
