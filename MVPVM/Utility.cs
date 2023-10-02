using Prism.Regions;

namespace MVPVM
{
    public static class Utility
    {
        public static void RemoveView(IRegionManager regionManager, string regionName, object view)
        {
            regionManager.Regions[regionName].Remove(view);
        }

        public static void ShowView(IRegionManager regionManager, string regionName, object view)
        {
            regionManager.Regions[regionName].RemoveAll();
            regionManager.AddToRegion(regionName, view);
        }
    }
}
