using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLayerSorter : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    GetComponent<Renderer>().sortingLayerName = "Foreground";
        

    }

    public void FlipParticle()
    {
        var transfom = GetComponent<ParticleSystem>().GetComponent<Transform>();
        transfom.Rotate(180f, 0f, 0f);
    }
}
