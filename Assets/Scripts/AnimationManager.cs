using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : Singleton<AnimationManager>
{
    private Animator anim;

    public void Play(string animationName)
	{
		anim.Play(animationName);
	}

}
