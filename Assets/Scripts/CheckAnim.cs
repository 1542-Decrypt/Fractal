using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAnim : MonoBehaviour
{
    private void Start()
    {
        LookFor(); //The "Coconut texture" of this game lmfao
    }
    public void LookFor()
    {
        CharacterControl.walk = true;
    }
}
