using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResponseDatabase", menuName = "Custom Data/Response Database")]
public class NullResponses : ScriptableObject
{
    [TextArea(2, 3)]
    [SerializeField] List<string> responses = new List<string>();

    public List<string> Responses { get { return responses; } } //public getter.

}//class end.