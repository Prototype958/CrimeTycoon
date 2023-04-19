using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayRoster : MonoBehaviour
{
    public RosterCard RosterCardPrefab;
    private RosterCard RosterCard;

    public void Awake()
    {
        
        
    }

    private void AddToDisplay(Criminal criminal)
    {
        RosterCard = Instantiate(RosterCardPrefab, this.transform);
        RosterCard._criminal = criminal;
    }
}
