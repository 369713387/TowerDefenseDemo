using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTool {

	public static void Swap<T>(ref T a,ref T b)
    {
        T temp;
        temp = a;
        a = b;
        b = temp;
    }
}
