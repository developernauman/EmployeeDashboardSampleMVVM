using System.Threading.Tasks;

namespace Sample.Common
{
    public interface IDataProvider<T> where T : class
    {
        T Data { get; }
        Task Execute();
    }
}
