/* Copyright (c) 2020 Kuneko. All rights reserved. */

using UnityEngine;

public class Book : MonoBehaviour {

	//----------------------------------------
	// Public Variables 
	//----------------------------------------

	public Transform[] pageLeaf;
	public AudioClip openCloseSound;
	public AudioClip pageTurnSound;

	//----------------------------------------
	// Private Variables
	//----------------------------------------

	private Transform[] pageAnim;
	private float[] pageAngle;
	private float[] pageAngleMin;
	private float[] pageAngleMax;
	private float speed = 150.0f;
	private int page = -1;
	private int totalPages;
	private AudioSource myAudio;

	//----------------------------------------
	// Start
	//----------------------------------------

	void Start () {
	
		// Cache the audio source

		myAudio = GetComponent<AudioSource>();

		// Setup the pages

		totalPages = pageLeaf.Length;
		pageAnim = new Transform[totalPages];
		pageAngle = new float[totalPages];
		pageAngleMin = new float[totalPages];
		pageAngleMax = new float[totalPages];

		// Loop through all of the pages
		// Set minimum and maximum angles and setup animations

		for (int i = 0 ; i < totalPages ; i++) {
			pageAngleMin[i] = pageLeaf[i].localEulerAngles.y;
			pageAngleMax[i] = pageLeaf[i].localEulerAngles.y + 170;
			pageAnim[i] = pageLeaf[i].Find("Page");
			if (pageAnim[i] != null) {
				pageAnim[i].GetComponent<Animation>()["RL"].speed = 2.0f;
				pageAnim[i].GetComponent<Animation>()["LR"].speed = 2.0f;
			}
		}

	}

	//----------------------------------------
	// Update
	//----------------------------------------

	void Update () {

		// Loop through all of the pages
		// Turn the page if necessary and play animations if they have been setup

		for (int i = 0 ; i < totalPages ; i++) {
			if (page >= i) {
				pageAngle[i] += Time.deltaTime * speed;
				if (pageAnim[i] != null)
					pageAnim[i].GetComponent<Animation>().Play("RL");
			} else {
				pageAngle[i] -= Time.deltaTime * speed;
				if (pageAnim[i] != null)
					pageAnim[i].GetComponent<Animation>().Play("LR");
			}
			pageAngle[i] = Mathf.Clamp(pageAngle[i], pageAngleMin[i], pageAngleMax[i]);
			pageLeaf[i].localEulerAngles = new Vector3(0.0f, pageAngle[i], 0.0f);
		}

	}

	//----------------------------------------
	// Turn Page
	//----------------------------------------

	public void TurnPage (int direction) {

		// Turn the pages if possible in the specified direction
		// Play sound effects for page turning and book opening/closing

		switch (direction) {
			case -1 :
				if (page < totalPages-1) {
					page++;
					if (page == 0 || page == totalPages-1)
						myAudio.PlayOneShot(openCloseSound);
					else
						myAudio.PlayOneShot(pageTurnSound);
				}
				break;
			case 1 :
				if (page > -1) {
					page--;
					if (page == -1 || page == totalPages-2)
						myAudio.PlayOneShot(openCloseSound);
					else
						myAudio.PlayOneShot(pageTurnSound);
				}
				break;
		}

	}

    //----------------------------------------
	// Turn To Page
	//----------------------------------------

    void TurnToPage (int num) {

        // Turn immediately to the specified page
        // This function is called when clicking on the page tabs

        page = num;
        myAudio.PlayOneShot(pageTurnSound);

    }

}