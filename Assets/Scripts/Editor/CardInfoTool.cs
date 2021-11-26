using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

public class CardInfoTool : EditorWindow
{
    private const string dataPath = "/Resources/Json/CardInfo.json";
    private List<CardInfo> cardList = new List<CardInfo>();

    [MenuItem("CustomTool/CardInfoTableTool")]
    public static void ShowWindow()
    {
        GetWindow<CardInfoTool>("CardInfoTool");
    }

    private void OnGUI()
    {
        var style = new GUIStyle();
        GUILayout.Label("<size=15><color=red>ī���� ���� ���̺��� �����մϴ�.</color></size>\n", style);


        if (GUILayout.Button("Load If File Exist"))
        {
            Load();
        }

        SetVariables();

        if (GUILayout.Button("ī�� �߰�"))
        {
            cardList.Add(new CardInfo());
        }

        if (GUILayout.Button("������ ī�� ����"))
        {
            if (cardList.HasItem() && EditorUtility.DisplayDialog("������ ī�� ����", "������ �����Ͻðڽ��ϱ�?", "Ok", "Cancel"))
            {
                cardList.Remove(cardList.Last());
            }
        }

        if (GUILayout.Button("Save"))
        {
            if (EditorUtility.DisplayDialog("Save Data", "������ ������ ����/������ �Ͻðڽ��ϱ�?", "Ok", "Cancel"))
            {
                var jsonData = JsonConvert.SerializeObject(cardList);
                Debug.Log("Json Serialize: \n" + jsonData);
                Save(jsonData);
            }
        }
    }

    private void SetVariables()
    {
        if (cardList.HasItem())
        {
            foreach (var card in cardList)
            {
                card.SetId(EditorGUILayout.IntField("Id", card.Id));
                card.SetHp(EditorGUILayout.IntField("Hp", card.Hp));
                card.SetAtk(EditorGUILayout.IntField("Atk", card.Atk));
                card.SetCardName(EditorGUILayout.TextField("Name", card.CardName));

                DrawUILine(Color.green);
            }
        }
    }

    private bool TryLoadCardInfo(out List<CardInfo> cardInfo)
    {
        var fullPath = string.Format($"{Application.dataPath}{dataPath}");
        if (File.Exists(fullPath))
        {
            FileStream fileStream = new FileStream(fullPath, FileMode.Open);
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();
            string jsonData = Encoding.UTF8.GetString(data);
            cardInfo = JsonConvert.DeserializeObject<CardInfo[]>(jsonData).ToList();
            return true;
        }
        cardInfo = null;
        return false;
    }

    private void Load()
    {
        if (!TryLoadCardInfo(out cardList))
        {
            EditorUtility.DisplayDialog("File Not Found", "������ ã�� �� �����ϴ�.", "Ok");
            if (cardList.NotHaveItem())
            {
                cardList.Add(new CardInfo());
            }
        }
    }

    private void Save(string jsonData)
    {
        var fullPath = string.Format($"{Application.dataPath}{dataPath}");
        FileStream fileStream = new FileStream(fullPath, FileMode.Create);
        var data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    private void DrawUILine(Color color, int thickness = 2, int padding = 10)
    {
        Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
        r.height = thickness;
        r.y += padding / 2;
        r.x -= 2;
        r.width += 6;
        EditorGUI.DrawRect(r, color);
    }
}
