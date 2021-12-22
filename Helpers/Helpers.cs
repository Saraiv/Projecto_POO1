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

    public static void DrawMainMenu(){
        int escolhaMenu = -1;
        bool sairMenu = false;

        do{
            Console.Clear();
            PedidosHelper.ImprimirPedidos();
            MobilidadeUrbanaHelpers.ImprimirMobilidade();
            Console.Write("\n\n");

            Console.WriteLine("1- Inserir Mobilidade Elétrica;");
            Console.WriteLine("2- Remover Mobilidade Elétrica;");
            Console.WriteLine("3- Inserir Pedido de Utilização;");
            Console.WriteLine("4- Remover Pedido de Utilização;");
            Console.WriteLine("5- Custo Associado a um Pedido;");
            Console.WriteLine("0- Sair.");

            escolhaMenu = int.Parse(Console.ReadLine());
            switch(escolhaMenu){
                case 1:
                    MobilidadeUrbanaHelpers.InserirMobilidade();
                    break;
                case 2:
                    MobilidadeUrbanaHelpers.RemoverMobilidade();
                    break;
                case 3:
                    PedidosHelper.PedidoUtilizacao();
                    break;
                case 4:
                    PedidosHelper.RemoverUtilizacao();
                    break;
                case 5:
                    PedidosHelper.CalculoAssociadoAoPedido();
                    break;
                case 0:
                    sairMenu = true;
                    break;
                default:
                    Console.WriteLine("Por favor, inserir um número correto.");
                    Console.ReadLine();
                    break;
            }
        }while(sairMenu == false);
    }
}