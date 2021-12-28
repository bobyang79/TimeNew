using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Cover : MonoBehaviour
{
    public AudioClip audioClip;
    public GameObject gbClassChose;
    public GridLayoutGroup gridLAyout;
    private bool isClick = false;
    private float timer = 0;
    public GameObject gmClassChose;
    public Main main;
    public void clickCover(int x) {
        isClick = true;
        gmClassChose.SetActive(false);
        gameObject.GetComponent<AudioSource>().PlayOneShot(audioClip);
        main.startNewGame(x);
        Debug.Log("click");
    }
    void Start()
    {
        int i;
        for(i=1;i<=Main.MaxClass;++i) {
            BtnClassChose btn = Instantiate(gbClassChose, gridLAyout.transform).GetComponent<BtnClassChose>();
            btn.setLevel(i,this);
        }
    }
 

    // Update is called once per frame
    void Update()
    {
        if(isClick) {
            timer += Time.deltaTime;
            if( timer <= 1) {
                gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1 - timer);
            }else {
                Destroy(gameObject);
            }
        }
    }
}
