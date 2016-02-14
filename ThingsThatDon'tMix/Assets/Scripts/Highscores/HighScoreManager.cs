using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;

public class HighScoreManager : MonoBehaviour
{
    private static HighScoreManager instance = null;
    public static HighScoreManager Instance { get { return instance; } }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }

    private AzureStorageConsole.BlobHelper m_BlobHelper;
    public Scores CurrentScores;

	void Start ()
    {
        m_BlobHelper = new AzureStorageConsole.BlobHelper();
        CurrentScores = RetrieveScores();
    }

    public void UploadScores()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + "/TempData.dat");
        bf.Serialize(fs, CurrentScores);
        fs.Close();
        FileStream fs2 = File.Open(Application.persistentDataPath + "/TempData.dat", FileMode.Open);
        BinaryReader br = new BinaryReader(fs2);
        byte[] bin = br.ReadBytes(Convert.ToInt32(fs2.Length));
        string rawData = Convert.ToBase64String(bin);
        fs2.Close();
        File.Delete(Application.persistentDataPath + "/TempData.dat");
        m_BlobHelper.PutBlob("highscores", "Scores", rawData);
    }

    private Scores RetrieveScores()
    {
        Scores scoreReturn;
        string rawData;
        HttpWebResponse response = m_BlobHelper.GetBlob("highscores", "Scores");
        Stream inputStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(inputStream);
        rawData = reader.ReadToEnd();
        byte[] bin = Convert.FromBase64String(rawData);
        FileStream fs = File.Create(Application.persistentDataPath + "/TempData.dat");
        BinaryWriter bw = new BinaryWriter(fs);
        bw.Write(bin);
        fs.Close();
        FileStream fs2 = File.Open(Application.persistentDataPath + "/TempData.dat", FileMode.Open);
        BinaryFormatter bf = new BinaryFormatter();
        scoreReturn = (Scores)bf.Deserialize(fs2);
        fs2.Close();
        File.Delete(Application.persistentDataPath + "/TempData.dat");
        return scoreReturn;
    }

    private void SortScores()
    {
        List<KeyValuePair<string, uint>> _Scores = new List<KeyValuePair<string, uint>>();
        _Scores.Add(new KeyValuePair<string, uint>(CurrentScores.Score1.Name, CurrentScores.Score1.Value));
        _Scores.Add(new KeyValuePair<string, uint>(CurrentScores.Score2.Name, CurrentScores.Score2.Value));
        _Scores.Add(new KeyValuePair<string, uint>(CurrentScores.Score3.Name, CurrentScores.Score3.Value));
        _Scores.Add(new KeyValuePair<string, uint>(CurrentScores.Score4.Name, CurrentScores.Score4.Value));
        _Scores.Add(new KeyValuePair<string, uint>(CurrentScores.Score5.Name, CurrentScores.Score5.Value));

        _Scores.Sort((firstpair,nextpair) => { return firstpair.Value.CompareTo(nextpair.Value); });

        CurrentScores.Score1.Name = _Scores[0].Key;
        CurrentScores.Score1.Value = _Scores[0].Value;
        CurrentScores.Score2.Name = _Scores[1].Key;
        CurrentScores.Score2.Value = _Scores[1].Value;
        CurrentScores.Score3.Name = _Scores[2].Key;
        CurrentScores.Score3.Value = _Scores[2].Value;
        CurrentScores.Score4.Name = _Scores[3].Key;
        CurrentScores.Score4.Value = _Scores[3].Value;
        CurrentScores.Score5.Name = _Scores[4].Key;
        CurrentScores.Score5.Value = _Scores[4].Value;

        UploadScores();
    }

    public void InsertScore(string name, int score)
    {
        List<KeyValuePair<string, uint>> _Scores = new List<KeyValuePair<string, uint>>();
        if (score < CurrentScores.Score1.Value)
        {
            return;
        }
        else if(score > CurrentScores.Score1.Value)
        {
            _Scores.Add(new KeyValuePair<string, uint>(CurrentScores.Score1.Name, CurrentScores.Score1.Value));
            _Scores.Add(new KeyValuePair<string, uint>(CurrentScores.Score2.Name, CurrentScores.Score2.Value));
            _Scores.Add(new KeyValuePair<string, uint>(CurrentScores.Score3.Name, CurrentScores.Score3.Value));
            _Scores.Add(new KeyValuePair<string, uint>(CurrentScores.Score4.Name, CurrentScores.Score4.Value));
            _Scores.Add(new KeyValuePair<string, uint>(CurrentScores.Score5.Name, CurrentScores.Score5.Value));
            _Scores.Add(new KeyValuePair<string, uint>(name, (uint)score));

            _Scores.Sort((firstpair, nextpair) => { return firstpair.Value.CompareTo(nextpair.Value); });

            CurrentScores.Score1.Name = _Scores[1].Key;
            CurrentScores.Score1.Value = _Scores[1].Value;
            CurrentScores.Score2.Name = _Scores[2].Key;
            CurrentScores.Score2.Value = _Scores[2].Value;
            CurrentScores.Score3.Name = _Scores[3].Key;
            CurrentScores.Score3.Value = _Scores[3].Value;
            CurrentScores.Score4.Name = _Scores[4].Key;
            CurrentScores.Score4.Value = _Scores[4].Value;
            CurrentScores.Score5.Name = _Scores[5].Key;
            CurrentScores.Score5.Value = _Scores[5].Value;

            UploadScores();
        }
    }

    [Serializable]
    public class Score
    {
        public string Name;
        public uint Value;
    }

    [Serializable]
    public class Scores
    {
        public Score Score1;
        public Score Score2;
        public Score Score3;
        public Score Score4;
        public Score Score5;
    }
}
