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

    private void OnCollisionEnter(Collision collision)
    {
        if(GetComponent<AudioSource>() != null && !GetComponent<AudioSource>().isPlaying && GetComponent<Rigidbody>().velocity.magnitude > 2)
        {
            GetComponent<AudioSource>().Play();
        } else if (GetComponent<AudioSource>() != null && !GetComponent<AudioSource>().isPlaying && collision.rigidbody != null && collision.rigidbody.velocity.magnitude > 2)
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
