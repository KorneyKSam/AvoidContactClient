using Common;
using Common.Animation;
using System.Collections.Generic;
using System.Linq;

namespace NPCs.Sporozoa
{
    public class SporozoaInFlaskBehaviour
    {
        private AnimatedSprite m_AnimatedSporozoaInFlask;
        private SporozoaInFlaskState m_CurrentState;

        private readonly Dictionary<SporozoaInFlaskState, List<string>> m_States = new()
        {
            { SporozoaInFlaskState.Calm, new List<string>() { "Calm1", "Calm2", "Calm3" } },
            { SporozoaInFlaskState.Angry, new List<string>() { "FlaskHit1", "FlaskHit2", "FlaskHit3" } },
            { SporozoaInFlaskState.Madness, new List<string>() { "Madness1" } },
            { SporozoaInFlaskState.Curiosity, new List<string>() { "FlaskTouch1", "FlaskTouch2", "Investigate1" } },
        };

        public SporozoaInFlaskBehaviour(AnimatedSprite animatedSporozoa)
        {
            m_AnimatedSporozoaInFlask = animatedSporozoa;
        }

        public void SetState(SporozoaInFlaskState state)
        {
            if (m_CurrentState != state)
            {
                m_CurrentState = state;
                SwitchAnimation();
            }
        }

        public void StopBehaviour()
        {
            m_AnimatedSporozoaInFlask.PlayAnimation(m_States[SporozoaInFlaskState.Calm].First());
        }

        private void SwitchAnimation()
        {
            m_AnimatedSporozoaInFlask.PlayAnimation(GetRandomAnimationName(m_CurrentState), SwitchAnimation);
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