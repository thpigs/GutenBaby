/* Copyright (c) 2020 Kuneko. All rights reserved. */

using UnityEngine;

public class PageTurn : MonoBehaviour {

	//----------------------------------------
	// Public Variables
	//----------------------------------------

	public int direction;

	//----------------------------------------
	// On Mouse Down
	//----------------------------------------

	void OnMouseDown () {

		// Turn the book pages when this game object is clicked
        // If a direction has been set in the Unity editor then that direction is sent to the book
        // If no direction is set then the name of the tab is used as the target page number i.e. Tab01 goes to page 1

        if (direction == 0)
            SendMessageUpwards("TurnToPage", int.Parse(name.Substring(3, 2)));
        else
    		SendMessageUpwards("TurnPage", direction);
		
	}

}