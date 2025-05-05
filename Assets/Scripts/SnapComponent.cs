using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SnapComponent : MonoBehaviour
{
    [SerializeField] private List<Transform> _snapTargets;
    [SerializeField] private float _positionTolerance = 0.25f;

    private bool _alreadySnapped = false;
    private Rigidbody _rb;
    private XRGrabInteractable _grabInteractable;

    public UnityEvent OnSnapped;
    public Action<SnapComponent> SnappedCallback;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _grabInteractable = GetComponent<XRGrabInteractable>();

        if (_snapTargets == null || _snapTargets.Count == 0)
        {
            var handler = FindObjectOfType<Salle1.WordHandler>();
            if (handler != null)
            {
                _snapTargets = handler.GetEmplacements();
            }
        }

        _grabInteractable.selectExited.AddListener(OnReleased);
        _grabInteractable.selectEntered.AddListener(OnGrabbed);
    }

    private void OnDestroy()
    {
        if (_grabInteractable != null)
        {
            _grabInteractable.selectExited.RemoveListener(OnReleased);
            _grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        Unsnap();
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        if (_snapTargets == null || _snapTargets.Count == 0) return;

        foreach (var target in _snapTargets)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= _positionTolerance)
            {
                Snap(target);
                return;
            }
        }
        _rb.useGravity = true;
    }

    private void Snap(Transform target)
    {
        transform.position = target.position;
        transform.rotation = target.rotation;

        if (_rb != null)
        {
            _rb.constraints = RigidbodyConstraints.FreezeAll;
            _rb.useGravity = false;
        }

        _alreadySnapped = true;
        OnSnapped?.Invoke();
        SnappedCallback?.Invoke(this);
    }

    private void Unsnap()
    {
        _alreadySnapped = false;
        if (_rb != null)
        {
            _rb.constraints = RigidbodyConstraints.None;
            _rb.useGravity = true;
        }
    }

    public bool IsSnapped => _alreadySnapped;
}
