using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Connection : MonoBehaviour
{
    [Header("Ollama Sever Settings")]
    [SerializeField] private string serverURL = "http://localhost:11434";
    [SerializeField] private string modelName = "gemma4:e2b";

    public IEnumerator OllamaConnect()
    {
        string url = $"{serverURL}/api/chat";

        string json =
            $@"{{
                ""model"":""{modelName}"",
                ""message"": [
                    {{
                        ""rule"": ""user"",
                        ""content"": ""안녕! 한 문장으로 자기를 소개해줘.""
                    }}
                ],
                ""stream"": false
            }}";
        
        // REST API 전송
        UnityWebRequest request = UnityWebRequest.Post(url, json, "application/json");
        Debug.Log("[접속] Ollama 서버에 요청 전송 중 ...");
        
        
    }
}
