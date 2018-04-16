using UnityEngine;
using System.Collections;

public class KillAnimatedSprite : MonoBehaviour {
    public AnimationClip targetAnim;

    private IEnumerator KillOnAnimationEnd() {
        yield return new WaitForSeconds(targetAnim.length - 0.05f);
        Destroy(gameObject);
    }

    private void Start() {
        StartCoroutine(KillOnAnimationEnd());
    }
}