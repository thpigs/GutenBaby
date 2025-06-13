/* Copyright (c) 2020 Kuneko. All rights reserved. */

using UnityEngine;

public class Music : MonoBehaviour {

    //----------------------------------------
    // Private Static Variables
    //----------------------------------------

    private static Music instance;

    //----------------------------------------
    // Awake
    //----------------------------------------

    void Awake () {

        // Position this game object at the MainCamera position

        transform.position = Camera.main.transform.position;

        // Check to see if an instance of this object exists
        // If it doesm remove this game object
        // Else set this game object to the instance
        // This is used for the Music game object so the music will continue playing between scenes

        if (instance != null && instance != this)
            Destroy(gameObject);
        else { 
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }

}