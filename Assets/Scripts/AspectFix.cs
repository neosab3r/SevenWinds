using UnityEngine;

public class AspectFix : MonoBehaviour
{
    private Camera _camera;
    private float _defaultWidth;

    private const float _defaultAspect = 0.5625f;

    private void Start()
    {
        _camera = Camera.main;

        _defaultWidth = _camera.orthographicSize * _defaultAspect;
        _camera.orthographicSize = _defaultWidth / _camera.aspect;
    }
}