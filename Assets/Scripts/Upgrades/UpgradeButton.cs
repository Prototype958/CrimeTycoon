using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
	public static event System.Action<UpgradeClass> UpgradePurchased;

	// Upgrade Scriptable Object reference
	public UpgradeClass Upgrade;

	// Button field references
	public TextMeshProUGUI NameDisplay;
	public TextMeshProUGUI CostDisplay;
	public TextMeshProUGUI DescDisplay;

	public void AssignUpgrade(UpgradeClass u)
	{
		Upgrade = u;

		NameDisplay.text = Upgrade.Name;
		CostDisplay.text = Upgrade.Cost.ToString("C2");
		DescDisplay.text = Upgrade.Description;
	}

	public void PurchaseUpgrade()
	{
		if (IncomeSystem.Instance.CanAfford(Upgrade.Cost) && ComparePrerequisites(Upgrade.GetPrerequisites()))
		{
			IncomeSystem.Instance.Spend(Upgrade.Cost);

			// If not a job stat upgrade, complete upgrade here
			// else send event to JobStatsClass to perform upgrades
			if (Upgrade.GetStatsToUpgrade().Contains(Stat.Rank))
			{
				CriminalManager.Instance.UpgradeRecruitRank();
			}
			else
			{
				UpgradePurchased?.Invoke(Upgrade);
			}


			if (Upgrade.CheckForNextRank())
				AssignUpgrade(Upgrade);
			else
				Destroy(this.gameObject);
		}
		else
			Debug.Log("aint no money");
	}

	private bool ComparePrerequisites(List<Upgrade> prereqs)
	{
		foreach (Upgrade u in prereqs)
		{
			if (!GameManager.Instance.CheckForPrerequisiteUpgrade(u))
				return false;
		}

		return true;
	}
}
