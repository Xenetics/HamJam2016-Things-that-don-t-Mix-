using UnityEngine;
using System.Collections;

public class HighScoreManager : MonoBehaviour
{

    private AzureStorageConsole.TableHelper m_TableHelper;

	void Start ()
    {
        m_TableHelper = new AzureStorageConsole.TableHelper();
        m_TableHelper.CreateTable("highscore");
	}
	
	void Update ()
    {
	
	}
}
