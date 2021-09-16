using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possibilities 
{
    public static Possibility[] possibilityArr = new Possibility[] 
    {
        new Possibility(new int[]{ 0,4,1 },13),
        new Possibility(new int[]{ 4,4,3 },13),
        new Possibility(new int[]{ 2,2,0 },13),
        new Possibility(new int[]{ 4,1,0 },13),
        new Possibility(new int[]{ 1,0,2 },13),
        new Possibility(new int[]{ 0,0,0 },9),
        new Possibility(new int[]{ 1,1,1 },8),
        new Possibility(new int[]{ 3,3,3 },7),
        new Possibility(new int[]{ 4,4,4 },6),
        new Possibility(new int[]{ 2,2,2 },5)
    };

}

public struct Possibility
{
    public int[] order;
    public int possibilityRate;
    public Possibility(int[] order, int possibilityRate)
    {
        this.order = order;
        this.possibilityRate = possibilityRate;
    }
}