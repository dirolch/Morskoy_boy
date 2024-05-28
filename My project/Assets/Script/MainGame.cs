using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{

    public int GameMode = 0;

    public GameObject PlayerPole, ComputerPole, Player;



    bool whosMove = true;

     void OnGUI()
    {



        float CentrScreenX = Screen.width / 2;
        float CentrScreenY = Screen.height / 2;

        Rect LocationButton;
        Camera cam;

        GamePole PlayerPoleControl = PlayerPole.GetComponent<GamePole>();


        switch (GameMode)
        {

            case 0:
               cam = GetComponent<Camera>();

                cam.orthographicSize = 8;

                this.transform.position = new Vector3(0, 0, -10);

                LocationButton = new Rect(new Vector2(CentrScreenX - 150, CentrScreenY - 50), new Vector2(300, 200));

                GUI.Box(LocationButton, "");

                LocationButton = new Rect(new Vector2(CentrScreenX - 40, CentrScreenY - 40), new Vector2(200, 30));

                GUI.Label(LocationButton, "МЕНЮ ИГРЫ");

                LocationButton = new Rect(new Vector2(CentrScreenX - 100, CentrScreenY), new Vector2(200, 30));

                if (GUI.Button(LocationButton,"СТАРТ"))
                {
                    GameMode = 1;
                }

                LocationButton = new Rect(new Vector2(CentrScreenX - 100, CentrScreenY +40), new Vector2(200, 30));

                if (GUI.Button(LocationButton, "ВЫХОД"))
                {
                    Application.Quit();
                }

                 

                break;

            case 1:

                cam = GetComponent<Camera>();

                cam.orthographicSize = 10;

                this.transform.position = new Vector3(30, 0, -10);


                LocationButton = new Rect(new Vector2(CentrScreenX - 150, 0), new Vector2(300, 200));

                GUI.Box(LocationButton, "");

                LocationButton = new Rect(new Vector2(CentrScreenX - 10, 10), new Vector2(200, 30));

                GUI.Label(LocationButton, "ДОК");

                LocationButton = new Rect(new Vector2(CentrScreenX - 100, 50), new Vector2(200, 30));

                if (GUI.Button(LocationButton, "Вернуться в меню"))
                {
                    PlayerPoleControl.ClearPole();
                    GameMode = 0;
                }

                LocationButton = new Rect(new Vector2(CentrScreenX - 100, 90), new Vector2(200, 30));

                if (GUI.Button(LocationButton, "Разместить флот"))
                {
                    PlayerPoleControl.RandomShip();
                  
                }

                if (PlayerPoleControl.LifeShip()==20)
                {
                    LocationButton = new Rect(new Vector2(CentrScreenX - 100, 130), new Vector2(200, 30));
                    if (GUI.Button(LocationButton, "В бой"))
                    { 
                        GameMode = 3;


                        PlayerPole.GetComponent<GamePole>().CopyPole();

                        ComputerPole.GetComponent<GamePole>().RandomShip();
                    }





                }

                break;

            case 3:

                this.transform.position = new Vector3(30, -30, -10);


                cam = GetComponent<Camera>();

                cam.orthographicSize = 14;



                break;

            case 4:

                this.transform.position = new Vector3(120, 0, -10);

                LocationButton = new Rect(new Vector2(CentrScreenX - 150, 0), new Vector2(300, 200));
                GUI.Box(LocationButton, "");

                LocationButton = new Rect(new Vector2(CentrScreenX - 10, 10), new Vector2(200, 30));
                GUI.Label(LocationButton, "Меню");

                LocationButton = new Rect(new Vector2(CentrScreenX - 100, 50), new Vector2(200, 30));
                if (GUI.Button(LocationButton, "Вернуться в меню"))
                {
                    PlayerPoleControl.ClearPole();
                    GameMode = 0;
                }

                break;


            case 5:

                this.transform.position = new Vector3(70, 0, -10);

                LocationButton = new Rect(new Vector2(CentrScreenX - 150, 0), new Vector2(300, 200));
                GUI.Box(LocationButton, "");

                LocationButton = new Rect(new Vector2(CentrScreenX - 10, 10), new Vector2(200, 30));
                GUI.Label(LocationButton, "Меню");

                LocationButton = new Rect(new Vector2(CentrScreenX - 100, 50), new Vector2(200, 30));
                if (GUI.Button(LocationButton, "Вернуться в меню"))
                {
                    PlayerPoleControl.ClearPole();
                    GameMode = 0;
                }

                break;

        }

    }


    GamePole.TestCord Hooming()
    {
        GamePole.TestCord XY;

        XY.X = -1;
        XY.Y = -1;


        foreach (GamePole.Ship Test in Player.GetComponent<GamePole>().ListShip)
        {

            foreach (GamePole.TestCord Paluba in Test.ShipCoord)
            {
                int Index = Player.GetComponent<GamePole>().GetIndexBlock(Paluba.X, Paluba.Y);

                if (Index==1)
                {
                    return Paluba;
                }
            }


        }

        return XY;
    }

    int ShootCount = 0;

    [System.Obsolete]
    void AI() 
    {
        if (!whosMove)
        {

            int ShotX = Random.RandomRange(0, 9);

            int ShotY = Random.RandomRange(0, 9);

            int PC_Ship = ComputerPole.GetComponent<GamePole>().LifeShip();

            if (PC_Ship<10)
            {
                if (ShootCount==0)
                {
                    GamePole.TestCord XY = Hooming();

                    if ((XY.X >= 0) && (XY.Y >= 0))
                    {
                        ShotX = XY.X;
                        ShotY = XY.Y;
                    }
                    ShootCount++;
                }
                else
                {
                    ShootCount=0;
                }


               
            }

            



            whosMove = !Player.GetComponent<GamePole>().Shoot(ShotX, ShotY);

        }





    }


    void TestWhoWin()
    {
        int PC_Ship = ComputerPole.GetComponent<GamePole>().LifeShip();
        int Player_Ship = Player.GetComponent<GamePole>().LifeShip();

        if (PC_Ship == 0) GameMode = 4;


        if (Player_Ship == 0) GameMode = 5;



    }


    public void UserClick(int X, int Y)
    {

        if (whosMove)
        {
            whosMove = ComputerPole.GetComponent<GamePole>().Shoot(X, Y);
        }
    }






    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (GameMode==3)
        {
            TestWhoWin();

            AI();
        }
    }
}
