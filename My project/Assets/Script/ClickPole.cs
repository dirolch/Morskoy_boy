using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPole : MonoBehaviour
{

    public GameObject WhoPerent = null;

    public int CoordX, CoordY;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



         void OnMouseDown()
    {
        if (WhoPerent != null)
        {
            WhoPerent.GetComponent<GamePole>().WhoClick(CoordX, CoordY);
        }
    }
}
