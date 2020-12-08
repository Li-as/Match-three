using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelGoalsSystem : MonoBehaviour
{
    [SerializeField] private MovesGoal _movesGoal;
    [SerializeField] private ScoreGoal _scoreGoal;
    [SerializeField] private MultiElementsGoal _multiElementsGoal;

    private int _receivedStarsAmount;
    private bool _goalsReached;
    private bool _goalsFailed;

    public MovesGoal MovesGoal => _movesGoal;
    public ScoreGoal ScoreGoal => _scoreGoal;
    public MultiElementsGoal MultiElementsGoal => _multiElementsGoal;
    public int ReceivedStarsAmount => _receivedStarsAmount;
    public bool GoalsReached => _goalsReached;
    public bool GoalsFailed => _goalsFailed;

    private void Start()
    {
        _receivedStarsAmount = 0;
        _movesGoal.Init();
        _scoreGoal.Init();
    }

    public bool IsEndOfLevel()
    {
        bool isEndOfLevel = false;

        if (_scoreGoal.IsReached() && _movesGoal.IsReached())
        {
            _receivedStarsAmount = 1;

            if (_multiElementsGoal.IsHalfReached())
            {
                _receivedStarsAmount++;
            }
            if (_multiElementsGoal.IsReached())
            {
                _receivedStarsAmount++;
            }

            _goalsReached = true;
            isEndOfLevel = true;
        }
        else if (_movesGoal.IsNoMovesLeft() && _scoreGoal.IsReached() == false)
        {
            _goalsFailed = true;
            isEndOfLevel = true;
        }

        return isEndOfLevel;
    }

    public void ReduceMovesAmount()
    {
        _movesGoal.ChangeAmount();
    }

    public void AddScore(int value)
    {
        _scoreGoal.ChangeAmount(value);
    }

    public void TryAddElements(Sprite elementSprite, int value)
    {
        _multiElementsGoal.TryAddElements(elementSprite, value);
    }

    public void Reset()
    {
        _receivedStarsAmount = 0;
        _goalsFailed = false;
        _goalsReached = false;
        _movesGoal.Reset();
        _scoreGoal.Reset();
        _multiElementsGoal.Reset();
    }
}
