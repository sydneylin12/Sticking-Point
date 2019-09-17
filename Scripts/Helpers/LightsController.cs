using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class LightsController : MonoBehaviour
{
    /// <summary>
    /// The parent object that holds the 3 lights. 
    /// </summary>
    private GameObject lightSet;

    /// <summary>
    /// The sprite for the light and the small green dot. 
    /// </summary>
    private Sprite lightSprite, smallDotSprite;

    /// <summary>
    /// Array of 3 sprite renderers. 
    /// </summary>
    private SpriteRenderer[] lightArray;

    /// <summary>
    /// Called before the first frame.
    /// </summary>
    void Start()
    {
        lightSet = GameObject.Find("Light Set");
        lightSprite = Resources.Load<Sprite>("Sprites/Light");
        smallDotSprite = Resources.Load<Sprite>("Sprites/Green Dot");
        lightArray = new SpriteRenderer[3];
        for (int i = 0; i < 3; i++)
        {
            // Initialize the child objects
            lightArray[i] = lightSet.transform.GetChild(i).GetComponent<SpriteRenderer>();
            lightArray[i].sprite = smallDotSprite;
            lightArray[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Resets all 3 lights to small dots (invisible). 
    /// </summary>
    public void ResetLights()
    {
        for (int i = 0; i < 3; i++)
        {
            lightArray[i].sprite = smallDotSprite;
            lightArray[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Activate the lights based on the AttemptState. 
    /// </summary>
    public IEnumerator SetLights(AttemptState attemptState)
    {
        if (attemptState == AttemptState.Success) //3 whites, maybe 1 red
        {
            for (int i = 0; i < lightArray.Length; i++)
            {
                lightArray[i].gameObject.SetActive(true);
                yield return new WaitForSeconds(0.3f);
            }
            int rand = Random.Range(0, 10);
            for (int i = 0; i < lightArray.Length; i++)
            {
                lightArray[i].sprite = lightSprite;
                if (rand < 3)
                {
                    lightArray[rand].color = Color.red;
                }
            }
        }
        else if (attemptState == AttemptState.Fail) //3 reds
        {
            for (int i = 0; i < lightArray.Length; i++)
            {
                lightArray[i].gameObject.SetActive(true);
                yield return new WaitForSeconds(0.3f);
            }
            for (int i = 0; i < lightArray.Length; i++)
            {
                lightArray[i].sprite = lightSprite;
                lightArray[i].color = Color.red;
            }
        }
        else
        {
            throw new InvalidOperationException("This should never happen.");
        }
    }
}

