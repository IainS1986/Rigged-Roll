using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private float _zoomAmount = 0;
    private float _maxToClamp = 10;
    private float _rotSpeed = 10;

    void Update()
    {
        _zoomAmount += Input.GetAxis("Mouse ScrollWheel");
        _zoomAmount = Mathf.Clamp(_zoomAmount, -_maxToClamp, _maxToClamp);
        var translate = Mathf.Min(Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")), _maxToClamp - Mathf.Abs(_zoomAmount));
        transform.Translate(0, 0, translate * _rotSpeed * Mathf.Sign(Input.GetAxis("Mouse ScrollWheel")));
    }
}
