using Deck;
using Managers;

namespace GameMode
{
    public class GMPhaseInit : GMPhaseBase
    {
        public override void Init()
        {
            base.Init();
            
            LevelManager.Instance.InitReference();
            
        }


        public override void OnUpdate()
        {
            base.OnUpdate();
        }
    }
}


