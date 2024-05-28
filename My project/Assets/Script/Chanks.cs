using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chanks : MonoBehaviour
{

    public Sprite[] imgs;


    public int index = 0;

    public bool HideChank = false;
    void ChangeImgs()
    {
        if (imgs.Length > index)
        {
            if((HideChank) && (index==1)) GetComponent<SpriteRenderer>().sprite = imgs[0];
            else
            GetComponent<SpriteRenderer>().sprite = imgs[index];
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        ChangeImgs();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeImgs();
    }
}
