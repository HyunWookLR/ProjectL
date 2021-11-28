using System;
using System.IO;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;
public static class FileCreator
{
    public async static void CreateAsync(string path, string fileName, string fileData)
    {
        var fileStream = new FileStream(string.Format($"{path}/{fileName}"), FileMode.Create);
        var data = Encoding.UTF8.GetBytes(fileData);
        await fileStream.WriteAsync(data, 0, data.Length);
        fileStream.Close();
    }

    public async static void CreateAsync(string path, string fileName, byte[] fileData)
    {
        var fileStream = new FileStream(string.Format($"{path}/{fileName}"), FileMode.Create);
        await fileStream.WriteAsync(fileData, 0, fileData.Length);
        fileStream.Close();
    }
}

public static class FileLoader
{
    public static string LoadString(string path, string fileName, string fileFormat)
    {
        try
        {
            var fileStream = new FileStream(string.Format($"{path}/{fileName}.{fileFormat}"), FileMode.Open);
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();
            return Encoding.UTF8.GetString(data);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return "";
        }
    }
    public static Sprite LoadSprite(string path, string fileName, string fileFormat)
    {
        const int defaultTextureSize = 2;
        try
        {
            var fileStream = new FileStream(string.Format($"{path}/{fileName}.{fileFormat}"), FileMode.Open);
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();
            Texture2D tex = new Texture2D(defaultTextureSize, defaultTextureSize);
            tex.LoadImage(data);
            return Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), Vector2.zero);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return Sprite.Create(new Texture2D(defaultTextureSize, defaultTextureSize), new Rect(0f, 0f, defaultTextureSize * 50, defaultTextureSize * 50), Vector2.zero);
        }
    }

    public static T LoadJson<T>(string path, string fileName, string fileFormat)
    {
        try
        {
            var fileStream = new FileStream(string.Format($"{path}/{fileName}.{fileFormat}"), FileMode.Open);
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();
            var jsonData = Encoding.UTF8.GetString(data);
            return JsonConvert.DeserializeObject<T>(jsonData);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return default(T);
        }
    }
}
