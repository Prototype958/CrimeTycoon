using UnityEngine;

public class Arrests
{
	[SerializeField] private float _arrestThreshold;

	public float ArrestThreshold { get { return _arrestThreshold; } set { _arrestThreshold = value; } }

	private SuspicionManager _suspicion;

	public Arrests()
	{
		_arrestThreshold = 50f;
		_suspicion = GameObject.FindObjectOfType<SuspicionManager>();
	}

	// Check if parameters for arrest have been met
	// If parameters met, attempt an arrest
	// If arrest successful, return true
	public bool CheckForArrest(Criminal criminal)
	{
		if (_suspicion.Suspicion > _arrestThreshold)
		{
			Debug.Log("go directly to jail");
			CriminalManager.Instance.OnArrest(criminal);

			return true;
		}
		return false;
	}
}
