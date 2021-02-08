using BuildingManagmentSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagmentSystem.Events
{
    public static class EventManager
    {
        public static event Action<BuildingData> OnBuildingSelection;

        public static void RaiseBuildingSelectionEvent(BuildingData data)
        {
            OnBuildingSelection?.Invoke(data);
        }
    }
}
