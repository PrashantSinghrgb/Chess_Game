using System.Collections;
using UnityEngine;

namespace PrashantSingh.Animations
{
    public class SelfDestroyingAnimationObject : MonoBehaviour
    {
        public void AnimationCompleted()
        {
            Destroy(gameObject);
        }
    }
}