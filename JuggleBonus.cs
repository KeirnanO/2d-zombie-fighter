using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JuggleBonus : MonoBehaviour {


    private int bonus;

    private Player player;
    private TextMeshProUGUI bonusText;

	// Use this for initialization
	void Awake ()
    {
        player = FindObjectOfType<Player>();
        bonusText = GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update ()
    {

        bonus = player.GetJuggleBonus();

        if(bonus < 2)
        {
            bonusText.SetText("");
        }
        else
        {
            bonusText.SetText("Juggle Bonus " + bonus);
        }


	}
}
