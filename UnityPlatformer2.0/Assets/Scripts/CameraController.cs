using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float zPozition = -10f;

    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, zPozition);
    }
}
