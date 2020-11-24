using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSocket : MonoBehaviour
{

    public Animator MyAnimator { get; set; }

    private SpriteRenderer spriteRenderer;

    private Animator parentAnimator;

    private AnimatorOverrideController animatorOverrideController;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        parentAnimator = GetComponentInParent<Animator>();
        MyAnimator = GetComponent<Animator>();

        animatorOverrideController = new AnimatorOverrideController(MyAnimator.runtimeAnimatorController);

        MyAnimator.runtimeAnimatorController = animatorOverrideController;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Equip(AnimationClip[] animations)
    {
        spriteRenderer.color = Color.white;
        animatorOverrideController["Attack_Fist_Down"] = animations[0];
        animatorOverrideController["Attack_Fist_Left"] = animations[1];
        animatorOverrideController["Attack_Fist_Right"] = animations[2];
        animatorOverrideController["Attack_Fist_Up"] = animations[3];

        animatorOverrideController["IdleDown"] = animations[4];
        animatorOverrideController["IdleLeft"] = animations[5];
        animatorOverrideController["IdleRight"] = animations[6];
        animatorOverrideController["IdleUp"] = animations[7];

        animatorOverrideController["Walk_Down_Naked"] = animations[8];
        animatorOverrideController["Walk_Left_Naked"] = animations[9];
        animatorOverrideController["Walk_Right_Naked"] = animations[10];
        animatorOverrideController["Walk_Up_Naked"] = animations[11];

    }
    public void Dequip()
    {
        animatorOverrideController["Attack_Fist_Down"] = null;
        animatorOverrideController["Attack_Fist_Left"] = null;
        animatorOverrideController["Attack_Fist_Right"] = null;
        animatorOverrideController["Attack_Fist_Up"] = null;

        animatorOverrideController["IdleDown"] = null;
        animatorOverrideController["IdleLeft"] = null;
        animatorOverrideController["IdleRight"] = null;
        animatorOverrideController["IdleUp"] = null;

        animatorOverrideController["Walk_Down_Naked"] = null;
        animatorOverrideController["Walk_Left_Naked"] = null;
        animatorOverrideController["Walk_Right_Naked"] = null;
        animatorOverrideController["Walk_Up_Naked"] = null;

        Color c = spriteRenderer.color;
        c.a = 0;
        spriteRenderer.color = c;
    }
}
