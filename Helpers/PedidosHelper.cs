using System;
using System.IO;
using System.Text;

class PedidosHelper{
    private static Pedidos[] ReadFilePedidos(){
        Pedidos[] todosPedidos = new Pedidos[20];
        string fileString;
        string[] stringToWords;
        int i = 0;
        try{
            StreamReader streamReader = new StreamReader(Constants.FileDirectoryPedidos);
            fileString = streamReader.ReadLine();
            do{
                if(fileString != null){
                    stringToWords = fileString.Split(' ');
                    todosPedidos[i] = new Pedidos(int.Parse(stringToWords[0]), 
                                                int.Parse(stringToWords[1]), 
                                                stringToWords[2], 
                                                int.Parse(stringToWords[3]),
                                                int.Parse(stringToWords[4]));
                    fileString = streamReader.ReadLine();
                    i++;
                }
            }while(fileString != null);
            streamReader.Close();
        } catch(Exception ex){
            Console.WriteLine("Error: "+ ex.Message);
        }
        
        return todosPedidos;
    }

    private static void InserirPedidoNoFicheiro(int nif, int codigo, int tempo, int distancia, string filePath){
        int linhasFicheiro = Helpers.NumeroLinhasFicheiro(filePath) + 1;

        using(StreamWriter streamWriter = new StreamWriter(filePath, true)){
            streamWriter.WriteLine(linhasFicheiro.ToString() + " " 
                                    + nif.ToString() + " M_" 
                                    + codigo.ToString() + " " 
                                    + tempo.ToString() + " " 
                                    + distancia.ToString());
            streamWriter.Close();
        }
    }

    private static bool VerificaSeTemOPedido(string filePath, int codigo){
        string fileString;
        string[] stringToWords;
        bool confirmacao = false;
        try{
            StreamReader streamReader = new StreamReader(Constants.FileDirectoryPedidos);
            fileString = streamReader.ReadLine();
            do{
                if(fileString != null){
                    stringToWords = fileString.Split(' ');
                    if(stringToWords[0].Equals(codigo)) confirmacao = true;
                    fileString = streamReader.ReadLine();
                }
            }while(fileString != null);
            streamReader.Close();
        } catch(Exception ex){
            Console.WriteLine("Error: "+ ex.Message);
        }
        return confirmacao;
    }

    private static bool RemoverNoFicheiroUtilizacao(string filePath, int codigo){
        string linha = "";
        bool confirmacao = false;
        string[] linhas = File.ReadAllLines(filePath);
        File.Delete(filePath);
        using (StreamWriter streamWriter = new StreamWriter(filePath, true)){
            if(linha != null){
                foreach(string linhaString in linhas){
                    if(linhaString.Substring(0, 1).Equals(codigo.ToString())){
                        confirmacao = true;
                        continue;
                    } else streamWriter.WriteLine(linhaString);
                }
            }
            streamWriter.Close();
        }

        return confirmacao;
    }

    private static float EfetuarCalculo(string filePath, int codigo, int mobilidade){
        float conta = 0;
        string comparacao = "M_" + mobilidade;
        Pedidos[] todosPedidos = ReadFilePedidos();
        MobilidadeUrbana[] todasMobilidades = MobilidadeUrbanaHelpers.ReadFileMobilidade();
        foreach(MobilidadeUrbana mobilidadeSelecionada in todasMobilidades){
            if(mobilidadeSelecionada != null){
                if(mobilidadeSelecionada.codigo == comparacao){
                    conta = todosPedidos[codigo].tempo * mobilidadeSelecionada.custo;
                } 
            }
        }
        return conta;
    }

    private static void DrawTableOfPedidos(Pedidos[] todosPedidos){
        Console.WriteLine("_________________________________________________________________________");
        Console.Write("|N??mero Ordem\t|NIF\t\t|C??digo\t|Tempo(min)\t|Dist??ncia(km)\t|\n");
        foreach(Pedidos pedidos in todosPedidos){
            if(pedidos != null) Console.WriteLine("|" + pedidos.nOrdem + "\t\t|" + pedidos.nif + "\t|"
                                                    +pedidos.codigo + "\t|" + pedidos.tempo + "\t\t|"
                                                    +pedidos.distancia + "\t\t|");
        }
        Console.WriteLine("|_______________________________________________________________________|");
        Helpers.PremirQualquerTeclaParaContinuar();
    }

    private static void ListarPedidoPretendida(int codigo){
        Pedidos[] todosPedidos = ReadFilePedidos();
        Console.WriteLine("\n\n_________________________________________________________________________");
        Console.Write("|N??mero Ordem\t|NIF\t\t|C??digo\t|Tempo(min)\t|Dist??ncia(km)\t|\n");

        foreach(Pedidos pedidos in todosPedidos){
            if(pedidos != null){
                if(pedidos.codigo == "M_" + codigo){
                    Console.WriteLine("|" + pedidos.nOrdem + "\t\t|" + pedidos.nif + "\t|"
                                                    +pedidos.codigo + "\t|" + pedidos.tempo + "\t\t|"
                                                    +pedidos.distancia + "\t\t|");
                }
            }
        }
        Console.WriteLine("|_______________________________________________________________________|");
        Helpers.PremirQualquerTeclaParaContinuar();
    }

    public static void UtilizacaoDaPedidoSelecionada(){
        int codigo;
        while(true){
            Console.Write("Qual ?? o c??digo da mobilidade que deseja ver? M_");
            codigo = int.Parse(Console.ReadLine());

            if(codigo > 0 && MobilidadeUrbanaHelpers.VerificarSeTemCodigoMobilidade(codigo, Constants.FileDirectoryMobilidade)) break;
            Console.WriteLine("Inseriu o c??digo errado.");
        }

        ListarPedidoPretendida(codigo);
    }

    public static void CalculoAssociadoAoPedido(){
        int codigoPedidos, linhasPedidos = 0, codigoMobilidade, linhasMobilidade = 0;

        while(true){
            Console.Write("Qual o c??digo do pedido que deseja calcular? ");
            codigoPedidos = int.Parse(Console.ReadLine());
            linhasPedidos = Helpers.NumeroLinhasFicheiro(Constants.FileDirectoryPedidos);

            Console.Write("Qual o c??digo da mobilidade que deseja calcular? M_");
            codigoMobilidade = int.Parse(Console.ReadLine());
            linhasMobilidade = Helpers.NumeroLinhasFicheiro(Constants.FileDirectoryMobilidade);

            if(codigoPedidos > 0 && codigoPedidos <= linhasPedidos 
            && codigoMobilidade > 0 && codigoMobilidade <= linhasMobilidade) break;
            Console.WriteLine("Insira um n??mero v??lido!");
        }

        Console.WriteLine("O valor que ir?? gastar ??: " + EfetuarCalculo(Constants.FileDirectoryPedidos, codigoPedidos, codigoMobilidade));
        Helpers.PremirQualquerTeclaParaContinuar();
    }

    public static void ImprimirPedidos(){
        Pedidos[] todosPedidos = new Pedidos[20];
        todosPedidos = ReadFilePedidos();
        
        Console.WriteLine("PEDIDOS");
        DrawTableOfPedidos(todosPedidos);
    }

    public static void PedidoUtilizacao(){
        int nif = 0, codigo = 0, tempo = 0, distancia = 0;
        bool verificarSeExisteCodigo = false;

        while(true){
            Console.Write("Insira o seu NIF: ");
            nif = int.Parse(Console.ReadLine());
            if(nif > 0 && nif < 1000000000 && UtilizadorHelpers.ConfirmacaoSeHaUtilizador(nif)) break;
            Console.WriteLine("Insira um NIF v??lido!");
        }

        while(true){
            Console.Write("Insira o c??digo: M_");
            codigo = int.Parse(Console.ReadLine());
            verificarSeExisteCodigo = MobilidadeUrbanaHelpers.VerificarSeTemCodigoMobilidade(codigo, Constants.FileDirectoryMobilidade);
            if(codigo > 0 && verificarSeExisteCodigo) break;
            Console.WriteLine("Insira um c??digo v??lido!");
        }

        while(true){
            Console.Write("Insira o tempo: ");
            tempo = int.Parse(Console.ReadLine());

            Console.Write("Insira a distancia: ");
            distancia = int.Parse(Console.ReadLine());

            if(distancia > 0 && tempo > 0 && MobilidadeUrbanaHelpers.VerificarSeEstaDisponivelAMobilidade(codigo, tempo, distancia)) break;
            
            if(distancia < 0 || tempo < 0) Console.WriteLine("Por favor verifique um dos campos que nao foi corretamente preenchido");
            else{
                Console.Write("\nA mobilidade que deseja nao tem autonomia suficiente para este tempo e distancia."
                                + "Talvez queira usar outra mobilidade, tal como: ");
                MobilidadeUrbanaHelpers.AconselharOutraMobilidade("M_" + codigo);
                Helpers.PremirQualquerTeclaParaContinuar();
            }
        }

        InserirPedidoNoFicheiro(nif, codigo, tempo, distancia, Constants.FileDirectoryPedidos);
    }

    public static void RemoverUtilizacao(){
        int codigo;

        while(true){
            Console.Write("Qual o c??digo do pedido que deseja apagar? ");
            codigo = int.Parse(Console.ReadLine());
            if(codigo > 0 && RemoverNoFicheiroUtilizacao(Constants.FileDirectoryPedidos, codigo)){
                Console.WriteLine("Pedido removido com sucesso.");
                Helpers.PremirQualquerTeclaParaContinuar();
                break;
            } else Console.WriteLine("Inseriu o c??digo errado.");
        }
    }
}