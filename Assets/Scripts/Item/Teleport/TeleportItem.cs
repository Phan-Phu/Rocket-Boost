using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportItem : MonoBehaviour
{
    [SerializeField] GameObject endPoint;
    private Vector3 _posEndPoint;

    private void Start()
    {
        _posEndPoint = new Vector3();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.Player))
        {
            _posEndPoint = endPoint.transform.position + Vector3.right;
            ChangePosPlayer(_posEndPoint);
        }
    }

    private void ChangePosPlayer(Vector3 position)
    {
        Player player = FindObjectOfType<Player>();
        if (!player) { return; }
        player.transform.position = position;
    }
}
