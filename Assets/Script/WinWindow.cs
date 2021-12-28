using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinWindow : MonoBehaviour
{
    // Start is called before the first frame update
    public enum WinType{
        hasNext,
        lastClass
    };

    public Text title;
    public Text description;
    public GameObject nextClass;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showWin(WinType winType) {
        gameObject.SetActive(true);
        gameObject.transform.SetSiblingIndex(100);
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.Play();
        if (winType == WinType.hasNext) {
            description.text = "恭喜通關\n" +
                "點擊進入下一關";
            nextClass.SetActive(true);
        } else {
            description.text = "恭喜通關\n" +
                "目前關卡已全破\n"+"我們將盡快新增新關卡\n";
            nextClass.SetActive(false);
        }
    }
}
