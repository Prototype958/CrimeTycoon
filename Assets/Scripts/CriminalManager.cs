using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CriminalManager : MonoBehaviour
{
	public static CriminalManager Instance;

	[SerializeField]
	private StatDisplay DisplayCard;

	private List<string> SingleNameOptions;
	private List<string> TwoNameOptions;
	private List<string> ComplexNameOptions;

	public void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy(this);
		else
			Instance = this;

		BuildNameLists();
	}

	public void DisplayLargeStatCard(Criminal criminal)
	{
		DisplayCard.ToggleActive(criminal);
	}

	public Criminal BuildNewCriminal()
	{
		return new Criminal();
	}

	private List<string> ParseFile(string fileName)
	{
		List<string> TempList = new List<string>();
		string text;

		text = System.IO.File.ReadAllText(fileName);

		string[] strValues = text.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

		foreach (string str in strValues)
		{
			TempList.Add(str);
		}

		return TempList;
	}

	private void BuildNameLists()
	{
		if (SingleNameOptions == null)
		{
			Debug.Log("building");
			SingleNameOptions = new List<string>();
			SingleNameOptions = ParseFile("Assets/SingleNames.txt");
		}

		if (TwoNameOptions == null)
		{
			TwoNameOptions = new List<string>();
			TwoNameOptions = ParseFile("Assets/TwoNames.txt");
		}

		if (ComplexNameOptions == null)
		{
			ComplexNameOptions = new List<string>();
			ComplexNameOptions = ParseFile("Assets/ComplexNames.txt");
		}
	}

	public string GenerateName()
	{
		string name = "";

		// Randomize name combo
		// 1 - Single name
		//     pull from SingleNameOptions OR TwoNameOptions
		// 2 - Double Name
		//     Combine a SingleNameOptions with TwoNameoptions
		// 3 - Complex Name
		//     pull from SingleNameOptions OR TwoNameOptions AND ComplexNameOptions
		int combo = Random.Range(1, 4);

		if (combo == 1)
		{
			if (Random.Range(1, 2) == 1)
			{
				name += SingleNameOptions[Random.Range(0, SingleNameOptions.Count)];
			}
			else
			{
				name += TwoNameOptions[Random.Range(0, TwoNameOptions.Count)];
			}
		}
		else if (combo == 2)
		{
			name += SingleNameOptions[Random.Range(0, SingleNameOptions.Count)];
			name += " ";
			name += TwoNameOptions[Random.Range(0, TwoNameOptions.Count)];
		}
		else if (combo == 3)
		{
			if (Random.Range(1, 2) == 1)
			{
				name += SingleNameOptions[Random.Range(0, SingleNameOptions.Count)];
			}
			else
			{
				name += TwoNameOptions[Random.Range(0, TwoNameOptions.Count)];
			}

			name += " ";
			name += ComplexNameOptions[Random.Range(0, ComplexNameOptions.Count)];
		}


		return name;
	}
}

// When criminal picker panel is pulled up
//  Generate 3 criminals, add to temp list List<Criminal> _pickList
//  send pickList to UI, UI manager builds stat cards and displays
//  when criminal is picked, return selection to criminalManager
//  add selected to roster, List<Criminal> _currentRoster