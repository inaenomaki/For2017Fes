using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerController : MonoBehaviour
{
    //Title Variable
    //


    //Game Variable
    //

    //投げられる顔のパーツ
    public GameObject[] thrownParts = new GameObject[6];

    //答えの位置を持っているオブジェクトを保持して点数計算の結果を取得する
    public GameObject[] correctParts = new GameObject[6];

    private int point;

    private bool endGameFlag;

    //Result Variable
    //

    private GameObject targetCamera;

    private GameObject resultUI;



    public void init()
    {
        point = 0;
        endGameFlag = false;
    }

    // Use this for initialization
    void Start()
    {
        init();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            updateTitle();
        }

        if (SceneManager.GetActiveScene().name == "Game")
        {
            //もうゲームが終わっていたら
            if (checkGameEnd() == true)
            {
                endGameFlag = true;

                //リザルト中は見えないようにする
                GameObject pointText = GameObject.Find("Result").transform.FindChild("Point").gameObject ;
                pointText.SetActive(false);
            }



            if (endGameFlag == false)
            {
                updateGame();
            }
            else
            {
                updateResult();
            }
        }


    }

    void updateTitle()
    {

    }

    void updateGame()
    {
        correctParts[0] = GameObject.Find("correctMayu_L");
        correctParts[1] = GameObject.Find("correctMayu_R");
        correctParts[2] = GameObject.Find("correctEye_L");
        correctParts[3] = GameObject.Find("correctEye_R");
        correctParts[4] = GameObject.Find("correctNose");
        correctParts[5] = GameObject.Find("correctMouse");

        Text pointText = GameObject.Find("Result").transform.FindChild("Point").GetComponent<Text>();

        int answerSum = 0;
        for (int i = 0; i < 6; i++)
        {
            answerSum += correctParts[i].GetComponent<CorrectPosController>().point;
        }

        this.point = answerSum / 6;

        pointText.text = point + "点";
    }

    void updateResult()
    {
        //カメラのソースのコンポーネントを取得
        TargetCameraController targetCameraController = GameObject.Find("Target Camera").GetComponent<TargetCameraController>();
        Text resultText = GameObject.Find("Result").transform.FindChild("Text").GetComponent<Text>();
        GameObject toTitleButton = GameObject.Find("Result").transform.FindChild("ToTitleButton").gameObject;


        //右上を拡大
        targetCameraController.zoomIn();

        //拡大が終わり切ったら
        if (targetCameraController.getSize() >= 1)
        {
            resultText.text = point + "点!!";

            toTitleButton.SetActive(true);

        }
    }

    bool checkGameEnd()
    {

        thrownParts[0] = GameObject.Find("mayu_L");
        thrownParts[1] = GameObject.Find("mayu_R");
        thrownParts[2] = GameObject.Find("eye_L");
        thrownParts[3] = GameObject.Find("eye_R");
        thrownParts[4] = GameObject.Find("nose");
        thrownParts[5] = GameObject.Find("mouse");
        

        float endGameVelocity = 0.7f;//この速度以下になったら終了する


        //終了しない条件
        //・投げられるものがまだ存在している
        //・まだものが動いている
        //

        for (int i = 0; i < 6; i++)
        {
            //掴める位置にあったら
            if (checkInScreen(thrownParts[i].transform.position))
            {
                return false;
            }

            //velocityが大きい、つまり動いているものがあったら
            if (thrownParts[i].GetComponent<Rigidbody2D>().velocity.magnitude >= endGameVelocity)
            {
                return false;
            }
        }

        //1つも、「掴めるものがなく」もしくは「動いている」ものがなかったならゲームは終了
        return true;
    }

    bool checkInScreen(Vector3 pos)
    {
        Vector3 view_pos = Camera.main.WorldToViewportPoint(pos);
        if (view_pos.x < -0.0f ||
           view_pos.x > 1.0f ||
           view_pos.y < -0.0f ||
           view_pos.y > 1.0f)
        {
            // 範囲外 
            return false;
        }
        // 範囲内 
        return true;
    }
}
