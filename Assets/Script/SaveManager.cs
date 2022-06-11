using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System; // UnityJsonを使う場合に必要
using System.IO; // ファイル書き込みに必要

// [Serializable] をつけないとシリアライズできない

[Serializable]
public struct KIFUData{
    public int turn;
    public string player;
    public string Koma;
    public int beforeX;
    public int beforeY;
    public int afterX;
    public int afterY; 
}

/// <summary>
/// セーブデータ管理
/// </summary>
public class SaveManager {

  string fileNum;
  string path;

  // 保存するファイル
  string SAVE_FILE_PATH;
  int turn = 1;

  StreamWriter writer;

    //セーブが終わるまではこのインスタンスを使い続ける
    public SaveManager(){
        fileNum = System.DateTime.Now.Year.ToString() + System.DateTime.Now.Day.ToString() +  System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Second.ToString();
        Debug.Log(fileNum);
        SAVE_FILE_PATH = "kifu_" + fileNum + ".txt";

        //最初にファイルを作成
        var data = new KIFUData();
        string line = "start";
        //JSONシリアアライズ
        //var json = JsonUtility.ToJson(data);
        //Assetフォルダに保存
        path = Application.dataPath + "/KIFUData/" + SAVE_FILE_PATH;
        writer = new StreamWriter(path,false);//上書き
        //writer.WriteLine (json);
        writer.WriteLine (line);
        //writer.Flush ();
        //writer.Close ();
    }


    public void KIFU(string play,string KomaName,int beforeV,int beforeH,int afterV,int afterH){
        var data = new KIFUData();
        /* 
        data.player = play;
        data.turn = turn;
        data.Koma = KomaName;
        data.beforeX = beforeV;
        data.beforeY = beforeH;
        data.afterX = afterV;
        data.afterY = afterH;
        */
        if(play != "end"){
            string line = String.Format("ターン数:{0},{1},beforeV:{2},beforeH:{3},afterV:{4},afterH:{5}",turn,KomaName,beforeV.ToString(),beforeH.ToString(),afterV.ToString(),afterH.ToString());
            Debug.Log(line);

            //JSONシリアアライズ
            //var json = JsonUtility.ToJson(data);
            //Assetフォルダに保存
            //path = Application.dataPath + "/KIFUData/" + SAVE_FILE_PATH;
            //var writer = new StreamWriter(path,false);
            //writer.WriteLine(json);
            writer.WriteLine(line);
            turn++;
        }else{
            writer.Flush ();
            writer.Close ();
            turn = 1;
        }
        

      
    }

}

