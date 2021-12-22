using System.Collections.Generic;

namespace Series.Interfaces
{
    public interface IRepositorio<T>
    {
         List<T> Lista();

         public T RetornarPorId(int id);

         public void Insere(T objeto);

         public void Excluir(int id);

         public void Atualizar(int id, T objeto);

         public int ProximoId();
    }
}