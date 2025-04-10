using System;
using UnityEngine;

// 테이블에 있는 데이터들의 타입을 지정.
[Serializable]
public class ItemData_Entity
{
    public int id;
    public string name;
    public string iconImg;
    public int sellGold;
    public int attackDamage;
    public int attackRange;
    public int attackRate;
    public bool equip;
}

[Serializable]
public class TipData_Entity
{
    public int id;
    public string sceneName;
    public string tipText;
}

[Serializable]
public class Language_Entity
{
    // 요즘에는 유니티에서 언어 지원을함.
    // 이전에는 로컬라이징을 통해서 언어를 변경해주었어야함.

    // 여러가지 언어를 id에따라 만들어 둔뒤 
    // id값에 해당하는 언어를 가져옴.
   
    public int id;
    public string KOR;
    public string ENG;    
}

[Serializable]
public class Monster_Entity
{
    public int id;
    public string monsterName;
    public int monsterType;
    public int moveSpeed;
    public int attackDamage;
    public int maxHP;
    public int dropID;
    public int dropRate;
    public int EXP;
}

[Serializable]
public class EXP_Entity
{
    public int curLevel;
    public int nextEXP;
}


