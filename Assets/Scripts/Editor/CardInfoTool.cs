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
        GUILayout.Label("<size=15><color=red>카드의 정보 테이블을 생성합니다.</color></size>\n", style);


        if (GUILayout.Button("Load If File Exist"))
        {
            Load();
        }

        SetVariables();

        if (GUILayout.Button("카드 추가"))
        {
            cardList.Add(new CardInfo());
        }

        if (GUILayout.Button("마지막 카드 삭제"))
        {
            if (cardList.HasItem() && EditorUtility.DisplayDialog("마지막 카드 삭제", "정말로 삭제하시겠습니까?", "Ok", "Cancel"))
            {
                cardList.Remove(cardList.Last());
            }
        }

        if (GUILayout.Button("Save"))
        {
            if (EditorUtility.DisplayDialog("Save Data", "정말로 파일을 생성/덮어씌우기 하시겠습니까?", "Ok", "Cancel"))
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
            EditorUtility.DisplayDialog("File Not Found", "파일을 찾을 수 없습니다.", "Ok");
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
