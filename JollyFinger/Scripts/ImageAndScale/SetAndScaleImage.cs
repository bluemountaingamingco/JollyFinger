using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAndScaleImage : MonoBehaviour
{
    private Vector3 imageScale;

    void Start()
    {
        if (gameObject.GetComponent<Clean>() != null)
            imageScale = Compute.CustomImageScale(GetComponent<SpriteRenderer>().sprite.pixelsPerUnit);

        else // if background
        {      
            SetBackGround();

            imageScale = Compute.ImageToBackGroundScale(gameObject.GetComponent<SpriteRenderer>().bounds.size);
        }
            
        gameObject.transform.localScale = imageScale;
    }

    void SetBackGround()
    {
        int levelIndex = GameManager.Instance.SelectedLevelIndex;

        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        if (levelIndex == 1)
            spriteRenderer.sprite = Resources.Load<Sprite>("Background/bg1");
        else
            spriteRenderer.sprite = Resources.Load<Sprite>("Background/bg2");
    }
}
