using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerAnimScript : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;

    public AnimationClip downIdle;
    public AnimationClip upIdle;
    public AnimationClip sideIdle;
    public AnimationClip diagdownIdle;
    public AnimationClip diagupIdle;
    public AnimationClip downWalk;
    public AnimationClip upWalk;
    public AnimationClip sideWalk;
    public AnimationClip diagdownWalk;
    public AnimationClip diagupWalk;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.x == 0 && rb.velocity.y >= .01f)
        {
//            anim.clip = upWalk;
//            anim.Play();
        }

        else if (rb.velocity.x == 0f && rb.velocity.y >= -.01f)
        {
 //           anim.clip = downWalk;
 //           anim.Play();
        }

        else if (rb.velocity.x >= 0.01f && rb.velocity.y >= .01f)
        {
            //diag up right
  //          anim.clip = diagupWalk;
  //          anim.Play();

  //          if (anim. = diagupWalk)
  //          {
  //              transform.localScale = new Vector3(-1, 0, 0);
  //          }
        }

        else if (rb.velocity.x <= -0.01f && rb.velocity.y >= .01f)
        {
            //diag up left
  //          anim.clip = diagupWalk;
  //          anim.Play();
        }

        else if (rb.velocity.x <= -0.01f && rb.velocity.y <= -.01f)
        {
            //diag down left
   //         anim.clip = diagdownWalk;
   //         anim.Play();

   //         if (anim.clip = diagdownWalk)
   //         {
   //             transform.localScale = new Vector3(-1, 0, 0);
   //         }

        }

        else if (rb.velocity.x >= 0.01f && rb.velocity.y <= -.01f)
        {
            //diag down right
   //        anim.clip = diagdownWalk;
   //         anim.Play();
        }

        else if (rb.velocity.x >= 0.01f && rb.velocity.y == 0f)
        {
            //right
   //         anim.clip = sideWalk;
   //         anim.Play();
        }

        else if (rb.velocity.x <= -0.01f && rb.velocity.y == 0f)
        {
            //left
   //         anim.clip = sideWalk;
   //         anim.Play();

   //         if(anim.clip = sideWalk)
   //         {
   //             transform.localScale = new Vector3(-1, 0, 0);
   //         }
        }

    }
}
