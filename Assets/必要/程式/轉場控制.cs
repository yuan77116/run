using UnityEngine;
using UnityEngine.SceneManagement; //引用場景控制

public class 轉場控制 : MonoBehaviour
{
    public int 關卡int =1;
    private string 前往場景 = null;
    public string 直達輸入關卡=null;

    public void 載入場景等待()
    {
        前往場景 = ("關卡" + 關卡int);
        Invoke("載入場景", 0.7f);  //等待0.7s執行 public void 載入場景()
    }
    private void 載入場景()
    {
        SceneManager.LoadScene(前往場景);
        print("載入場景名： " + 前往場景);
        Time.timeScale = 1;
    }
    public void 離開程式()
    {
        Time.timeScale = 1;
        Application.Quit();
        print("離開遊戲");
    }
    public void 回主介面()
    {
        SceneManager.LoadScene("主介面");
        print("回主介面");
        Time.timeScale = 1;
    }
    public void 直接下一關()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }
    public void 使用直達()
    {
        Time.timeScale = 1;
        前往場景 = (直達輸入關卡);
        Invoke("載入場景", 0.7f);
    }
}

