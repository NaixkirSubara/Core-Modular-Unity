namespace MyStudio.Core.Optimization
{
    public interface IPooledObject
    {
        // Pengganti 'Start()'. Dipanggil setiap kali objek diambil dari pool.
        void OnObjectSpawn();
    }
}