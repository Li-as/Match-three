using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class Element : MonoBehaviour
{
    [SerializeField] protected float MoveSpeed;

    private SpriteRenderer _renderer;
    private Animator _animator;

    public event UnityAction ElementDestroyed;
    public event UnityAction ElementStoppedMoving;
    public SpriteRenderer Renderer => _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
    }

    public void StartMovingTo(Vector3 targetPosition)
    {
        StartCoroutine(MoveTo(targetPosition));
    }

    private IEnumerator MoveTo(Vector3 targetPosition)
    {
        while (IsCloseTo(targetPosition) == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, MoveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition;
        ElementStoppedMoving?.Invoke();

        yield break;
    }

    private bool IsCloseTo(Vector3 targetPosition)
    {
        Vector3 distance = targetPosition - transform.position;
        if (distance.magnitude < 0.001f)
        {
            return true;
        }

        return false;
    }

    public void StartDestroying()
    {
        _animator.enabled = true;
        _animator.SetTrigger("Destroy");
        StartCoroutine(WaitForDestroyAnimationEnd("Explosion"));
    }

    private IEnumerator WaitForDestroyAnimationEnd(string stateName)
    {
        if (IsAnimationPlaying(stateName) == false)
        {
            yield return new WaitForSeconds(0.25f);
        }

        while (IsAnimationPlaying(stateName))
        {
            //Debug.Log("In WaitForDestroyEnd while cycle");
            yield return null;
        }
        ElementDestroyed?.Invoke();

        yield break;
    }

    private bool IsAnimationPlaying(string stateName)
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
            _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            return true;
        }

        return false;
    }
}
