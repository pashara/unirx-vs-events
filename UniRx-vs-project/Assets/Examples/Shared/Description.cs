using UnityEngine;

namespace Examples.Shared
{
    public class Description : MonoBehaviour
    {
        [TextArea(5, 50)]
        [SerializeField] private string description;
    }
}