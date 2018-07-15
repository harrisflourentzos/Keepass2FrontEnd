using Keepass2.Model;

namespace Keepass2.Storage
{
    public interface IRepository
    {
        void Save(Safe safe);
        Safe Load(string location);
    }
}
