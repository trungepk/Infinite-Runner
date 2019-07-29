using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandbox : MonoBehaviour {
    [SerializeField] GameObject player;
    [SerializeField] Material playerSkin;

    public void ChangePlayerSkin()
    {
        player.GetComponent<Renderer>().material = playerSkin;
    }
   
}
