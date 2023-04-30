using UnityEngine;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
	public static event System.Action<Upgrade> UpgradePurchased;

	// Upgrade Scriptable Object reference
	public Upgrade Upgrade;

	// Button field references
	public TextMeshProUGUI NameDisplay;
	public TextMeshProUGUI CostDisplay;
	public TextMeshProUGUI DescDisplay;

	public void AssignUpgrade(Upgrade u)
	{
		Upgrade = u;

		NameDisplay.text = Upgrade.Name;
		CostDisplay.text = Upgrade.Cost.ToString("C2");
		DescDisplay.text = Upgrade.Description;
	}

	public void PurchaseUpgrade()
	{
		if (IncomeSystem.Instance.CanAfford(Upgrade.Cost))
		{
			UpgradePurchased?.Invoke(Upgrade);
			IncomeSystem.Instance.Spend(Upgrade.Cost);
			Destroy(this.gameObject);
		}
		else
			Debug.Log("aint no money");
	}
}
