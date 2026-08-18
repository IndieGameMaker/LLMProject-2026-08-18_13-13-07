using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Connection))]
public class ConnectionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 기본 UI는 드로잉
        DrawDefaultInspector();
        // 접근할 대상 클래스 할당
        var connection = (Connection)target;
        // 버튼 생성
        if (GUILayout.Button("연결 테스트") == true)
        {
            // Connection 클래스의 OllamaConnect 코루틴을 호출
            connection.StartCoroutine(connection.OllamaConnect());
        }
        
    }
}
