using System.Collections.Generic;
using UnityEngine;

public class SortUpgrades : MonoBehaviour
{
	[SerializeField] private List<UpgradeButton> _allUpgrades;

	private bool isInitialized = false;

	private void Awake()
	{
		_allUpgrades = new();

		//UpgradeButton.UpgradePurchased += ResortUpgrades;
	}

	private void Init()
	{
		isInitialized = true;
		var temp = FindObjectsOfType<UpgradeButton>();

		foreach (UpgradeButton ub in temp)
		{
			_allUpgrades.Add(ub);
		}
	}

	private void OnDestroy() => UpgradeButton.UpgradePurchased -= ResortUpgrades;

	public void ResortUpgrades(UpgradeClass u)
	{
		if (!isInitialized)
			Init();

		_allUpgrades.Sort();
		Debug.Log("sorting");
	}
}
