using TMPro;
using UnityEngine;

public class RosterCard : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _nameField, _powerField, _stealthField, _techField, _charmField;

	[SerializeField] public Criminal _criminal;

	public void Init(Criminal criminal)
	{
		_criminal = criminal;
		UpdateStatBlocks(criminal);
	}

	private void UpdateStatBlocks(Criminal criminal)
	{
		_nameField.text = criminal.Name;
		_powerField.text = criminal.Power.ToString();
		_stealthField.text = criminal.Stealth.ToString();
		_techField.text = criminal.Tech.ToString();
		_charmField.text = criminal.Charm.ToString();
	}

	public void OnClick()
	{
		CriminalManager.Instance.DisplayLargeStatCard(_criminal);
	}
}
