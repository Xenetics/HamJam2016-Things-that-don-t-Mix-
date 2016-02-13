using UnityEngine;
using System.Collections;

public class StateIntro : GameState 
{
	private float countDown = 6.5f;
    private bool m_ShieldPlay = true;
	public StateIntro(GameManager manager):base(manager){ }
	
	public override void OnStateEntered()
	{
		Application.LoadLevel("Intro");
        SoundManager.Instance.PlaySound( SoundManager.SoundType.SFX , "guitarIntro");
	}
	public override void OnStateExit(){}
	public override void StateUpdate() 
	{
		if(countDown<=0)
		{
			gameManager.NewGameState(gameManager.stateMainMenu);
		}
		else
		{
			countDown -= Time.deltaTime;	
		}

        if(countDown < 2.5f)
        {
            if (m_ShieldPlay)
            {
                SoundManager.Instance.PlaySound(SoundManager.SoundType.SFX, "shieldIntro");
                m_ShieldPlay = false;
            }
        }
	}
	public override void StateGUI() 
	{
		GUILayout.Label("state: State Intro  -  "+countDown);
	}
}
