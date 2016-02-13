using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;

public class HighScoreManager : MonoBehaviour
{

    private AzureStorageConsole.BlobHelper m_BlobHelper;
    private Scores CurrentScores;

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

    }

    [Serializable]
    private class Score
    {
        public string Name;
        public uint Value;
    }

    [Serializable]
    private class Scores
    {
        public Score Score1;
        public Score Score2;
        public Score Score3;
        public Score Score4;
        public Score Score5;
    }
}
