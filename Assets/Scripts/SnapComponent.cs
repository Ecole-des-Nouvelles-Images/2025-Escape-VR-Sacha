using System.Collections.Generic;
using SalleIntro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapComponent : MonoBehaviour
{
    [SerializeField] private List<Transform> _snapTargets;
    [SerializeField] private float _positionTolerance;

    private bool _alreadySnapped = false;
    private Rigidbody _rb;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable _grabInteractable;

    private IntroHandler _introHandler;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        _introHandler = FindFirstObjectByType<IntroHandler>();

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
        Transform bestTarget = null;
        float bestDistance = float.MaxValue;

        foreach (var target in _snapTargets)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= _positionTolerance)
            {
                if (target.TryGetComponent(out Salle1.EmplacementComponent emplacement) && emplacement.IsOccupied)
                    continue;

                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    bestTarget = target;
                }
            }
        }

        if (bestTarget != null)
        {
            Snap(bestTarget);
        }
        else
        {
            _rb.useGravity = true;
        }
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
            if (TryGetComponent(out Salle1.LetterComponent letter))
            {
                emplacement.SetLetter(letter, this);
            }
        }

        _introHandler?.CheckPedestalGroups(); // 👈 Ajout important
    }

    private void Unsnap()
    {
        _alreadySnapped = false;
        _rb.constraints = RigidbodyConstraints.None;
        _rb.useGravity = true;

        foreach (var target in _snapTargets)
        {
            if (target.TryGetComponent(out Salle1.EmplacementComponent emplacement))
            {
                emplacement.ClearIfOccupant(this);
            }
        }

        _introHandler?.CheckPedestalGroups(); // 👈 Ajout important
    }

    public bool IsSnapped => _alreadySnapped;
}
