using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitAllUpgrades : MonoBehaviour
{
	public UpgradeButton UpgradeButtonPrefab;

	private Upgrade[] _allUpgrades;

	private void Awake()
	{
		_allUpgrades = Resources.LoadAll<Upgrade>("Upgrades");
		SortUpgradesByCost(_allUpgrades);

		foreach (Upgrade u in _allUpgrades)
		{
			UpgradeButton button = Instantiate(UpgradeButtonPrefab, this.transform);
			button.AssignUpgrade(u);
		}
	}

	private void SortUpgradesByCost(Upgrade[] upgrades)
	{
		for (int step = 0; step < upgrades.Length - 1; step++)
		{
			bool swapped = false;

			for (int i = 0; i < upgrades.Length - step - 1; i++)
			{
				if (upgrades[i].Cost > upgrades[i + 1].Cost)
				{
					Upgrade temp = upgrades[i];
					upgrades[i] = upgrades[i + 1];
					upgrades[i + 1] = temp;

					swapped = true;
				}
			}

			if (!swapped)
				break;
		}
	}
}
