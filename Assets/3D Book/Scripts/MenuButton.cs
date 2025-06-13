/* Copyright (c) 2020 Kuneko. All rights reserved. */

using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour {

    //----------------------------------------
    // On Mouse Down
    //----------------------------------------

    void OnMouseDown () {

        // When this object is clicked
        // Load the scene with the same name as this game object

        SceneManager.LoadScene(name);

    }

}