using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    internal bool isGrounded = true;
    private string floortag;
    void OnCollisionStay(Collision col)
    {
        floortag = col.gameObject.tag.ToString();
        if (floortag == null)
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = true;
        }
    }
}
