using UnityEngine;

//建立素材選項
[CreateAssetMenu(menuName ="人形/對話資料",fileName ="對話資料")]
public class 存放對話資料 : ScriptableObject
{
    public string 對話者名稱1;
    public string 對話者名稱2;
    public bool[] a, b;
    [TextArea(2,5)]
    public string[] 對話內容;
}
