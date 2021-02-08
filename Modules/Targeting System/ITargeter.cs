namespace TargetingSystem
{
    public interface ITargeter
    {
        TargetInfo GetTargetInfo();

        bool SetTarget(ITargetable target);

        void ClearTargets();
    }
}
