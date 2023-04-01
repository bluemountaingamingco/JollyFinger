using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clean : MonoBehaviour
{
    #region variables

    [SerializeField] private Camera _camera;

    [SerializeField] private Texture2D _dirtMaskBase;

    [SerializeField] private Texture2D _brush;

    [SerializeField] private Material _material;

    public List<Vector2> gapPositions = new List<Vector2>();

    public Texture2D _templateDirtMask;

    public float dirtAmountTotal;

    public float dirtAmount;

    private Vector2Int lastPaintPixelPosition;

    private Color[] pixels;

    private Vector2 prevPosition;

    private Vector2 currentPosition;

    private Color pixelDirt;

    private Color pixelDirtMask;

    public int data;

    public int errorInt;

    private int heightScreenRes;

    private int widthScreenRes;

    private Color32[] dirtMaskArr;

    public Color32[] brushPixels;

    private int pixelYMaxSize;

    private int pixelXMaxSize;

    private int brushInBetweenDistance;

    private float targetCleanAmount;

    private int moveCount;

    private float removedAmount;

    private float spriteLeaveTime;

    private bool isTouchingSprite;

    #endregion

    #region Methods

    private void Start()
    {
        targetCleanAmount = GameManager.Instance.GetCleanTargetAmount();

        CreateTexture();

        brushInBetweenDistance =   19;
    }

    private void Update()
    {
        if (!isTouchingSprite)
        {
            bool isBGMusicPlaying = AudioManager.instance.IsBGMusicPlaying;

            if (isBGMusicPlaying)
            {
                float timer = GameManager.Instance.GetTimer();

                float timeDifference = timer - spriteLeaveTime;

                if (timeDifference > .25)
                {
                    AudioManager.instance.SetCleaningSound(AudioState.State.Pause);
                }
            }
        }

        if (Input.touchCount > 0)
        {           
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position != currentPosition || touch.position != prevPosition)
                {
                    moveCount += 1;

                    SetPixel(touch.position);

                    prevPosition = touch.position;
                }
            }

            else if (touch.phase == TouchPhase.Moved)
            {
                if (touch.position != currentPosition || touch.position != prevPosition)
                {
                    moveCount += 1;

                    currentPosition = touch.position;

                    Vector2 dif = currentPosition - prevPosition;

                    float distance = dif.magnitude;

                    if (distance > brushInBetweenDistance)
                    {
                        float count = distance / brushInBetweenDistance;

                        for (int i = 0; i <= count; i++)
                        {
                            Vector2 gapPoint = Vector2.MoveTowards(prevPosition, currentPosition, brushInBetweenDistance * i);

                            SetPixel(gapPoint);
                        }

                        gapPositions.Clear();
                    }

                    else
                    {
                        SetPixel(currentPosition);
                    }

                    prevPosition = touch.position;
                }
            }  
        }

        else if (isTouchingSprite)
        {
            SetSpriteTouch(false);
        }

    }

    private void SetPixel(Vector2 touchPos)
    {
        RaycastHit hit = GetRaycasthit(touchPos);

        if (hit.collider != null)
        {
            DrawPixel(hit.textureCoord.x, hit.textureCoord.y);

            if (!isTouchingSprite)
                SetSpriteTouch(true);
        }

        else
        {
            if (isTouchingSprite)
                SetSpriteTouch(false);
        }

    }

    private RaycastHit GetRaycasthit(Vector2 position)
    {
        RaycastHit hit;

        Physics.Raycast(Camera.main.ScreenPointToRay(position), out hit);

        return hit;
    }

    private void SetSpriteTouch(bool isPlayMusic)
    {
        if (isPlayMusic)
        {
            bool isBGMusicPlaying = AudioManager.instance.IsBGMusicPlaying;

            isTouchingSprite = !isTouchingSprite;

            if (!isBGMusicPlaying)
            {
                AudioManager.instance.SetCleaningSound(AudioState.State.Play);
            }
        }

        else
        {
            spriteLeaveTime = GameManager.Instance.GetTimer();

            isTouchingSprite = !isTouchingSprite;
        }
    }

    private void CreateTexture()
    {
        _templateDirtMask = new Texture2D(_dirtMaskBase.width, _dirtMaskBase.height);
        _templateDirtMask.SetPixels32(_dirtMaskBase.GetPixels32());
        _templateDirtMask.Apply();

        _material.SetTexture("_DirtMask", _templateDirtMask);

        dirtAmountTotal = 0f;

        for (int x = 0; x < _dirtMaskBase.width; x++)
        {
            for (int y = 0; y < _dirtMaskBase.height; y++)
            {
                dirtAmountTotal += _dirtMaskBase.GetPixel(x, y).g;
            }
        }

        dirtAmount = dirtAmountTotal;

        heightScreenRes = Display.main.systemHeight;

        widthScreenRes = Display.main.systemWidth;

        dirtMaskArr = _dirtMaskBase.GetPixels32();

        brushPixels = _brush.GetPixels32(0);

        pixelYMaxSize = _templateDirtMask.height - _brush.height;

        pixelXMaxSize = _templateDirtMask.width - _brush.width;
    }

    private (int, int, int) CheckPixelDeduction(int pixelValue, int templateMaxDimension, int brushDimension)
    {
        int newPixelOffset = 0;

        int additionalPixel = 0;

        int brushIndexPos = 0;

        int halfBrushDimension = brushDimension / 2;

        bool isOutsideTemplateBgn = (pixelValue - halfBrushDimension) < 0;

        bool isOutsideTemplateEnd = (pixelValue + halfBrushDimension) > templateMaxDimension;

        if (!isOutsideTemplateBgn && !isOutsideTemplateEnd)
        {
            newPixelOffset = pixelValue - halfBrushDimension;

            additionalPixel = brushDimension;
        }
        else
        {
            if (isOutsideTemplateBgn)
            {
                newPixelOffset = 0;

                additionalPixel = pixelValue + halfBrushDimension;

                brushIndexPos = brushDimension - additionalPixel;
            }
            else
            {
                newPixelOffset = pixelValue - halfBrushDimension;

                additionalPixel = halfBrushDimension + (templateMaxDimension - pixelValue);
            }
        }
        
        return (newPixelOffset, additionalPixel, brushIndexPos);
    }

    private void CheckIfPlayerWon(float dirtAmountRemaining)
    {
        if (dirtAmountRemaining <= targetCleanAmount )
        {
            GameManager.Instance.SetGameOver();
        }
    }

    #endregion
}
