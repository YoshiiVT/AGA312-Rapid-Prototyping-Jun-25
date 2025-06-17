using System;
using System.Collections;
using UnityEngine;

public static class CorutineX
{
    static public IEnumerator WaitFrame()
    {
        yield return new WaitForEndOfFrame();
    }
}


