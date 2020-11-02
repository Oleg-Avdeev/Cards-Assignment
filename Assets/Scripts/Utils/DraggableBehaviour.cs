using UnityEngine;

public abstract class DraggableBehaviour : MonoBehaviour
{
    [SerializeField] private Collider2D _collider = default;
    [SerializeField] private float _iddleSize = 1f;
    [SerializeField] private float _activeSize = 1.5f;

    private bool _dragging = false;
    private Camera _gameCamera;

    private void Awake()
    {
        _gameCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        _dragging = true;
        _collider.enabled = false;
        transform.localScale = Vector3.one * _activeSize;

        HandlePickedUp();
    }

    private void OnMouseUp()
    {
        _dragging = false;
        _collider.enabled = true;
        transform.localScale = Vector3.one * _iddleSize;

        HandleDropped();
    }

    protected abstract void HandlePickedUp();

    protected abstract void HandleDropped();

    private void Update()
    {
        if (_dragging)
        {
            var point = Input.mousePosition;
            point = _gameCamera.ScreenToWorldPoint(point);
            point.z = -20;

            transform.position = point;
        }
    }

}