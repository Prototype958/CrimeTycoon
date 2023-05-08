using UnityEngine;
using TMPro;

public class BuyRosterSizeButton : MonoBehaviour
{
	[SerializeField] private float _rosterSizeCost;

	[SerializeField] private TextMeshProUGUI _costText;

	private void Awake()
	{
		_costText.text = _rosterSizeCost.ToString("c2");
	}

	public void OnClickRosterSize()
	{
		if (IncomeSystem.Instance.CanAfford(_rosterSizeCost))
		{
			RosterManager.Instance.UpdateMaxRosterSize(3);
			IncomeSystem.Instance.Spend(_rosterSizeCost);
			// Increase next roster size increase cost
			_costText.text = (_rosterSizeCost *= 2.5f).ToString("c2");
		}
	}
}
