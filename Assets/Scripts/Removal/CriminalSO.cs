using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriminalSO : ScriptableObject
{
    public string Name;

    public int Rank;

    public int Power;
    public int Stealth;
    public int Tech;
    public int Charm;

    public string Specialty;

    public string[] names;

    public string GetName()
    {
        names = new string[5];
        names[0] = "Vinnie the Horse";
        names[1] = "Bobby Ten-Toes";
        names[2] = "Tiny the Kid";
        names[3] = "Frank the Postmaster";
        names[4] = "Tommy Tightmouth";

        if(Name != null)
        {
            return Name;
        }
        else
        {
            return names[Random.Range(0, names.Length)];
        }
    }
}
