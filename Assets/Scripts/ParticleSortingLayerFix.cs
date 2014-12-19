using UnityEngine;
using System.Collections;

public class ParticleSortingLayerFix : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		particleSystem.renderer.sortingLayerName = "Player";
		particleSystem.renderer.sortingOrder = -1;
	}

}
