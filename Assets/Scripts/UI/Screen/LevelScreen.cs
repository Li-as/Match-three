using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class LevelScreen : Screen
{
    [SerializeField] protected Image LevelNumber;
    [SerializeField] protected Sprite LevelNumberSprite;
    [SerializeField] protected Image[] StarsImages;
    [SerializeField] protected Sprite EmptyStarSprite;
    [SerializeField] protected Sprite FilledStarSprite;
    [SerializeField] protected GoalsList GoalsList;
    [SerializeField] protected SoundsPerformer SoundsPerformer;


    protected virtual void Init(LevelGoalsSystem goalsSystem)
    {
        if (goalsSystem != null)
        {
            LevelNumber.sprite = LevelNumberSprite;
            for (int i = 0; i < StarsImages.Length; i++)
            {
                if (goalsSystem.ReceivedStarsAmount > i)
                {
                    StarsImages[i].sprite = FilledStarSprite;
                }
            }
            GoalsList.Show(goalsSystem);
        }
    }

    public virtual void Reset()
    {
        foreach (var starImage in StarsImages)
        {
            starImage.sprite = EmptyStarSprite;
        }
        GoalsList.Reset();
    }
}
