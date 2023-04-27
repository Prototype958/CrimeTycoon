using System;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UI;

public class StatCard : MonoBehaviour
{
	public static event Action<Criminal> RosterUpdated;

	public Image BackGroundImage;
	public Sprite sprite;

	// Assign Reference to all Stat Values
	[SerializeField]
	private TextMeshProUGUI _powerValueField, _stealthValueField, _techValueField, _charmValueField, _nameField;

	private Criminal _criminal;

	private void Awake()
	{
		BackGroundImage = GetComponent<Image>();

		BackGroundImage.sprite = sprite;
		_criminal = CriminalManager.Instance.BuildNewCriminal();

		// Display character stat information on StatCard
		UpdateStatBlocks();
	}

	public void RecruitCriminal()
	{
		if (RosterManager.Instance.UpdateCurrentRosterSize(1))
		{
			Debug.Log("Selected " + _criminal.Name);
			RosterUpdated?.Invoke(_criminal);
			Destroy(this.gameObject);
		}
		else
		{
			Debug.Log("Roster is full");
		}
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
