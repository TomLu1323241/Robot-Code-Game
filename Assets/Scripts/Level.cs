using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level : MonoBehaviour
{
    bool mouseDown = false;
    void OnMouseUp()
    {
        if (mouseDown)
        {
            if (this.GetComponentInChildren<TextMeshPro>().text.Equals("X"))
            {
                return;
            }
            SceneLoader.LoadScene(int.Parse(this.GetComponentInChildren<TextMeshPro>().text));
        }
    }

    void OnMouseExit()
    {
        mouseDown = false;
    }

    void OnMouseDown()
    {
        mouseDown = true;
    }
}
