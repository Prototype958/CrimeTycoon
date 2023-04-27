using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharSelectManager : MonoBehaviour
{
	public StatCard CardPrefab;
	public StatCard[] StatCards;

	private bool toggle = false;

	public void Awake()
	{
		StatCards = new StatCard[3];

		StatCard.RosterUpdated += DisablePanel;
	}

	public void DisablePanel(Criminal c)
	{
		ToggleCharSelectorDisplay();
	}

	public void OnEnable()
	{
		for (int i = 0; i < StatCards.Length; i++)
		{
			StatCards[i] = Instantiate(CardPrefab, this.transform);
		}
	}

	public void OnDisable()
	{
		foreach (StatCard card in StatCards)
		{
			if (card != null)
				Destroy(card.gameObject);
		}
	}

	public void ToggleCharSelectorDisplay()
	{
		if (IncomeSystem.Instance.CanAfford(GameManager.Instance.RecruitCost))
		{
			IncomeSystem.Instance.Spend(GameManager.Instance.RecruitCost);
			this.gameObject.SetActive(toggle = !toggle);
		}
	}
}
