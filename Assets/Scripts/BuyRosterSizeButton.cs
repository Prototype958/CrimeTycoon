using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyRosterSizeButton : MonoBehaviour
{
	[SerializeField] private float _rosterSizeCost;

	public void OnClickRosterSize()
	{
		if (IncomeSystem.Instance.CanAfford(_rosterSizeCost))
		{
			RosterManager.Instance.UpdateMaxRosterSize(3);
		}
	}
}
