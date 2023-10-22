using EasyButtons;
using UnityEngine;

public class LogSeparator : MonoBehaviour
{
    [Button]
    private void Separate()
    {
        Debug.Log("===");
    }
}
