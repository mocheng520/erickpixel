namespace PixelRTS.Factory
{
    public interface ISpawner
    {
        void Spawn();
    }

    public interface IMultiSpawner : ISpawner
    {
        void Spawn(int index);
    }
}