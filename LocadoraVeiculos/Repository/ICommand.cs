namespace LocadoraVeiculos.Repository
{
    public interface ICommand<T>
    {
        void Incluir(T obj);
        void Excluir(T obj);
        void Atualizar(T obj);
    }
}
