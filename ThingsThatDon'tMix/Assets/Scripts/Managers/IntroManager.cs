using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public enum Intro { RoconnFadeIn, Horns, RoconnFadeOut, Griffiths }
    private Intro m_CurrentIntro = Intro.RoconnFadeIn;
    [SerializeField]
    private Image m_RoconnLogo;
    [SerializeField]
    private Image m_DevilHorns;
    [SerializeField]
    private float m_HornSpeed;
    [SerializeField]
    private Image m_GriffithsLogo;

    void Update()
    {
        HandleState();
    }

    private void SetState(Intro state)
    {
        ExitState();
        m_CurrentIntro = state;
        EnterState();
    }
    private void HandleState()
    {
        switch(m_CurrentIntro)
        {
            case Intro.RoconnFadeIn:
                Color color = m_RoconnLogo.color;
                color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), Time.deltaTime * 1);
                if((1 - color.a) < 0.01f)
                {
                    color = new Color(1, 1, 1, 1);
                    SetState(Intro.Horns);
                }
                m_RoconnLogo.color = color;
                break;
            case Intro.Horns:

                break;
            case Intro.RoconnFadeOut:

                break;
            case Intro.Griffiths:

                break;
        }
    }
    private void EnterState()
    {
        switch (m_CurrentIntro)
        {
            case Intro.RoconnFadeIn:

                break;
            case Intro.Horns:

                break;
            case Intro.RoconnFadeOut:

                break;
            case Intro.Griffiths:

                break;
        }
    }
    private void ExitState()
    {
        switch (m_CurrentIntro)
        {
            case Intro.RoconnFadeIn:

                break;
            case Intro.Horns:

                break;
            case Intro.RoconnFadeOut:

                break;
            case Intro.Griffiths:

                break;
        }
    }
}
