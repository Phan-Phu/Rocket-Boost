using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnegyItem : MonoBehaviour
{
    [SerializeField][Range(0f, 1f)] private float value = 0.5f;
    private Movement playerMovement;

    private void Awake()
    {
        playerMovement = FindObjectOfType<Movement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.Player))
        {
            IncreaseEnegy();
        }
    }

    private void IncreaseEnegy()
    {
        playerMovement.TimeUseEnergy -= value;
        Destroy(gameObject);
    }
}
