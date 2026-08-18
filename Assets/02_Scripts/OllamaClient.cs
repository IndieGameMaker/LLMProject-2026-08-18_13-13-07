using System;
using UnityEngine;

// Ollama REST API 사용해서 통신
// 엔드포인트 => POST http://localhost:11434/api/chat
public class OllamaClient : MonoBehaviour
{
    // 엔드포인트
    private const string API_URL = "http://localhost:11434/api/chat";
    // 모델
    private const string MODEL = "gemma4:e2b";
    
    // 이벤트 : LLM 응답 도착 / 로딩 상태 변경
    public static event Action<string> OnResponseReceived;
    public static event Action<bool> OnLoadingChanged;
    
    
}
