using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SnapComponent : MonoBehaviour 
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _positionTolerance = 0.25f;

    private bool _alreadySnapped = false;
    private Rigidbody _rb;
    private XRGrabInteractable _grabInteractable;

    public UnityEvent OnSnapped;
    public Action<SnapComponent> SnappedCallback;
    
    private RigidbodyConstraints _originalConstraints;
    private bool _originalUseGravity;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
        _grabInteractable = GetComponent<XRGrabInteractable>();

        _originalConstraints = _rb.constraints;
        _originalUseGravity = _rb.useGravity;

        _grabInteractable.selectExited.AddListener(OnReleased);
        _grabInteractable.selectEntered.AddListener(OnGrabbed);
    }

    private void OnGrabbed(SelectEnterEventArgs args) {
        if (_alreadySnapped) {
            Unsnap();
        }
    }

    private void OnDestroy() {
        _grabInteractable.selectExited.RemoveListener(OnReleased);
        _grabInteractable.selectEntered.RemoveListener(OnGrabbed);
    }

    private void OnReleased(SelectExitEventArgs args) {
        if (_alreadySnapped || _target == null) return;

        float distance = Vector3.Distance(transform.position, _target.position);
        if (distance <= _positionTolerance) {
            Snap();
        }
        else
        {
            _rb.useGravity = true;
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
            _rb.constraints = _originalConstraints;
            _rb.useGravity = true;
        }
    }

    public bool IsSnapped => _alreadySnapped;
}