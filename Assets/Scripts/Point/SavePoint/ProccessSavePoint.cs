using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProccessSavePoint : MonoBehaviour
{
    [SerializeField] SavePoint savePoint;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPositionSavePoint();
    }

    private void SpawnPositionSavePoint()
    {
        savePoint = FindObjectOfType<SavePoint>();

        if(!savePoint) { return; }

        if (savePoint.PosSave != Vector3.zero)
        {
            transform.position = savePoint.PosSave + Vector3.up;
        }
    }
}
