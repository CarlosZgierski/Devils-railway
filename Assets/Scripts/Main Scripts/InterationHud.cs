using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterationHud : MonoBehaviour {

    private SpriteRenderer sRenderer;

    private void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
    }

    void Update ()
    {
        if (sRenderer.sprite != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                sRenderer.sprite = null;
            }
        }
	}
}
