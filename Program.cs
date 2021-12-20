using System;
using System.IO;

public class Program{
    //Pergunta um
    static Pedidos[] readFilePedidos(){
        Pedidos[] todosPedidos = new Pedidos[20];
        string fileString;
        string[] stringToWords;
        int i = 0;
        try{
            StreamReader streamReader = new StreamReader(Constants.FileDirectoryMDM);
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

    //Pergunta Um
    static void drawTableOfPedidos(Pedidos[] todosPedidos){
        Console.WriteLine("_________________________________________________________________________");
        Console.Write("|Número Ordem\t|NIF\t\t|Código\t|Tempo(min)\t|Distância(km)\t|\n");
        foreach(Pedidos pedidos in todosPedidos){
            if(pedidos != null) Console.WriteLine("|" + pedidos.nOrdem + "\t\t|" + pedidos.nif + "\t|"
                                                    +pedidos.codigo + "\t|" + pedidos.tempo + "\t\t|"
                                                    +pedidos.distancia + "\t\t|");
        }
        Console.WriteLine("|_______________________________________________________________________|");
    }

    //Pergunta Um
    static void ImprimirPedidos(){
        Pedidos[] todosPedidos = new Pedidos[20];
        todosPedidos = readFilePedidos();

        drawTableOfPedidos(todosPedidos);
    }

    //Pergunta Dois
    static MobilidadeUrbana[] readFileMobilidade(){
        MobilidadeUrbana[] tiposMobilidade = new MobilidadeUrbana[20];
        string fileString;
        string[] stringToWords;
        int i = 0;
        try{
            StreamReader streamReader = new StreamReader(Constants.FileDirectoryMU);
            fileString = streamReader.ReadLine();
            do{
                if(fileString != null){
                    stringToWords = fileString.Split(' ');
                    tiposMobilidade[i] = new MobilidadeUrbana(stringToWords[0],
                                                                stringToWords[1],
                                                                float.Parse(stringToWords[2]),
                                                                int.Parse(stringToWords[3]));
                    fileString = streamReader.ReadLine();
                    i++;
                }
            }while(fileString != null);
            streamReader.Close();
        } catch(Exception ex){
            Console.WriteLine("Error: "+ ex.Message);
        }
        
        return tiposMobilidade;
    }

    //Pergunta Dois
    static void drawTableOfMobilidade(MobilidadeUrbana[] tiposMobilidade){
        Console.WriteLine("_________________________________________________________________");
        Console.Write("|Código\t|Tipo\t\t\t|Custo\t\t|Autonomia\t|\n");
        foreach(MobilidadeUrbana mobilidade in tiposMobilidade){
            if(mobilidade != null) {
                if(mobilidade.tipo.Length < 7) Console.WriteLine("|" + mobilidade.codigo + "\t|" + mobilidade.tipo + "\t\t\t|" 
                                                    + mobilidade.custo + "\t\t|" + mobilidade.autonomia + "\t\t|");
                else Console.WriteLine("|" + mobilidade.codigo + "\t|" + mobilidade.tipo + "\t\t|" 
                                                    + mobilidade.custo + "\t\t|" + mobilidade.autonomia + "\t\t|");
            }
        }
        Console.WriteLine("|_______________________________________________________________|");
    }

    //Pergunta Dois
    static void ImprimirMobilidade(){
        MobilidadeUrbana[] tiposMobilidade = new MobilidadeUrbana[20];
        tiposMobilidade = readFileMobilidade();
    
        drawTableOfMobilidade(tiposMobilidade);
    }

    static void Main(){
        int escolhaMenu = -1;
        bool sairMenu = false;

        do{
            Console.Clear();
            ImprimirPedidos();
            ImprimirMobilidade();
            Console.Write("\n\n");

            Console.WriteLine("1- Inserir Mobilidade Elétrica;");
            Console.WriteLine("2- Remover Mobilidade Elétrica;");
            Console.WriteLine("0- Sair.");

            escolhaMenu = int.Parse(Console.ReadLine());
            switch(escolhaMenu){
                case 1:
                    break;
                case 2:
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
