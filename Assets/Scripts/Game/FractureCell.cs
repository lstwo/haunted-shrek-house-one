using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractureCell : MonoBehaviour
{
    public Animation _animation;

    private void OnEnable()
    {
        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
