﻿using UnityEngine;
using System.Collections;

public class CameraScript3_fix : MonoBehaviour {
    public GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
	}

    void LateUpdate()
    {

    }
}
