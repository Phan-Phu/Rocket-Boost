using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePoint : MonoBehaviour
{
    private Vector3 _posSave = new Vector2();
    private bool _isDestroy = false;
    [HideInInspector] public Vector3 PosSave { get { return _posSave; } set { _posSave = value; } }
    [HideInInspector] public bool IsDestroy { get { return _isDestroy; } set { _isDestroy = value; } }

    private void Awake()
    {
        int scenes = FindObjectsOfType<SavePoint>().Length;
        if (scenes < 2)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (_isDestroy)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player))
        {
            _posSave = transform.position;
        }
    }
}
