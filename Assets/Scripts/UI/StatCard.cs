using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// Stat card for the Select New Criminal/Henchman
public class StatCard : MonoBehaviour
{
	public static event Action<Criminal> RosterUpdated;

	public Image BackGroundImage;
	public Sprite sprite;

	// Assign Reference to all Stat Values
	[SerializeField] private TextMeshProUGUI _powerValueField, _stealthValueField, _techValueField, _charmValueField, _nameField;

	private Criminal _criminal;

	private void Awake()
	{
		BackGroundImage.sprite = sprite;
		_criminal = CriminalManager.Instance.BuildNewCriminal();

		// Display character stat information on StatCard
		UpdateStatBlocks();
	}

	public void RecruitCriminal()
	{
		RosterUpdated?.Invoke(_criminal);
		Destroy(this.gameObject);
	}

	private void UpdateStatBlocks()
	{
		_nameField.text = _criminal.Name;

		_powerValueField.text = _criminal.Power.ToString();
		_stealthValueField.text = _criminal.Stealth.ToString();
		_techValueField.text = _criminal.Tech.ToString();
		_charmValueField.text = _criminal.Charm.ToString();
	}
}
