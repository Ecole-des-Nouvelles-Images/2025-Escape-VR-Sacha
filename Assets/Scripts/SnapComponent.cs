using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SnapInteractable : MonoBehaviour {
    [SerializeField] private Transform _target;
    [SerializeField] private float _positionTolerance = 0.25f;

    private bool _alreadySnapped = false;
    private Rigidbody _rb;
    private XRGrabInteractable _grabInteractable;

    public UnityEvent OnSnapped;
    public Action<SnapInteractable> SnappedCallback;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
        _grabInteractable = GetComponent<XRGrabInteractable>();

        _grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void OnDestroy() {
        _grabInteractable.selectExited.RemoveListener(OnReleased);
    }

    private void OnReleased(SelectExitEventArgs args) {
        if (_alreadySnapped || _target == null) return;

        float distance = Vector3.Distance(transform.position, _target.position);
        if (distance <= _positionTolerance) {
            Snap();
        }
    }

    public void Snap() {
        if (_alreadySnapped || _target == null) return;

        transform.position = _target.position;
        transform.rotation = _target.rotation;

        if (_rb != null) {
            _rb.constraints = RigidbodyConstraints.FreezeAll;
            _rb.useGravity = false;
        }

        _alreadySnapped = true;
        OnSnapped?.Invoke();
        SnappedCallback?.Invoke(this);
    }

    public void Unsnap() {
        _alreadySnapped = false;
        if (_rb != null) {
            _rb.constraints = RigidbodyConstraints.None;
            _rb.useGravity = true;
        }
    }

    public bool IsSnapped => _alreadySnapped;
}