using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetStarUI : MonoBehaviour
{
    [SerializeField]
    private int levelIndex;

    [SerializeField]
    private GameObject star1GO;

    [SerializeField]
    private GameObject star2GO;

    [SerializeField]
    private GameObject star3GO;

    [SerializeField]
    private Sprite star_on;

    [SerializeField]
    private Sprite star_off;

    private Image star1Img;

    private Image star2Img;

    private Image star3Img;

    private Color starColor;

    void SetStarReference()
    {
        star1Img = star1GO.GetComponent<Image>();

        star2Img = star2GO.GetComponent<Image>();

        star3Img = star3GO.GetComponent<Image>();

        starColor = new Color(1, 1, 1, 1f);
    }

    public void SetStarDetails(int starCount)
    {
        SetStarReference();

        star1Img.sprite = starCount >= 1 ? star_on : star_off;

        star2Img.sprite = starCount >= 2 ? star_on : star_off;

        star3Img.sprite = starCount >= 3 ? star_on : star_off;

        star1Img.color = starColor;

        star2Img.color = starColor;

        star3Img.color = starColor;
    }

    public int GetLevelIndex()
    {
        return levelIndex;
    }
}
