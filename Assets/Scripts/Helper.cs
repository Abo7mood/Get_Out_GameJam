using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper 
{
    public static int randomziation(int min,int max)
    {
       int rand= Random.Range(min, max);
        return rand;
    }
}
