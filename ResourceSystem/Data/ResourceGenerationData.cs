using System;

namespace ResourcesSystem.Data
{
    [Serializable]
    public class ResourceGenerationData
    {
        public GameResource resourceToGenerate;

        public int maxEfficiencyNodesAmount;
        public int maxStorageCapacity;
        public int extractionAmount;

        public float generationTime;
        public float resourceDetectionRadius;
    }
}