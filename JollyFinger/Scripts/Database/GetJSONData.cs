using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Text;

public class GetJSONData : MonoBehaviour
{
    private string Key = "5p+0cRcXKeZQ/mph0oRKpg==";

    private string IV = "uz8bnsGJZdOwuOqVC2iPqA==";

    private string GetJSONPath()
    {
        Debug.Log("Application.persistentDataPath ");

        Debug.Log(Application.persistentDataPath);

        return Application.persistentDataPath + "/LevelDetails.JSON";
    }

    public void UpdateLevelStar(int levelIndex, int starValue)
    {
        GameLevel gameLevel = GameManager.Instance.GetSavedGameData();

        List<LevelDetails> levelDetails = gameLevel.LevelDetails;

        LevelDetails levelInfo = levelDetails.FirstOrDefault(x => x.LevelIndex == levelIndex);

        if (starValue > levelInfo.Stars)
        {
            levelInfo.Stars = starValue;

            if (levelIndex == gameLevel.NextLevelToUnlock)
                gameLevel.NextLevelToUnlock += 1;

            WriteLevelData(gameLevel);

            GameManager.Instance.SetGameLevelData(gameLevel);
        }
    }

    public void WriteLevelData(GameLevel value) 
    {
        string gameData = JsonConvert.SerializeObject(value);

        using FileStream stream = File.Create(GetJSONPath());

        using Aes aesProvider = Aes.Create();
        aesProvider.Key = Convert.FromBase64String(Key);
        aesProvider.IV = Convert.FromBase64String(IV);

        using ICryptoTransform cryptoTransform = aesProvider.CreateEncryptor();

        using CryptoStream cryptoStream = new CryptoStream(
            stream,
            cryptoTransform,
            CryptoStreamMode.Write
            );

        cryptoStream.Write(Encoding.ASCII.GetBytes(gameData));
    }

    public GameLevel GetLevelData()
    {

        GameLevel gameLevel = GameLevelBuilder.GameLevelWithDefaultValue();

        if (!File.Exists(GetJSONPath()))
        {
            WriteLevelData(gameLevel);
        }
        else
        {
            byte[] fileBytes = File.ReadAllBytes(GetJSONPath());

            using Aes aesProvider = Aes.Create();
            aesProvider.Key = Convert.FromBase64String(Key);
            aesProvider.IV = Convert.FromBase64String(IV);

            using ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor(
                aesProvider.Key,
                aesProvider.IV
            );

            using MemoryStream decryptionStream = new MemoryStream(fileBytes);
            using CryptoStream cryptoStream = new CryptoStream(
                decryptionStream,
                cryptoTransform,
                CryptoStreamMode.Read
                );

            using StreamReader reader = new StreamReader(cryptoStream);

            string result = reader.ReadToEnd();

            gameLevel = JsonConvert.DeserializeObject<GameLevel>(result);
        }

        return gameLevel;
    }
}
