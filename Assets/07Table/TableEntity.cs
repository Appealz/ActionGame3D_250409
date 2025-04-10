using System;
using UnityEngine;

// ���̺� �ִ� �����͵��� Ÿ���� ����.
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
    // ���򿡴� ����Ƽ���� ��� ��������.
    // �������� ���ö���¡�� ���ؼ� �� �������־������.

    // �������� �� id������ ����� �е� 
    // id���� �ش��ϴ� �� ������.
   
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


