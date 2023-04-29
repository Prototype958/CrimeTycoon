using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyRecruitButton : MonoBehaviour
{
	[SerializeField] private float _recruitCost;

	[SerializeField] private CharSelectManager RecruitPanel;

	public void OnClickRecruit()
	{
		if (IncomeSystem.Instance.CanAfford(_recruitCost) && !RosterManager.Instance.IsRosterFull())
		{
			RecruitPanel.ToggleDisplay();
		}
	}
}
