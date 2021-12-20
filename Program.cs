using System;
using System.IO;

public class Program{
    static Pedidos[] readFile(){
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

    static void drawTableOfPedidos(Pedidos[] todosPedidos){
        Console.Write("Número Ordem\tNIF\t\tCódigo\tTempo(min)\tDistância(km)\n");
        foreach(Pedidos pedidos in todosPedidos){
            if(pedidos != null) Console.WriteLine(pedidos.nOrdem + "\t\t" + pedidos.nif + "\t"
                                                    +pedidos.codigo + "\t" + pedidos.tempo + "\t\t"
                                                    +pedidos.distancia + "\n");
        }
    }

    static void PerguntaUm(){
        Pedidos[] todosPedidos = new Pedidos[20];
        todosPedidos = readFile();

        drawTableOfPedidos(todosPedidos);
    }

    static void Main(){
        PerguntaUm();
    }
}
