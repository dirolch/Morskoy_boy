using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hels : MonoBehaviour
{

    public GameObject HelsChank,
                      GamePole;

    GameObject[] HelsBar = new GameObject[20];


    void CreateHelBar()
    {
        Vector3 GetPositionScreen = this.transform.position;

        float DX = 0.5f;

        for (int I = 0; I < 20; I++)
        {
            HelsBar[I] = Instantiate(HelsChank) as GameObject;


            HelsBar[I].transform.position = GetPositionScreen;

            GetPositionScreen.x += DX;

        }
    }


    void RefreshHels()
    {
        int L = 0;
        for (int I = 0; I < 20; I++) HelsBar[I].GetComponent<Chanks>().index = 0;

        if (GamePole != null) L = GamePole.GetComponent<GamePole>().LifeShip();

        for (int I = 0; I < L; I++) HelsBar[I].GetComponent<Chanks>().index = 1;




    }


    // Start is called before the first frame update
    void Start()
    {
        if  (HelsChank != null) CreateHelBar();

    }

    // Update is called once per frame
    void Update()
    {

        if ((GamePole != null) && (HelsChank != null)) RefreshHels();
        
    }
}
