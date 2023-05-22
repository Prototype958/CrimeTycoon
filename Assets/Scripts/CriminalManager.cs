using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CriminalManager : MonoBehaviour
{
	public static CriminalManager Instance;

	[SerializeField] private StatDisplay DisplayCard;
	[SerializeField] private int _recruitRank;

	public int RecruitRank => _recruitRank;

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

	public void UpgradeRecruitRank()
	{
		_recruitRank++;
	}

	// If criminal is arrested close Large Stat Display window if open
	// Move criminal from mainRoster to arrestedRoster
	// Remove criminal from any active job rosters
	// find correct roster display card and remove it from scroll container
	public void OnArrest(Criminal criminal)
	{
		if (DisplayCard.CurrentCriminal == criminal)
			DisplayCard.Deactivate();

		RosterManager.Instance.MoveToArrested(criminal);

		var _card = GameObject.FindObjectsOfType<RosterCard>(true).First(s => s._criminal == criminal);

		Destroy(_card.gameObject);

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
			SingleNameOptions = new List<string>();
			SingleNameOptions = ParseFile("Assets/Resources/SingleNames.txt");
		}

		if (TwoNameOptions == null)
		{
			TwoNameOptions = new List<string>();
			TwoNameOptions = ParseFile("Assets/Resources/TwoNames.txt");
		}

		if (ComplexNameOptions == null)
		{
			ComplexNameOptions = new List<string>();
			ComplexNameOptions = ParseFile("Assets/Resources/ComplexNames.txt");
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