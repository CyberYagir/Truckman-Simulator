using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScript : MonoBehaviour
{
    public static GlobalScript g;

    private void Start()
    {
        g = this;
        DontDestroyOnLoad(g);
    }
}
