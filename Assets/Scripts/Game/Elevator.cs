using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [Tooltip("Collider used to stop the player from enterng the Elevator and immediately exiting")]
    public GameObject blockCollider;
    public AnimationClip openClip;
    public AnimationClip closeClip;

    public bool saveFloorWhenUsed = true;

    public int floor;

    private void Start()
    {
        GetComponentInChildren<Animation>().clip = openClip;
        GetComponentInChildren<Animation>().Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !GameManager.Instance.justLoadedFloor)
            StartCoroutine(GoToNextFloor());
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.Instance.justLoadedFloor = false;
        }
    }

    private IEnumerator GoToNextFloor()
    {
        blockCollider.SetActive(true);

        yield return new WaitForSeconds(1f);

        GameManager.Instance.justLoadedFloor = true;
        GetComponentInChildren<Animation>().clip = closeClip;
        GetComponentInChildren<Animation>().Play();

        yield return new WaitForSeconds(0.75f);

        StartCoroutine(GameManager.Instance.LoadFloor(floor));
    }
}
