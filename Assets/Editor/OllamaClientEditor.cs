using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OllamaClient))]
public class OllamaClientEditor : Editor
{
    private string _systemMessage = "마법 왕국의 현자이자 마법 도서관의 수호자 마법사 입니다.";
    private string _userMessage = "안녕하세요. 저는 방랑검객 잭입니다.";

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var client = (OllamaClient)target;
        
        // 입력필드 표시
        _systemMessage = EditorGUILayout.TextField("System Message", _systemMessage);
        _userMessage = EditorGUILayout.TextField("User Message", _userMessage);

        if (GUILayout.Button("요청 보내기"))
        {
            var messages = new List<OllamaMessage>
            {
                new OllamaMessage{ role = "system", content = _systemMessage},
                new OllamaMessage{ role = "user", content = _userMessage}
            };
            
            client.SendChat(messages);
        }
    }
}
