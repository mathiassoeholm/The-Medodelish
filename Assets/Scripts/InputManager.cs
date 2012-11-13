using UnityEngine;
using System.Collections;

public class InputManager : UnityManager<InputManager>
{
    private Monster lastMonsterTapped;

	// Use this for initialization
	private void Start()
    {
	    
	}
	
	// Update is called once per frame
    private void Update()
    {
        RaycastHit hitInfo;

        // Cast a mouse input check ray
#if UNITY_IPHONE
        if (Input.touchCount > 0 && Input.GetTouch(Input.touchCount - 1).phase == TouchPhase.Began && Physics.Raycast(Camera.mainCamera.ScreenPointToRay(Input.mousePosition), out hitInfo, 10))

#else
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.mainCamera.ScreenPointToRay(Input.mousePosition), out hitInfo, 10))
#endif
        {
            // Check if we hit a monster
            if (hitInfo.collider.GetComponent<Monster>() != null)
            {
                if (this.lastMonsterTapped != null)
                {
                    // Stop monster sound
                    this.lastMonsterTapped.StopSound();

                    this.lastMonsterTapped.GoDown();

                    this.lastMonsterTapped = null;
                }
                
                this.lastMonsterTapped = hitInfo.collider.GetComponent<Monster>();

                // Invoke tapped on monster event
                EventManager.Instance.TappedOnMonster(this.lastMonsterTapped);

                // Play monster sound
                this.lastMonsterTapped.StartSound();

                GameManager.Instance.AddMonster(this.lastMonsterTapped);
                this.lastMonsterTapped.GoUp();
            }
        }

        // Check if we lift finger from screen and we hit a monster
#if UNITY_IPHONE
        if (Input.touchCount > 0 && Input.GetTouch(Input.touchCount - 1).phase == TouchPhase.Ended && this.lastMonsterTapped != null)
#else
        if (Input.GetMouseButtonUp(0) && this.lastMonsterTapped != null)
#endif
        {
            // Stop monster sound
            this.lastMonsterTapped.StopSound();
            
            this.lastMonsterTapped.GoDown();

            this.lastMonsterTapped = null;
        }
	}
}
