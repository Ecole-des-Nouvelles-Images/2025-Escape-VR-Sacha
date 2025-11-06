using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SnapComponent : MonoBehaviour
{
    public static event Action<SnapComponent> OnAnySnapped;
    public static event Action<SnapComponent> OnAnyUnsnapped;

    [SerializeField] private List<Transform> _snapTargets;
    [SerializeField] private float _positionTolerance = 0.1f;

    private bool _alreadySnapped = false;
    private Rigidbody _rb;
    private XRGrabInteractable _grabInteractable;
    private Salle1.EmplacementComponent _currentEmplacement;

    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _grabInteractable = GetComponent<XRGrabInteractable>();

        _grabInteractable.selectExited.AddListener(OnReleased);
        _grabInteractable.selectEntered.AddListener(OnGrabbed);
    }

    private void OnDestroy()
    {
        _grabInteractable.selectExited.RemoveListener(OnReleased);
        _grabInteractable.selectEntered.RemoveListener(OnGrabbed);
    }

    private void OnGrabbed(SelectEnterEventArgs args) => Unsnap();

    private void OnReleased(SelectExitEventArgs args)
    {
        Transform bestTarget = null;
        float bestDistance = float.MaxValue;

        foreach (var target in _snapTargets)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= _positionTolerance)
            {
                if (target.TryGetComponent(out Salle1.EmplacementComponent emp) && emp.IsOccupied)
                    continue;

                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    bestTarget = target;
                }
            }
        }

        if (bestTarget != null)
            Snap(bestTarget);
        else
            _rb.useGravity = true;
    }

    private void Snap(Transform target)
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
        _rb.constraints = RigidbodyConstraints.FreezeAll;
        _rb.useGravity = false;

        _alreadySnapped = true;

        if (target.TryGetComponent(out Salle1.EmplacementComponent emplacement))
        {
            _currentEmplacement = emplacement;
            if (TryGetComponent(out Salle1.LetterComponent letter))
                emplacement.SetLetter(letter, this);
        }

        OnAnySnapped?.Invoke(this);
    }

    private void Unsnap()
    {
        if (!_alreadySnapped)
            return;

        _alreadySnapped = false;
        _rb.constraints = RigidbodyConstraints.None;
        _rb.useGravity = true;
        _currentEmplacement?.ClearIfOccupant(null);
        _currentEmplacement = null;

        OnAnyUnsnapped?.Invoke(this);
    }

    public bool IsSnapped => _alreadySnapped;
}
