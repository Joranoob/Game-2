using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    private Vector2 normalHeight;


    private void Start()
    {
        normalHeight = transform.localScale;
    }
}
