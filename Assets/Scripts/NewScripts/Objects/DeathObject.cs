﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathObject : ObstacleObject {

    public bool instantDeath = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        KillPlayer(instantDeath);
    }
}