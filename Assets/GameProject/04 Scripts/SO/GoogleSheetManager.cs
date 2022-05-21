using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetManager : MonoBehaviour
{
    //1번시트
    const string URL = "https://docs.google.com/spreadsheets/d/1L2FWK8NyCipB0KA-n64m0UNP8RZroxDGt7zicYdNjv8/export?format=tsv&range=A2:H";
    //2번 시트
    const string URL2 = "https://docs.google.com/spreadsheets/d/1L2FWK8NyCipB0KA-n64m0UNP8RZroxDGt7zicYdNjv8/export?format=tsv&gid=1236819615&range=A2:H";
    //3번 시트 DialogDB
    const string URL3 = "https://docs.google.com/spreadsheets/d/1L2FWK8NyCipB0KA-n64m0UNP8RZroxDGt7zicYdNjv8/export?format=tsv&gid=1012642768&range=A2:B";



    [SerializeField] RTSDB rtsDB;

    void Start()
    {
        StartCoroutine(DownloadRTSDB());
    }

    IEnumerator DownloadRTSDB()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();
        string data = www.downloadHandler.text;
        print(data);
        SetUnitDB(data);

        UnityWebRequest www2 = UnityWebRequest.Get(URL2);
        yield return www2.SendWebRequest();
        string data2 = www2.downloadHandler.text;
        print(data2);
        SetBuildingDB(data2);

        UnityWebRequest www3 = UnityWebRequest.Get(URL3);
        yield return www3.SendWebRequest();

        string data3 = www3.downloadHandler.text;
        print(data3);
        SetDialogDB(data3);
    }

    IEnumerator DownloadUnitDB()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        print(data);

        SetUnitDB(data);
    }
    IEnumerator DownloadBuildingDB()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL2);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        print(data);

        SetBuildingDB(data);
    }

    IEnumerator DownloadDialogDB()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL3);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        print(data);
        SetDialogDB(data);
    }
    void SetUnitDB(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;
        int columSize = row[0].Split('\t').Length;

        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');
            for (int j = 0; j < columSize; j++)
            {
                UnitData unitData = rtsDB.UnitDB[i];
                unitData.unitid = int.Parse(column[0]);
                unitData.name = column[1];
                unitData.price = int.Parse(column[2]);
                unitData.maxhp = float.Parse(column[3]);
                unitData.damage = float.Parse(column[4]);
                unitData.attackdistance = float.Parse(column[5]);
                unitData.searchrange = float.Parse(column[6]);
                unitData.movespeed = float.Parse(column[7]);

            }
        }
    }
    void SetBuildingDB(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;
        int columSize = row[0].Split('\t').Length;

        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');
            for (int j = 0; j < columSize; j++)
            {
                BuildingData buildingData = rtsDB.BuildingDB[i];
                buildingData.buildingid = int.Parse(column[0]);
                buildingData.name = column[1];
                buildingData.price = int.Parse(column[2]);
                buildingData.maxhp = float.Parse(column[3]);
                buildingData.damage = float.Parse(column[4]);
                buildingData.attackdistance = float.Parse(column[5]);
                buildingData.searchrange = float.Parse(column[6]);
                buildingData.income = int.Parse(column[7]);
            }
        }
    }

    void SetDialogDB(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;
        int columSize = row[0].Split('\t').Length;

        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');
            for (int j = 0; j < columSize; j++)
            {
                DialogData curdialog = rtsDB.DialogDB[i];
                curdialog.branch = int.Parse(column[0]);
                curdialog.dialog = column[1];
            }
        }
    }
}
