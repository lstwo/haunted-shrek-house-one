using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractureCell : MonoBehaviour
{
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
