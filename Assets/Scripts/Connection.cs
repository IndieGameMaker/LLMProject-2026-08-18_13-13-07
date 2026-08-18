using UnityEngine;

public class Connection : MonoBehaviour
{
    [Header("Ollama Sever Settings")]
    [SerializeField] private string serverURL = "http://localhost:11434";
    [SerializeField] private string modelName = "gemma4:e2b";
}
