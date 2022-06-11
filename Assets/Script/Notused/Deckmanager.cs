using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//廃止
public class Deckmanager  {


	public 　static readonly List<CharPrefab> chars = new List<CharPrefab>();//全駒データリスト
	


	//北から時計回り
	//gamemanagerに入れるべき？
	//charsに全カードデータ挿入
	public void AllAdd(){
		chars.Add (new CharPrefab (0,1,0,4,"歩","ある（く）","ホ",1,0,0,0,0,0,0,0,"[チェンジ 金]",CharPrefab.ET.change,CharPrefab.MT.front));
		chars.Add (new CharPrefab (1,2,0,4,"香","か","コウ",8,0,0,0,0,0,0,0,"[チェンジ 金]",CharPrefab.ET.change,CharPrefab.MT.front));
		chars.Add (new CharPrefab (2,2,0,4,"馬","うま","バ",0,2,0,0,0,0,0,2,"[チェンジ 金]",CharPrefab.ET.change,CharPrefab.MT.straght));
		chars.Add (new CharPrefab (3,3,0,4,"銀","","ギン",1,1,0,1,0,1,0,1,"[チェンジ 金]",CharPrefab.ET.change,CharPrefab.MT.front));
		chars.Add (new CharPrefab (4,3,0,4,"金","かね","キン",1,1,1,0,1,0,1,1,"",CharPrefab.ET.None,CharPrefab.MT.front));
		chars.Add (new CharPrefab (5,4,0,7,"飛","と（ぶ）","ヒ",8,0,8,0,8,0,8,0,"範囲＋王",CharPrefab.ET.movechange,CharPrefab.MT.front));
		chars.Add (new CharPrefab (6,4,0,7,"角","かど","カク",0,8,0,8,0,8,0,8,"範囲＋王",CharPrefab.ET.movechange,CharPrefab.MT.front));
		chars.Add (new CharPrefab (7,5,0,0,"王","","オウ",1,1,1,1,1,1,1,1,"",CharPrefab.ET.Every,CharPrefab.MT.front));
		chars.Add (new CharPrefab (8,5,0,0,"玉","たま","ギョク",1,1,1,1,1,1,1,1,"",CharPrefab.ET.Trap,CharPrefab.MT.front));
		chars.Add(new CharPrefab(9,2,4,38,"猫","ねこ","ビョウ",1,1,0,0,0,0,0,1,"[チェンジ 虎]",CharPrefab.ET.change,CharPrefab.MT.straght));
		// ver1
		chars.Add(new CharPrefab(10,1,1,4,"亜","","",1,0,0,0,1,0,0,0,"[チェンジ 金]",CharPrefab.ET.change,CharPrefab.MT.front));
		chars.Add(new CharPrefab(11,1,1,3,"吉","","",2,0,0,0,0,0,0,0,"[チェンジ 銀]",CharPrefab.ET.change,CharPrefab.MT.front));
		chars.Add(new CharPrefab(12,1,1,0,"甲","","",0,0,1,0,0,0,1,0,"",CharPrefab.ET.None,CharPrefab.MT.front));
		chars.Add(new CharPrefab(13,1,1,0,"忌","","",1,0,0,0,0,0,0,0,"範囲に＋N1,S2",CharPrefab.ET.movechange,CharPrefab.MT.front));
		chars.Add(new CharPrefab(14,1,2,4,"岐","","",0,1,0,0,0,0,0,1,"[チェンジ 金]",CharPrefab.ET.change,CharPrefab.MT.front));
		chars.Add(new CharPrefab(15,1,2,2,"蛮","","",1,1,0,0,0,0,0,0,"[チェンジ 馬]",CharPrefab.ET.change,CharPrefab.MT.front));
		chars.Add(new CharPrefab(16,1,3,0,"凍","","",1,0,0,0,0,0,0,0,"[トラップ]取ったコマを次の相手ターン行動不能にする",CharPrefab.ET.Trap,CharPrefab.MT.front));
		chars.Add(new CharPrefab(17,1,2,0,"刃","","",0,1,0,0,0,0,0,1,"S1を追加して範囲＋１",CharPrefab.ET.OneTime,CharPrefab.MT.front));
		chars.Add(new CharPrefab(18,1,5,7,"仙","","",1,0,0,0,0,0,0,0,"[チェンジ 王]",CharPrefab.ET.change,CharPrefab.MT.front));
		chars.Add(new CharPrefab(19,1,1,0,"偽","","",0,0,0,0,0,0,0,0,"動かない[トラップ]とったコマを自ストックに加える",CharPrefab.ET.Trap,CharPrefab.MT.front));
		chars.Add(new CharPrefab(20,1,1,3,"忍","","",2,0,0,0,0,0,0,0,"[チェンジ 銀]",CharPrefab.ET.change,CharPrefab.MT.front));
		chars.Add(new CharPrefab(21,3,3,6,"疾","","",0,8,0,0,0,0,0,8,"[チェンジ 角]",CharPrefab.ET.change,CharPrefab.MT.front));
		chars.Add(new CharPrefab(22,2,1,4,"貫","","",2,0,0,0,0,0,0,0,"[チェンジ 金]",CharPrefab.ET.change,CharPrefab.MT.straght));
		chars.Add(new CharPrefab(23,2,1,0,"憩","","",1,0,1,0,0,0,1,0,"[トラップ]自ストックになる",CharPrefab.ET.Trap,CharPrefab.MT.front));
		chars.Add(new CharPrefab(24,2,1,0,"軸","","",0,0,8,0,0,0,8,0,"",CharPrefab.ET.None,CharPrefab.MT.front));
		chars.Add(new CharPrefab(25,2,2,3,"匠","","",0,1,1,0,0,0,1,1,"[チェンジ 銀]",CharPrefab.ET.change,CharPrefab.MT.front));
		chars.Add(new CharPrefab(26,2,1,4,"滑","","",3,0,0,0,3,0,0,0,"[チェンジ 金]",CharPrefab.ET.change,CharPrefab.MT.front));
		chars.Add(new CharPrefab(27,2,1,4,"哀","","",1,0,0,1,1,1,0,0,"[チェンジ 金]",CharPrefab.ET.change,CharPrefab.MT.front));
		chars.Add(new CharPrefab(28,2,3,0,"削","","",1,0,1,0,0,0,1,0,"[トラップ]敵ストックをランダムに破壊する",CharPrefab.ET.Trap,CharPrefab.MT.front));
		chars.Add(new CharPrefab(29,2,2,0,"軌","","",1,1,1,1,1,1,1,1,"",CharPrefab.ET.None,CharPrefab.MT.front));
		chars.Add(new CharPrefab(30,2,4,0,"癖","","",8,0,0,1,8,1,0,0,"[チェンジ 歩]",CharPrefab.ET.change,CharPrefab.MT.front));
		chars.Add(new CharPrefab(31,3,4,31,"潤","","",0,2,2,0,0,0,2,2,"潤をストックに加える",CharPrefab.ET.OneTime,CharPrefab.MT.front));
		chars.Add(new CharPrefab(32,3,5,7,"弧","","",1,1,0,0,0,0,0,1,"[トラップ]王の範囲に自コマがいる場合取られない",CharPrefab.ET.Trap,CharPrefab.MT.front));
		chars.Add(new CharPrefab(33,3,1,7,"魔","","",1,1,1,1,1,1,1,1,"全方位＋１",CharPrefab.ET.movechange,CharPrefab.MT.front));
		chars.Add(new CharPrefab(34,3,5,7,"滅","","",1,0,0,0,0,0,0,0,"[チェンジ 王]範囲中の全てのコマを破壊する",CharPrefab.ET.OneTime,CharPrefab.MT.front));
		chars.Add(new CharPrefab(35,3,1,35,"崩","","",1,1,0,1,0,1,0,1,"[トラップ]自ストックに加える",CharPrefab.ET.Trap,CharPrefab.MT.front));
		chars.Add(new CharPrefab(36,3,1,0,"衝","","",2,2,0,0,0,0,0,2,"",CharPrefab.ET.None,CharPrefab.MT.front));
		chars.Add(new CharPrefab(37,3,1,0,"擁","","",8,0,0,0,8,0,0,0,"[ブロック１]",CharPrefab.ET.Trap,CharPrefab.MT.front));
		chars.Add(new CharPrefab(38,3,3,9,"虎","とら","コ",1,1,0,1,1,1,0,1,"[トラップ]猫をストックに加える",CharPrefab.ET.Trap,CharPrefab.MT.front));
		chars.Add(new CharPrefab(39,3,4,40,"人","ひと","ジン",1,0,0,0,0,0,0,0,"神になる",CharPrefab.ET.change,CharPrefab.MT.front));
		chars.Add(new CharPrefab(40,4,4,0,"神","","",8,8,8,8,8,8,8,8,"",CharPrefab.ET.change,CharPrefab.MT.front));
		//chars.Add(new CharPrefab(39,0,0,0,"角","","",1,8,1,8,1,8,1,8,"",CharPrefab.ET.change,CharPrefab.MT.front));
		/*
		chars.Add(new CharPrefab(39,3,2,0,"涼","","",0,3,0,0,0,0,0,3,"範囲＋N3,S3",CharPrefab.ET.movechange,CharPrefab.MT.front));
		chars.Add(new CharPrefab(40,3,4,5,"弦","","",1,0,8,0,1,0,8,0,"[チェンジ 飛]",CharPrefab.ET.change,CharPrefab.MT.front));
		chars.Add(new CharPrefab(41,4,5,0,"幼","","",1,1,1,1,1,1,1,1,"[トラップ]範囲中の敵コマの全移動範囲をN1にする",CharPrefab.ET.Trap,CharPrefab.MT.front));
		chars.Add(new CharPrefab(42,4,2,0,"奪","","",0,8,0,8,0,8,0,8,"[アタック]ランダムに敵ストックのコマを１駒自ストックにする",CharPrefab.ET.Atack,CharPrefab.MT.front));
		chars.Add(new CharPrefab(43,4,4,0,"覆","","",0,0,8,0,0,0,8,0,"[ブロック１][トラップ]アタックした敵コマを破壊する。その後このコマの効果はなくなる",CharPrefab.ET.Trap,CharPrefab.MT.front));
		chars.Add(new CharPrefab(44,4,5,0,"霊","","",2,1,0,1,0,1,0,1,"[アタック]ストックにした敵コマに変化する",CharPrefab.ET.Atack,CharPrefab.MT.front));
		chars.Add(new CharPrefab(45,4,1,45,"賊","","",2,0,2,0,2,0,2,0,"[トラップ]自ストックになる",CharPrefab.ET.Trap,CharPrefab.MT.front));
		chars.Add(new CharPrefab(46,4,1,0,"鯨","","",3,3,3,3,3,3,3,3,"",CharPrefab.ET.None,CharPrefab.MT.front));
		chars.Add(new CharPrefab(47,4,1,7,"騎","","",3,0,0,0,0,0,0,0,"[チェンジ 王]",CharPrefab.ET.change,CharPrefab.MT.straght));
		chars.Add(new CharPrefab(48,4,3,0,"凹","","",0,5,5,5,5,5,5,5,"",CharPrefab.ET.None,CharPrefab.MT.front));
		chars.Add(new CharPrefab(49,4,3,0,"凸","","",5,0,5,5,5,5,5,0,"",CharPrefab.ET.None,CharPrefab.MT.front));
		chars.Add(new CharPrefab(50,5,5,0,"帝","","",2,2,0,1,1,1,0,2,"全方向＋２",CharPrefab.ET.movechange,CharPrefab.MT.straght));
		chars.Add(new CharPrefab(51,5,5,0,"獄","","",2,1,1,1,2,1,1,1,"この駒以外で同じ列のコマ全て破壊する",CharPrefab.ET.OneTime,CharPrefab.MT.front));
		chars.Add(new CharPrefab(52,5,2,0,"魂","","",1,1,1,0,1,0,1,1,"[ブロック３][トラップ]アタックしてきた敵駒を破壊する",CharPrefab.ET.Trap,CharPrefab.MT.front));
		chars.Add(new CharPrefab(53,5,3,0,"槍","","",1,0,0,0,1,0,0,0,"全方向+∞",CharPrefab.ET.movechange,CharPrefab.MT.front));
		chars.Add(new CharPrefab(54,5,4,4,"魏","","",8,1,1,0,0,1,1,1,"[チェンジ 金]全範囲+1",CharPrefab.ET.OneTime,CharPrefab.MT.front));
		chars.Add(new CharPrefab(55,5,4,0,"呉","","",1,1,1,1,1,1,1,1,"[ブロック 2][アタック]敵ブロックを無視する",CharPrefab.ET.Atack,CharPrefab.MT.front));
		chars.Add(new CharPrefab(56,5,4,57,"蜀","","",1,0,2,1,1,1,2,0,"劉コマ2枚自ストックに加える",CharPrefab.ET.OneTime,CharPrefab.MT.front));
		chars.Add(new CharPrefab(57,5,0,0,"劉","","",2,2,0,0,2,0,0,2,"",CharPrefab.ET.None,CharPrefab.MT.straght));
		chars.Add(new CharPrefab(59,5,5,0,"呂","","",3,3,3,3,3,3,3,3,"[アタック]取った駒の左右の駒を破壊する。自分はストックを使えない",CharPrefab.ET.Atack,CharPrefab.MT.front));
		chars.Add(new CharPrefab(60,5,5,0,"竜","","",1,2,1,1,1,1,1,2,"相手ストックを全て破壊する",CharPrefab.ET.OneTime,CharPrefab.MT.straght));
		*/
}

public CharPrefab PrivateLoad(int i){
	CharPrefab cc = new CharPrefab(chars[i].id,chars[i].rank,chars[i].rear,chars[i].efectnumber,chars[i].name,chars[i].rub1,chars[i].rub2,chars[i].move[0],chars[i].move[1],chars[i].move[2],chars[i].move[3],chars[i].move[4],chars[i].move[5],chars[i].move[6],chars[i].move[7],chars[i].efect,chars[i].Type,chars[i].MType);
	return cc;
}

public int KomaCount(){
	if(chars != null){
		return chars.Count;
	}
	return 0;
}

public string GetName(int i){
	if(chars[i] != null){
		return chars[i].name;
	}
	return "";
}

}
