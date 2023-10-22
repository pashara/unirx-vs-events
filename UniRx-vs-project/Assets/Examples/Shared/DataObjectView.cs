using UnityEngine;
using UnityEngine.UI;

namespace Examples.Shared
{
    public class DataObjectView : MonoBehaviour
    {
        [SerializeField] private Text label;
        
        public void Initialize(DataObject dataObject)
        {
            label.text = dataObject.ToString();
        }
    }
}