using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody), typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable))]
public class SnapComponent : MonoBehaviour
{
    [SerializeField] private List<Transform> _snapTargets;
    [SerializeField] private float _positionTolerance;

    private bool _alreadySnapped = false;
    private Rigidbody _rb;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable _grabInteractable;

    public UnityEvent OnSnapped;
    public Action<SnapComponent> SnappedCallback;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        if (_snapTargets == null || _snapTargets.Count == 0)
        {
            var handler = FindFirstObjectByType<Salle1.WordHandler>();
            if (handler != null)
                _snapTargets = handler.GetEmplacements();
        }

        _grabInteractable.selectExited.AddListener(OnReleased);
        _grabInteractable.selectEntered.AddListener(OnGrabbed);
    }

    private void OnDestroy()
    {
        _grabInteractable.selectExited.RemoveListener(OnReleased);
        _grabInteractable.selectEntered.RemoveListener(OnGrabbed);
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

        _rb.constraints = RigidbodyConstraints.FreezeAll;
        _rb.useGravity = false;

        _alreadySnapped = true;
        OnSnapped?.Invoke();
        SnappedCallback?.Invoke(this);

        if (target.TryGetComponent(out Salle1.EmplacementComponent emplacement))
        {
            if (TryGetComponent(out Salle1.LetterComponent letter))
            {
                emplacement.SetLetter(letter, this);
            }
        }
    }

    private void Unsnap()
    {
        _alreadySnapped = false;
        _rb.constraints = RigidbodyConstraints.None;
        _rb.useGravity = true;

        // Clear any previous occupant if needed
        foreach (var target in _snapTargets)
        {
            if (target.TryGetComponent(out Salle1.EmplacementComponent emplacement))
            {
                emplacement.ClearIfOccupant(this);
            }
        }
    }

    public bool IsSnapped => _alreadySnapped;
}
