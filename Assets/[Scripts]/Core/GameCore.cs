using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azterik_Core
{
    public class GameCore : MonoBehaviour
    {
    #if UNITY_EDITOR
        public static int score = 133742069;
    #elif !UNITY_EDITOR
    public static int score = 0;
    #endif


    }
}

