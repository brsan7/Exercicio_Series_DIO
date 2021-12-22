using System;
using Series.Classes;
using Series.Enum;

namespace Series
{ 
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while(opcaoUsuario.ToUpper() != "X")
            {
                switch(opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    case "-1":
                        Console.WriteLine("Digite um valor válido");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.WriteLine("Programa finalizado");
        }

        private static void ListarSeries()
        {
            Console.WriteLine("Listar séries");

            var lista = repositorio.Lista();

            if(lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada");
                return;
            }

            foreach(var serie in lista)
            {
                var excluido = serie.retornaExcluido();

                Console.WriteLine("#ID {0}: {1} {2}",serie.retornaId(),serie.retornaTitulo(),(excluido ? "*Excluído*":""));
            }
        }

        private static void InserirSerie()
        {
            Console.WriteLine("Inserir nova série");
            
            foreach(int i in System.Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, System.Enum.GetName(typeof(Genero), i));
            }

            Console.Write("Digite o genêro entre as opções acima: ");
            string verificacaoGenero = verificarEntrada(Console.ReadLine(),"numeros");
            if(verificacaoGenero.Equals("-1")) verificacaoGenero = "14";
            int entradaGenero = int.Parse(verificacaoGenero);

            Console.Write("Digite o título da série: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("Digite a descrição da série: ");
            string entradaDescricao = Console.ReadLine();

            Console.Write("Digite o ano de início da série: ");
            string verificacaoAno = verificarEntrada(Console.ReadLine(),"numeros");
            if(verificacaoAno.Equals("-1")) verificacaoAno = "0";
            int entradaAno = int.Parse(verificacaoAno);

            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        descricao: entradaDescricao,
                                        ano: entradaAno);
            
            repositorio.Insere(novaSerie);
        }

        private static void AtualizarSerie()
        {
            Console.Write("Digite o id d série: ");
            string idEntrada = verificarEntrada(Console.ReadLine(),"numeros");
            if(!idEntrada.Equals("-1"))
            {
                int indiceSerie = int.Parse(idEntrada);
            
            
                foreach(int i in System.Enum.GetValues(typeof(Genero)))
                    {
                    Console.WriteLine("{0}-{1}", i, System.Enum.GetName(typeof(Genero), i));
                }

                Console.Write("Digite o genêro entre as opções acima: ");
                string verificacaoGenero = verificarEntrada(Console.ReadLine(),"numeros");
                if(verificacaoGenero.Equals("-1")) verificacaoGenero = "14";
                int entradaGenero = int.Parse(verificacaoGenero);

                Console.Write("Digite o título da série: ");
                string entradaTitulo = Console.ReadLine();

                Console.Write("Digite a descrição da série: ");
                string entradaDescricao = Console.ReadLine();

                Console.Write("Digite o ano de início da série: ");
                string verificacaoAno = verificarEntrada(Console.ReadLine(),"numeros");
                if(verificacaoAno.Equals("-1")) verificacaoAno = "0";
                int entradaAno = int.Parse(verificacaoAno);

                Serie atualizarSerie = new Serie(id: indiceSerie,
                                                genero: (Genero)entradaGenero,
                                                titulo: entradaTitulo,
                                                descricao: entradaDescricao,
                                                ano: entradaAno);
            
                repositorio.Atualizar(indiceSerie, atualizarSerie);
                }
                else Console.Write("Id inválido");
        }

        private static void ExcluirSerie()
        {
            Console.Write("Digite o id da série a ser exluída: ");
            string idEntrada = verificarEntrada(Console.ReadLine(),"numeros");
            if(!idEntrada.Equals("-1"))
            {
                int indiceSerie = int.Parse(idEntrada);
                var serie = repositorio.RetornarPorId(indiceSerie);

                Console.WriteLine("#ID {0}: {1}",indiceSerie,serie.retornaTitulo());
                Console.WriteLine("Deseja mesmo excluír?\n[S] - Sim\n[N] - Não");

                var exluir = verificarEntrada(Console.ReadLine().ToUpper(),"exclusao");

                if(exluir == "S")
                {
                    repositorio.Excluir(indiceSerie);
                    Console.WriteLine("***Exclusão realizada com sucesso***");
                }
                else Console.WriteLine("***Exclusão não realizada***");
            }
            else Console.Write("Valor inválido ***Exclusão não realizada***");
        }

        private static void VisualizarSerie()
        {
            Console.Write("Digite o id da série: ");
            string idEntrada = verificarEntrada(Console.ReadLine(),"numeros");
            if(!idEntrada.Equals("-1"))
            {
                int indiceSerie = int.Parse(idEntrada);

                var serie = repositorio.RetornarPorId(indiceSerie);

                Console.WriteLine(serie);
            }
            else Console.WriteLine("Id inválido");
        }
        private static string ObterOpcaoUsuario()
            {
                Console.WriteLine();
                Console.WriteLine("****Exercicio Series****");
                Console.WriteLine("1 - Listar séries");
                Console.WriteLine("2 - Inserir nova série");
                Console.WriteLine("3 - Atualizar série");
                Console.WriteLine("4 - Excluir série");
                Console.WriteLine("5 - Visualizar série");
                Console.WriteLine("C - Limpar tela");
                Console.WriteLine("X - Sair");
                Console.WriteLine();

                string opcaoUsuario = verificarEntrada(Console.ReadLine().ToUpper(),"menu");
                Console.WriteLine();
                return opcaoUsuario;
            }
        private static string verificarEntrada(string entrada, string tipoVerificacao)
        {
            string[] menu = new string[]{"1","2","3","4","5","C","X"};
            string[] exclusao = new string[]{"S","N"};
            
            if(entrada == "")return "-1";
            else
            {
                switch(tipoVerificacao)
                {
                    case "menu":
                        if(Array.Exists(menu,elemento => elemento == entrada)) return entrada;
                        else return "-1";

                    case "genero":
                        if(int.TryParse(entrada, out int intEntrada1))
                        {
                            if(intEntrada1 > 0 && intEntrada1 < 14) return entrada;
                            else return "-1";
                        }
                        else return "-1";

                    case "numeros":
                        if(int.TryParse(entrada, out int intEntrada2))
                        {
                            if(intEntrada2 >= 0) return entrada;
                            else return "-1";
                        }
                        else return "-1";

                    case "exclusao":
                        if(Array.Exists(exclusao,elemento => elemento == entrada)) return entrada;
                        else return "-1";
                        
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
