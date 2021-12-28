using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseWindow : MonoBehaviour {
    public AudioClip failedTime;
    public AudioClip failedBump;
    public AudioClip failedBoring;
    public AudioClip failedGirls;
    // Start is called before the first frame update
    public enum LoseType {
        youInSamePlace,
        girlsInSamePlace,
        questFailed,
        bringGrilInDoor,
        noneLose,
    };

    public Text title;
    public Text description;
    public GameObject nextClass;
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void showLose(LoseType loseType) {
        gameObject.SetActive(true);
        gameObject.transform.SetSiblingIndex(100);

        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        

        if (loseType == LoseType.youInSamePlace) {
            title.text = "灰飛煙滅";
            description.text = "兩個宏宏撞在一起\n" + "造成時空錯亂\n" + "請重新再來";
            audioSource.PlayOneShot(failedBump);
        } else if (loseType == LoseType.girlsInSamePlace) {
            title.text = "后宮著火";
            description.text = "兩個女生撞在一起\n" + "後宮著火啦\n"+"請重新再來";
            audioSource.PlayOneShot(failedGirls);
        } else if (loseType == LoseType.questFailed) {
            title.text = "寂寞空閨";
            description.text = "等待你的女生獨守空閨\n" +"沒人陪\n" + "請重新再來";
            audioSource.PlayOneShot(failedBoring);
        } else if (loseType == LoseType.bringGrilInDoor) {
            title.text = "時空裂縫";
            description.text = "你把女生帶進了時空裂縫\n" + "對方迷失在時空裂縫中\n" + "請重新再來";
            audioSource.PlayOneShot(failedTime);
        }
    }
}
