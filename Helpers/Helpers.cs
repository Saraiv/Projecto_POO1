using System;
using System.IO;
using System.Text;

class Helpers{

    //Pergunta um
    private static Pedidos[] readFilePedidos(){
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
    private static void drawTableOfPedidos(Pedidos[] todosPedidos){
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
    private static void ImprimirPedidos(){
        Pedidos[] todosPedidos = new Pedidos[20];
        todosPedidos = readFilePedidos();

        drawTableOfPedidos(todosPedidos);
    }

    //Pergunta Dois
    private static MobilidadeUrbana[] readFileMobilidade(){
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
    private static void drawTableOfMobilidade(MobilidadeUrbana[] tiposMobilidade){
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
    private static void ImprimirMobilidade(){
        MobilidadeUrbana[] tiposMobilidade = new MobilidadeUrbana[20];
        tiposMobilidade = readFileMobilidade();
    
        drawTableOfMobilidade(tiposMobilidade);
    }

    private static int NumeroLinhasFicheiro(string filePath){
        using (StreamReader streamReader = new StreamReader(filePath)){
            int i = 0;
            while (streamReader.ReadLine() != null){ i++; }
            streamReader.Close();
            return i;
        }
    }

    private static void InserirNoFicheiroMobilidade(string filePath, string tipoMobilidade, float custoMobilidade, int autonomiaMobilidade){
        int linhasFicheiro = NumeroLinhasFicheiro(filePath) + 1;

        // using(StreamWriter streamWriter = File.AppendText(filePath)){
        using(StreamWriter streamWriter = File.AppendText(filePath)){
            streamWriter.WriteLine("M_" + linhasFicheiro.ToString() + " "
                                    + tipoMobilidade + " "
                                    + custoMobilidade.ToString() + " "
                                    + autonomiaMobilidade.ToString());
            streamWriter.Close();
            
        }
    }

    private static void InserirMobilidade(){
        string tipoMobilidade = "";
        float custoMobilidade = 0f;
        int autonomiaMobilidade = 0;

        Console.Write("Tipo de mobilidade: ");
        tipoMobilidade = Console.ReadLine();

        while(true){
            Console.Write("Custo desta mobilidade: ");
            custoMobilidade = float.Parse(Console.ReadLine());
            if (custoMobilidade > 0) break;
            Console.WriteLine("Insira um custo de mobilidade válido!");
        }
        
        while(true){
            Console.Write("Autonomia desta mobilidade: ");
            autonomiaMobilidade = int.Parse(Console.ReadLine());
            if (autonomiaMobilidade > 0) break;
            Console.WriteLine("Insira uma autonomia de mobilidade válida!");
        }

        InserirNoFicheiroMobilidade(Constants.FileDirectoryMU, 
                                    tipoMobilidade, custoMobilidade,
                                    autonomiaMobilidade);

        Console.WriteLine("Mobilidade inserida com sucesso!");
        Console.ReadLine();
    }

    private static void RemoverNoFicheiroMobilidade(string filePath, int codigo){
        string linha = "";
        string[] linhas = File.ReadAllLines(filePath);
        File.Delete(filePath);
        using (StreamWriter streamWriter = File.AppendText(filePath)){
            if(linha != null){
                foreach(string linhaString in linhas){
                    if(linhaString.Contains("M_" + codigo)){
                        continue;
                    } else streamWriter.WriteLine(linhaString);
                }
            }
            streamWriter.Close();
        }

        Console.WriteLine("Mobilidade removida com sucesso.");
        Console.ReadLine();
    }
    
    private static void RemoverMobilidade(){
        int codigo;

        while(true){
            Console.Write("Qual dos códigos mobilidades deseja apagar? M_");
            codigo = int.Parse(Console.ReadLine());
            if(codigo > 0) break;
            Console.WriteLine("Insira um número válido!");
        }

        RemoverNoFicheiroMobilidade(Constants.FileDirectoryMU, codigo);
    }

    public static void drawMainMenu(){
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
                    InserirMobilidade();
                    break;
                case 2:
                    RemoverMobilidade();
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