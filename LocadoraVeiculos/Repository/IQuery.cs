namespace LocadoraVeiculos.Repository
{
    public interface IQuery<T>
    {
        T BuscarPorId(string id);
    }
}
