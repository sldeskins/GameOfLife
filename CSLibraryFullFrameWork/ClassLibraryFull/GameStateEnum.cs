using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_GOL_LibraryFull
{
    public enum GameStateEnum
    {
        NewGame,
        Continuing,
        SteadyState_AllDead,
        SteadyState_StaticAlive,
        SteadyState_Blinker, 
        Special_Repeater,
        Special_Glider_Gun, 
    }
}
