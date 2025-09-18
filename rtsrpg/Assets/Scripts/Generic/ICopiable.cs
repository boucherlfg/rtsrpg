using States;

namespace Generic
{
    public interface ICopiable<out T>
    {
        T Copy();
    }
}