using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootSteps : MonoBehaviour
{
    public CharacterController mover;
    public MiscStuff speaker;
    public sound_node SoundMaster;
    public sound_node JumpMaster;
    public int JumpSoundId;

    internal int currentSurface;
    Vector2 walkInput;
    Vector2 lookInput;
    public void Update()
    {
        walkInput.x = Input.GetAxis("Horizontal");
        walkInput.y = Input.GetAxis("Vertical");
        walkInput = walkInput.normalized;
        if (mover.isGrounded)
        {
            if (walkInput != Vector2.zero)
            {
                SoundMaster.AudiatedObject.enabled = true;
            }
            else
            {
                SoundMaster.AudiatedObject.enabled = false;
            }
        }
    }
        public void PlayJump()
    {
        JumpMaster.PlayAudio(JumpSoundId);
    }
}
