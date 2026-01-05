namespace MyStudio.Core.Architecture
{
    public interface ICommand
    {
        void Execute(); // Lakukan perintah
        void Undo();    // Batalkan perintah
    }
}
