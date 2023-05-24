using UnityEngine;

public class InitAllUpgrades : MonoBehaviour
{
	public UpgradeButton UpgradeButtonPrefab;

	private Upgrade[] _upgradeResources;

	private void Awake()
	{
		InitClass();
	}

	private void InitClass()
	{
		_upgradeResources = Resources.LoadAll<Upgrade>("Upgrades");
		SortUpgradesByCost(_upgradeResources);

		foreach (Upgrade u in _upgradeResources)
		{
			UpgradeClass upgrade = new UpgradeClass(u);
			UpgradeButton button = Instantiate(UpgradeButtonPrefab, this.transform);
			button.AssignUpgrade(upgrade);
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
