using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CompletionTickBar : MonoBehaviour
{
	[SerializeField] private Job _job;

	[SerializeField] private Slider _completionBar;

	public Job job => _job;
	public Slider Slider => _completionBar;

	private void Awake()
	{
		_completionBar = GetComponent<Slider>();
		_completionBar.value = 0;
	}
}