using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetGameOverStar : MonoBehaviour
{
    [SerializeField]
    private Sprite firstStarSprite;

    [SerializeField]
    private Sprite secondStarSprite;

    [SerializeField]
    private Sprite thirdStarSprite;

    private Image firstStarImg, secondStarImg, thirdStarImg;

    private void SetStarReference()
    {
        GameObject firstStarGO = gameObject.transform.GetChild(0).GetChild(0).gameObject;

        GameObject secondStarGO = gameObject.transform.GetChild(1).GetChild(0).gameObject;

        GameObject thirdStarGO = gameObject.transform.GetChild(2).GetChild(0).gameObject;

        firstStarImg = firstStarGO.GetComponent<Image>();

        secondStarImg = secondStarGO.GetComponent<Image>();

        thirdStarImg = thirdStarGO.GetComponent<Image>();
    }

    public void SetStarWon(int StarWon)
    {
        SetStarReference();

        if (StarWon == 3)
        {
            firstStarImg.sprite = thirdStarSprite;

            secondStarImg.sprite = thirdStarSprite;

            thirdStarImg.sprite = thirdStarSprite;
        }
        else if (StarWon == 2)
        {
            firstStarImg.sprite = secondStarSprite;

            secondStarImg.sprite = secondStarSprite;
        }
        else
        {
            firstStarImg.sprite = firstStarSprite;
        }
    }

    
}
