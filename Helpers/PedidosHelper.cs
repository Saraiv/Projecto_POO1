using System;
using System.IO;
using System.Text;

class PedidosHelper{
    //Pergunta um
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

    private static int EfetuarCalculo(string filePath, int codigo){
        return 1;
    }

    public static void CalculoAssociadoAoPedido(){
        int codigo, linhas = 0;

        while(true){
            Console.Write("Qual o código do pedido que deseja calcular? ");
            codigo = int.Parse(Console.ReadLine());
            linhas = Helpers.NumeroLinhasFicheiro(Constants.FileDirectoryPedidos);

            if(codigo > 0 && codigo <= linhas) break;
            Console.WriteLine("Insira um número válido!");
        }

        Console.WriteLine("O valor que irá gastar é: " + EfetuarCalculo(Constants.FileDirectoryPedidos, codigo));
    }

    public static void DrawTableOfPedidos(Pedidos[] todosPedidos){
        Console.WriteLine("_________________________________________________________________________");
        Console.Write("|Número Ordem\t|NIF\t\t|Código\t|Tempo(min)\t|Distância(km)\t|\n");
        foreach(Pedidos pedidos in todosPedidos){
            if(pedidos != null) Console.WriteLine("|" + pedidos.nOrdem + "\t\t|" + pedidos.nif + "\t|"
                                                    +pedidos.codigo + "\t|" + pedidos.tempo + "\t\t|"
                                                    +pedidos.distancia + "\t\t|");
        }
        Console.WriteLine("|_______________________________________________________________________|");
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
            if(nif > 0 && nif < 1000000000) break;
            Console.WriteLine("Insira um NIF válido!");
        }

        while(true){
            Console.Write("Insira o código: M_");
            codigo = int.Parse(Console.ReadLine());
            verificarSeExisteCodigo = MobilidadeUrbanaHelpers.VerificarSeTemCodigoMobilidade(codigo, Constants.FileDirectoryMobilidade);
            if(codigo > 0 && verificarSeExisteCodigo) break;
            Console.WriteLine("Insira um código válido!");
        }

        while(true){
            Console.Write("Insira o tempo: ");
            tempo = int.Parse(Console.ReadLine());
            if(tempo > 0) break;
            Console.WriteLine("Insira um tempo válido!");
        }

        while(true){
            Console.Write("Insira a distancia: ");
            distancia = int.Parse(Console.ReadLine());
            if(distancia > 0) break;
            Console.WriteLine("Insira uma distancia válido!");
        }

        InserirPedidoNoFicheiro(nif, codigo, tempo, distancia, Constants.FileDirectoryPedidos);
    }

    public static void RemoverUtilizacao(){
        int codigo;

        while(true){
            Console.Write("Qual o código do pedido que deseja apagar? ");
            codigo = int.Parse(Console.ReadLine());
            if(codigo > 0 && RemoverNoFicheiroUtilizacao(Constants.FileDirectoryPedidos, codigo)){
                Console.WriteLine("Pedido removido com sucesso.");
                Console.ReadLine();
                break;
            } else Console.WriteLine("Inseriu o código errado.");
        }
    }
}