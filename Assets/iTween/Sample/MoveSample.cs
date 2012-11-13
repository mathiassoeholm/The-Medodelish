using UnityEngine;
using System.Collections;

public class MoveSample : MonoBehaviour
{	
	void Start(){
        iTween.ScaleTo(gameObject, iTween.Hash("scale", Vector3.one, "easeType", "easeOutBounce", "delay", .1));
	}

    void Update()
    {
        
    }
}

