using System.Collections;
using System.Collections.Generic;
using OpenCover.Framework.Model;
using UnityEngine;

/*
  Base names(1part):
    Marco
    Bobby
    Tommy
    Steve
    Jeff
    Frank
    Hank
    Dandy
    Pauly
    Vinnie
    Walter
    Jessie
    Tony
    
    
  Specialty names (2part or 1part):
    Ten-toes
    Shanks
    Tightmouth
    Bigs
    Smalls
    Tiny
    Two-Eyes
    Bigmouth
    Crimeman
    Ugly
    Big-Fat
    

  Person The Thing (3part):
    the Rat
    the Clown
    the Kid
    the Pig
    the Dog
    the Fish
    the Cleaner
    the Teacher
    the Catapulter
    the Chinless
    the Slippery
    the Oddly Specific
    the Lovely
    the Unrefined
    the Stinky
    the Conductor
    the Postmaster
    the Horse
    the Clown
    the Criminal
    the Smelly
    
 
 
 */


public class NameBuilder : MonoBehaviour
{
    private List<string> _singleNames;
    private List<string> _doubleNames;
    private List<string> _descriptiveNames;

    public bool Initialized = false;

    public static NameBuilder Instance;
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void ParseFile()
    {
        string _fileName = "../assets/SingleNames";
        string text = System.IO.File.ReadAllText(_fileName);


        string[] strValues = text.Split(new string[] { "\n", "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);

        Debug.Log(strValues[3]);
    }

    public string BuildNewName()
    {
        string name = "";

        int x = Random.Range(1, 3);

        //Maybe get 1 name

        //Maybe get 2 names

        //Maybe get "the..." name

        if(x == 1)
        {
            name = SingleName();
        }

        return name;
    }

    private string SingleName()
    {
        string sName = "";



        return sName;
    }

    private void InitializaeAllNameOptions()
    {
        _singleNames = new List<string>();
        _doubleNames = new List<string>();
        _descriptiveNames = new List<string>();

        Initialized = true;
    }
}
