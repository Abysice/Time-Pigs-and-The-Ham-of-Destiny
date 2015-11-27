//All enums centralized here
//
//

using UnityEngine;
using System.Collections;

public class Enums : MonoBehaviour {

    public enum GameStateNames
    {
        GS_00_NULL = -1,
        GS_01_MENU = 0,
        GS_02_LOADING = 1, 
        GS_03_INPLAY,
        GS_04_LEAVING
    };

}
