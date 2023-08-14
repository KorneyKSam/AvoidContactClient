using Common;
using Common.Animation;
using Decorations;
using System.Collections.Generic;
using System.Linq;

namespace NPCs.Sporozoa
{
    public class SporozoaInFlaskBehaviour
    {
        private AnimatedSprite m_AnimatedSporozoaInFlask;
        private TableLamp m_TableLamp;
        private SporozoaInFlaskState m_CurrentState;

        private readonly Dictionary<SporozoaInFlaskState, List<string>> m_States = new()
        {
            { SporozoaInFlaskState.None, new List<string>() { "Idle",} },
            { SporozoaInFlaskState.Calm, new List<string>() { "Calm1", "Calm2", "Calm3" } },
            { SporozoaInFlaskState.Angry, new List<string>() { "Angry1", "Angry2", "Angry3" } },
            { SporozoaInFlaskState.Madness, new List<string>() { "Madness1" } },
            { SporozoaInFlaskState.Curiosity, new List<string>() { "FlaskTouch1", "FlaskTouch2", "Investigate1" } },
        };

        public SporozoaInFlaskBehaviour(AnimatedSprite animatedSporozoa, TableLamp tableLamp)
        {
            m_AnimatedSporozoaInFlask = animatedSporozoa;
            m_TableLamp = tableLamp;
            m_TableLamp.OnLightSwitch += SwitchState;
        }

        ~SporozoaInFlaskBehaviour()
        {
            m_TableLamp.OnLightSwitch -= SwitchState;
        }

        private void SwitchState()
        {
            SporozoaInFlaskState switchedState;

            if (m_TableLamp.IsLightOn)
            {
                switchedState = SporozoaInFlaskState.Angry;
            }
            else
            {
                switchedState = SporozoaInFlaskState.Calm;
            }

            if (m_CurrentState != switchedState)
            {
                m_CurrentState = switchedState;

                if (switchedState == SporozoaInFlaskState.None)
                {
                    RemoveAnimationListener();
                    StopBehaviour();
                }
                else
                {
                    AddAnimationListener();
                    SwitchToRandomAnimation();
                }
            }
        }

        private void SwitchToRandomAnimation()
        {
            m_AnimatedSporozoaInFlask.PlayAnimation(GetRandomAnimationName(m_CurrentState));
        }

        public void StopBehaviour()
        {
            m_AnimatedSporozoaInFlask.PlayAnimation(m_States[SporozoaInFlaskState.Calm].First());
        }

        private void RemoveAnimationListener()
        {
            m_AnimatedSporozoaInFlask.OnAnimationComplete -= SwitchToRandomAnimation;
        }

        private void AddAnimationListener()
        {
            RemoveAnimationListener();
            m_AnimatedSporozoaInFlask.OnAnimationComplete += SwitchToRandomAnimation;
        }

        private string GetRandomAnimationName(SporozoaInFlaskState state)
        {
            var moodSettings = m_States[state];
            return moodSettings[GetRandom(0, m_States[state].Count)];
        }

        private int GetRandom(int minValue, int maxValue)
        {
            return RandomProvider.GetThreadRandom().Next(minValue, maxValue);
        }
    }
}