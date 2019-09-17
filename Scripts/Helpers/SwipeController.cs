using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController
{
    /// <summary>
    /// Vector2 for the first press of the swipe.
    /// </summary>
    Vector2 firstPressPos;

    /// <summary>
    /// Vector2 for the release of the swipe.
    /// </summary>
    Vector2 secondPressPos;

    /// <summary>
    /// The current swipe.
    /// </summary>
    Vector2 currentSwipe;

    /// <summary>
    /// Swipe controller enum for left, right, up, down, and no direction.
    /// </summary>
    public enum Directions
    {
        LEFT_SWIPE,
        RIGHT_SWIPE,
        UP_SWIPE,
        DOWN_SWIPE,
        NO_DIRECITON
    }

    /// <summary>
    /// Gets mouse swipe direction and returns an enum.
    /// </summary>
    public Directions Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
            //normalize the 2d vector
            currentSwipe.Normalize();

            //swipe up
            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                return Directions.UP_SWIPE;
            }
            //swipe down
            if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                return Directions.DOWN_SWIPE;
            }
            //swipe left
            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                return Directions.LEFT_SWIPE;
            }
            //swipe right
            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                return Directions.RIGHT_SWIPE;
            }
        }
        return Directions.NO_DIRECITON;
    }

    /// <summary>
    /// Gets touch swipe direction and returns an enum.
    /// </summary>
    public Directions TouchSwipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);
                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                //normalize the 2d vector
                currentSwipe.Normalize();

                //swipe up
                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    return Directions.UP_SWIPE;
                }
                //swipe down
                if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    return Directions.DOWN_SWIPE;
                }
                //swipe left
                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    return Directions.LEFT_SWIPE;
                }
                //swipe right
                if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    return Directions.RIGHT_SWIPE;
                }
            }
        }
        return Directions.NO_DIRECITON;
    }
}
