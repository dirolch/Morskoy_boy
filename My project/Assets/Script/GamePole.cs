using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GamePole : MonoBehaviour
{

    //
    public GameObject GameMain;
    //


    public GameObject eLiters, eNums, ePole, eState;

    public GameObject MapDestination;

    public bool HideShip = false;


    GameObject[] Liters;

    GameObject[] Nums;
    public
    GameObject[,] Pole;




    int Time = 300, DateTime = 0;

    int lengPole = 10;



    public int[] ShipCount = { 0, 4, 3, 2, 1};


    //Функция возвращает правду если есть корабои в ангаре
    bool CountShips()
    {

        int Amaunt = 0;
         
        foreach (int Ship in ShipCount) Amaunt += Ship;

        if (Amaunt != 0) return true;

        return false;
    }

    // Функция очистки поля

    public
    void ClearPole()
    {

        ShipCount = new int[] { 0, 4, 3, 2, 1};
        ListShip.Clear();
        for (int Y = 0; Y < lengPole; Y++)
        {

            for (int X = 0; X < lengPole; X++)
            {

                Pole[X, Y].GetComponent<Chanks>().index = 0;

            }


        }


    }


public
    void RandomShip()
    {

        ClearPole();

        int SelectShip = 4;

        int X, Y;

        int Direction;

        while (CountShips())
        {
            X = Random.RandomRange(0, 10);
            Y = Random.RandomRange(0, 10);

            Direction = Random.RandomRange(0, 2);

            if (EnterDeck(SelectShip, Direction, X, Y))
            {
                ShipCount[SelectShip]--;

                if (ShipCount[SelectShip] == 0)
                {
                    SelectShip--;
                }
            }

        }



    }







    public
    struct TestCord
    {
        public int X, Y;

    }

    public
    struct Ship
    {
        public TestCord[] ShipCoord;

    }

    public
    List<Ship> ListShip = new List<Ship>();






    public void CopyPole()
    {

        if (MapDestination != null)
        {
            for (int Y = 0; Y < lengPole; Y++)
            {
                for (int X = 0; X < lengPole; X++)
                {
                    MapDestination.GetComponent<GamePole>().Pole[X, Y].GetComponent<Chanks>().index = Pole[X, Y].GetComponent<Chanks>().index;
                }
            }

            MapDestination.GetComponent<GamePole>().ListShip.Clear();

            MapDestination.GetComponent<GamePole>().ListShip.AddRange(ListShip);



        }




    }

    void CreatePole()
    {

        Vector3 StartPoze = transform.position;
        float XX = StartPoze.x + 1;
        float YY = StartPoze.y - 1;


        Liters = new GameObject[lengPole];
        Nums = new GameObject[lengPole];


        for (int Nadpis = 0; Nadpis < lengPole; Nadpis++)
        {
            Liters[Nadpis] = Instantiate(eLiters);
            Liters[Nadpis].transform.position = new Vector3(XX, StartPoze.y, StartPoze.z);
            Liters[Nadpis].GetComponent<Chanks>().index = Nadpis;
            XX++;


            Nums[Nadpis] = Instantiate(eNums);
            Nums[Nadpis].transform.position = new Vector3(StartPoze.x, YY, StartPoze.z);
            Nums[Nadpis].GetComponent<Chanks>().index = Nadpis;
            YY--;

        }

        XX = StartPoze.x + 1;
        YY = StartPoze.y - 1;

        Pole = new GameObject[lengPole, lengPole];
        for (int Y = 0; Y < lengPole; Y++)
        {
            for (int X = 0; X < lengPole; X++)
            {
                Pole[X,Y] = Instantiate(ePole);
                Pole[X, Y].GetComponent<Chanks>().index = 0;
                Pole[X, Y].GetComponent<Chanks>().HideChank = HideShip;




                Pole[X, Y].transform.position = new Vector3(XX, YY, StartPoze.z);
                if(HideShip)
                Pole[X, Y].GetComponent<ClickPole>().WhoPerent = this.gameObject;

                Pole[X, Y].GetComponent<ClickPole>().CoordX = X;
                Pole[X, Y].GetComponent<ClickPole>().CoordY = Y;

                XX++;
            }
            XX = StartPoze.x + 1;
            YY--;
        }

    }

    bool TestDeck(int X, int Y)
    {
        if ((X > -1 ) && (Y > -1) && (X < 10) && (Y < 10))
        {

            int[] XX = new int[9], YY = new int[9];

            XX[0] = X + 1;      XX[1] = X;          XX[2] = X - 1;
            YY[0] = Y + 1;      YY[1] = Y + 1;      YY[2] = Y + 1;


            XX[3] = X + 1;      XX[4] = X;          XX[5] = X - 1;
            YY[3] = Y;          YY[4] = Y;          YY[5] = Y;

            XX[6] = X + 1;      XX[7] = X;          XX[8] = X - 1;
            YY[6] = Y-1;        YY[7] = Y - 1;      YY[8] = Y - 1;


            for (int i = 0; i < 9; i++)
            {
                if ((XX[i] > -1) && (YY[i] > -1) && (XX[i] <10) && (YY[i] < 10))
                {
                  if (Pole[XX[i], YY[i]].GetComponent<Chanks>().index != 0) return false;
                }
 
            }
            return true;

        }
        return false;
    }


    TestCord[] TestShipDirect(int ShipType, int XD, int YD, int X, int Y)
    {
        TestCord[] ResultCoord = new TestCord[ShipType];

        for (int P = 0; P < ShipType; P++)
        {

            if (TestDeck(X, Y))
            {
                ResultCoord[P].X = X;
                ResultCoord[P].Y = Y;


            }
            else return null;


            X += XD;
            Y += YD;

        }


        return ResultCoord;
    }


    TestCord[] TestShip(int ShipType, int Direction,  int X, int Y)
    {
        TestCord[] ResultCoord = new TestCord[ShipType];

        if (TestDeck(X,Y))
        {

            switch (Direction)
            {
                case 0:

                    ResultCoord = TestShipDirect(ShipType, 1, 0, X, Y);

                    if (ResultCoord == null) ResultCoord = TestShipDirect(ShipType, -1, 0, X, Y);


                    break;

                case 1:

                    ResultCoord = TestShipDirect(ShipType, 0, 1, X, Y);

                    if (ResultCoord == null) ResultCoord = TestShipDirect(ShipType, 0, -1, X, Y);

                    break;

            }


            return ResultCoord;
        }



        return null;
    }





    bool EnterDeck(int ShipType, int Direction, int X, int Y)
    {
        TestCord[] P = TestShip(ShipType, Direction, X, Y);



        if (P != null)
        {


            foreach (TestCord T in P)
            {
                Pole[T.X, T.Y].GetComponent<Chanks>().index = 1;
            }


            Ship Deck;

            Deck.ShipCoord = P;

            ListShip.Add(Deck);


            return true;
        }
        return false;
    }


    // Start is called before the first frame update
    void Start()
    {
        CreatePole();

       if(HideShip) RandomShip();
    }

    // Update is called once per frame
    void Update()
    {
        DateTime++;
        if(DateTime > Time)
        {
            if (eState != null) eState.GetComponent<Chanks>().index = 0;
            DateTime = 0;
        }
    }


    public void WhoClick(int X, int Y)
    {

        //if (TestDeck(X,Y)) Pole[X, Y].GetComponent<Chanks>().index = 1;

        //EnterDeck(4, 0, X, Y);

        //Shoot(X, Y);

        if (GameMain != null) GameMain.GetComponent<MainGame>().UserClick(X, Y);


    }


    public int GetIndexBlock(int X, int Y)
    {
        return Pole[X, Y].GetComponent<Chanks>().index;
    }




    public
    bool Shoot(int X, int Y)
    {

        if(eState != null)  eState.GetComponent<Chanks>().index = 0;

        int PoleSelect = Pole[X, Y].GetComponent<Chanks>().index;
        bool Result = false;

        switch (PoleSelect)
        {

            //Промах
            case 0:
                Pole[X, Y].GetComponent<Chanks>().index = 2;
                Result = false;

                eState.GetComponent<Chanks>().index = 3;
                break;
                //Попадание
            case 1:
                Pole[X, Y].GetComponent<Chanks>().index = 3;

                Result = true;

                if (TestShoot(X,Y))
                {
                    if (eState != null) eState.GetComponent<Chanks>().index = 1;
                }

                else
                {
                    if (eState != null) eState.GetComponent<Chanks>().index = 2;
                }





                break;

        }

        return Result;
    }



    bool TestShoot(int X, int Y)
    {
        bool Result = false;

        foreach (Ship Test in ListShip)
        {

            foreach (TestCord Paluba in Test.ShipCoord)
            {

                if ((Paluba.X == X)&&(Paluba.Y == Y))
                {
                    int CountKill = 0;

                    foreach (TestCord KillPaluba in Test.ShipCoord)
                    {

                        int TestBlock = Pole[KillPaluba.X, KillPaluba.Y].GetComponent<Chanks>().index;
                        if (TestBlock == 3) CountKill++;

                    }

                    if (CountKill == Test.ShipCoord.Length)
                        Result = true;

                    else 
                        Result = false;

                    return Result;
                }
            }
        }


        return Result;
    }




    public int LifeShip()
    {
        int countLife = 0;

        foreach (Ship Test in ListShip)
        {
            foreach (TestCord Paluba in Test.ShipCoord)
            {
                int TestBlock = Pole[Paluba.X, Paluba.Y].GetComponent<Chanks>().index;

                if(TestBlock == 1) countLife++;
            }


        }

        return countLife;

    }
}
