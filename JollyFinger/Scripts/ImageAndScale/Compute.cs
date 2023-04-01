using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Compute 
{

    public static Vector3 ImageToBackGroundScale(Vector2 spriteSize) // Fill Image to Background
    {
        Vector3 topRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        float worldSpaceWidth = topRightCorner.x * 2;

        float worldSpaceHeight = topRightCorner.y * 2;

        float scaleFactorX = worldSpaceWidth / spriteSize.x;

        float scaleFactorY = worldSpaceHeight / spriteSize.y;

        return new Vector3(scaleFactorX, scaleFactorY, 1);
    }

    public static Vector3 CustomImageScale(float spritePPU, float GOLength = .74f) // make physical length of gameobject same to all devices
    {
        float pixelLength = 300 * GOLength;

        float worldSpaceScale = (pixelLength / spritePPU);

        return new Vector3(worldSpaceScale, worldSpaceScale, 1);
    }
}
