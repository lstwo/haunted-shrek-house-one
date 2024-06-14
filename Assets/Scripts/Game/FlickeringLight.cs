using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : BasicLight
{
    private void Start()
    {
        StartCoroutine(DoFlicker());
    }

    IEnumerator DoFlicker()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(.25f, 10f));
            for(int i = 0; i < Random.Range(1, 5); i++)
            {
                spotLight.SetActive(!spotLight.activeSelf);
                yield return new WaitForSeconds(Random.Range(.05f, 1f));
            }
            spotLight.SetActive(true);
        }
    }
}
