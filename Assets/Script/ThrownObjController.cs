﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObjController : MonoBehaviour
{

    public bool thrownFlag;//既に投げられた後かのフラグ

    bool draggedFlag;//ドラッグされているかのフラグ

    const int historyNum = 5;

    //過去のこのオブジェクトの座標
    Vector3[] moveHistory = new Vector3[historyNum] { new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0) };

    Vector3 clickedMousePoint;//クリックされた時のマウス座標

    Vector3 clickedThisPoint;//クリックされた時のこのオブジェクトの座標

    // Use this for initialization
    void Start()
    {
        thrownFlag = false;
        draggedFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
      

        //マウスが上にあるときにクリックされたら
        if (checkBeMouseOver() && checkMousePressed() && draggedFlag == false)
        {
            draggedFlag = true;

            //ドラッグが始まった時の座標を突っ込む
            clickedMousePoint = getMouseVector3();
            clickedThisPoint = gameObject.transform.position;
        }

        //ドラッグされたフラグが立っていたら
        if (draggedFlag == true)
        {
            //実際にはドラッグされていなかったら = 離された所だったら
            //画面から出ていたら
            if (!checkMousePressed() || !checkInScreen(gameObject.transform.position))
            {
                draggedFlag = false;
                thrownThis();
            }
            //実際にドラッグされていたら
            else
            {
                followMouse();
            }
        }
        updateMoveHistory();
    }

    void updateMoveHistory()
    {
        //[0][1][2][3][4]to
        //[0][0][1][2][3]
        for (int i = 0; i < historyNum - 1; i++)
        {
            moveHistory[i + 1] = moveHistory[i];
        }

        //現在の座標を突っ込む
        moveHistory[0] = transform.position;
    }

    bool checkBeMouseOver()
    {
        Vector3 mousePoint = Input.mousePosition;

        Vector3 screenMousePoint = Camera.main.ScreenToWorldPoint(mousePoint);
        Collider2D existCollide = Physics2D.OverlapPoint(screenMousePoint);//空間上でコライダーがある地点を含むかチェックします
        //取得できていなければnullが入っている
        if (existCollide)
        {
            //このオブジェクトだったら
            if (existCollide.transform.gameObject == gameObject)
            {
                return true;
            }
        }
        return false;
    }

    Vector3 getMouseVector3()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    //オブジェクトをマウスの移動に沿って移動させる
    void followMouse()
    {
        //クリックされた時の座標に、クリックされた時のマウス座標と現在のマウス座標の差(=移動分)を足す
        transform.position = clickedThisPoint + (getMouseVector3() - clickedMousePoint);
    }


    bool checkMousePressed()
    {

        if (Input.GetMouseButton(0))
        {
            return true;
        }

        return false;
    }

    //投げる処理を書く
    void thrownThis()
    {
        Vector3 Force = new Vector3();
        for (int i = 0; i < historyNum - 1; i++)
        {
            //今のと一個次のとの差分を移動量として保存
            Force +=  moveHistory[i]- moveHistory[i + 1];
        }

        thrownFlag = true;

        gameObject.GetComponent<Rigidbody2D>().AddForce(Force);
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
