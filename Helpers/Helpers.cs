using System;
using System.IO;
using System.Text;

class Helpers{
    public static int NumeroLinhasFicheiro(string filePath){
        using (StreamReader streamReader = new StreamReader(filePath)){
            int i = 0;
            while (streamReader.ReadLine() != null){ i++; }
            streamReader.Close();
            return i;
        }
    }

    public static void PremirQualquerTeclaParaContinuar(){
        Console.WriteLine("Premir qualquer tecla para continuar.");
        Console.ReadLine();
    }

    public static void DrawMainMenu(){
        int escolhaMenu = -1;
        bool sairMenu = false;

        do{
            Console.Clear();

            Console.WriteLine("1- Listar Pedidos");
            Console.WriteLine("2- Listar Mobilidades");
            Console.WriteLine("3- Listar Utilizadores");
            Console.WriteLine("4- Inserir Mobilidade Elétrica;");
            Console.WriteLine("5- Remover Mobilidade Elétrica;");
            Console.WriteLine("6- Inserir Pedido de Utilização;");
            Console.WriteLine("7- Remover Pedido de Utilização;");
            Console.WriteLine("8- Custo Associado a um Pedido;");
            Console.WriteLine("9- Listagem do plano de Utilização");
            Console.WriteLine("0- Sair.");

            escolhaMenu = int.Parse(Console.ReadLine());
            switch(escolhaMenu){
                case 1:
                    PedidosHelper.ImprimirPedidos();
                    break;
                case 2:
                    MobilidadeUrbanaHelpers.ImprimirMobilidade();
                    break;
                case 3:
                    UtilizadorHelpers.ImprimirUtilizadores();
                    break;
                case 4:
                    MobilidadeUrbanaHelpers.InserirMobilidade();
                    break;
                case 5:
                    MobilidadeUrbanaHelpers.RemoverMobilidade();
                    break;
                case 6:
                    PedidosHelper.PedidoUtilizacao();
                    break;
                case 7:
                    PedidosHelper.RemoverUtilizacao();
                    break;
                case 8:
                    PedidosHelper.CalculoAssociadoAoPedido();
                    break;
                case 9:
                    PedidosHelper.UtilizacaoDaPedidoSelecionada();
                    break;
                case 0:
                    Console.WriteLine("Obrigado por utilizar o nosso programa.");
                    sairMenu = true;
                    break;
                default:
                    Console.WriteLine("Por favor, inserir um número correto.");
                    PremirQualquerTeclaParaContinuar();
                    break;
            }
        }while(sairMenu == false);
    }
}