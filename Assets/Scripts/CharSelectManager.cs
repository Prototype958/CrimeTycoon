using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharSelectManager : MonoBehaviour
{
    public StatCard CardPrefab;
    public StatCard[] StatCard;



    public void Awake()
    {
        StatCard = new StatCard[3];
    }

    public void OnEnable()
    {
        for(int i = 0; i < StatCard.Length; i++)
        {
            StatCard[i] = Instantiate(CardPrefab, this.transform);
        }
    }

    public void OnDisable()
    {
        foreach (StatCard card in StatCard)
        {
            if(card != null)
                Destroy(card.gameObject);
        }
    }

}
