using UnityEngine;
using TMPro;

public class BuyRecruitButton : MonoBehaviour
{
	[SerializeField] private float _recruitCost;

	[SerializeField] private CharSelectManager RecruitPanel;
	[SerializeField] private TextMeshProUGUI _costText;

	private void Awake()
	{
		_costText.text = _recruitCost.ToString("c2");
	}

	public void OnClickRecruit()
	{
		if (IncomeSystem.Instance.CanAfford(_recruitCost) && !RosterManager.Instance.IsRosterFull())
		{
			RecruitPanel.ToggleDisplay();
			IncomeSystem.Instance.Spend(_recruitCost);
			// Increase next recruitment cost
			_costText.text = (_recruitCost *= 1.5f).ToString("c2");
		}
	}
}
