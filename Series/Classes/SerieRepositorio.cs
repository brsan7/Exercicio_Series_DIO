using System.Collections.Generic;
using Series.Interfaces;

namespace Series.Classes
{
    public class SerieRepositorio : IRepositorio<Serie>
    {
        private List<Serie> listaserie = new List<Serie>();
        public void Atualizar(int id, Serie objeto)
        {
            listaserie[id] = objeto;
        }

        public void Excluir(int id)
        {
            listaserie[id].Excluir();
        }

        public void Insere(Serie objeto)
        {
            listaserie.Add(objeto);
        }

        public List<Serie> Lista()
        {
            return listaserie;
        }

        public int ProximoId()
        {
            return listaserie.Count;
        }

        public Serie RetornarPorId(int id)
        {
            return listaserie[id];
        }
    }
}