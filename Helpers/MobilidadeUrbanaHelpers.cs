using System;
using System.IO;
using System.Text;

class MobilidadeUrbanaHelpers{
    private static MobilidadeUrbana[] ReadFileMobilidade(){
        MobilidadeUrbana[] tiposMobilidade = new MobilidadeUrbana[20];
        string fileString;
        string[] stringToWords;
        int i = 0;
        try{
            StreamReader streamReader = new StreamReader(Constants.FileDirectoryMobilidade);
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

    private static void InserirNoFicheiroMobilidade(string filePath, string tipoMobilidade, float custoMobilidade, int autonomiaMobilidade){
        int linhasFicheiro = Helpers.NumeroLinhasFicheiro(filePath) + 1;

        using(StreamWriter streamWriter = new StreamWriter(filePath, true)){
            streamWriter.WriteLine("M_" + linhasFicheiro.ToString() + " "
                                    + tipoMobilidade + " "
                                    + custoMobilidade.ToString() + " "
                                    + autonomiaMobilidade.ToString());
            streamWriter.Close();
        }
    }

    private static bool RemoverNoFicheiroMobilidade(string filePath, int codigo){
        string linha = "";
        bool confirmacao = false;
        string[] linhas = File.ReadAllLines(filePath);
        File.Delete(filePath);
        using (StreamWriter streamWriter = new StreamWriter(filePath, true)){
            if(linha != null){
                foreach(string linhaString in linhas){
                    if(linhaString.Contains("M_" + codigo)){
                        confirmacao = true;
                        continue;
                    } else streamWriter.WriteLine(linhaString);
                }
            }
            streamWriter.Close();
        }

        return confirmacao;
    }

    public static void AconselharOutraMobilidade(string codigo){
        MobilidadeUrbana[] mobilidadeUrbana = ReadFileMobilidade();
        
        foreach(MobilidadeUrbana mobilidadeSelecionada in mobilidadeUrbana){
            if(mobilidadeSelecionada != null){
                if(mobilidadeSelecionada.codigo != codigo){
                    if("M_" + Helpers.NumeroLinhasFicheiro(Constants.FileDirectoryMobilidade) != mobilidadeSelecionada.codigo)
                        Console.Write(mobilidadeSelecionada.tipo + " ou ");
                    else Console.Write(mobilidadeSelecionada.tipo);
                }
            }
        }
    }

    public static bool VerificarSeEstaDisponivelAMobilidade(int codigo, int tempo, int distancia){
        bool confirmacao = false;
        MobilidadeUrbana[] mobilidadeUrbana = ReadFileMobilidade();
        float conta = 0f;
        string codigoMobilidade = "M_" + codigo;
        
        try{
            foreach(MobilidadeUrbana mobilidadeSelecionada in mobilidadeUrbana){
                if(mobilidadeSelecionada != null){
                    conta = mobilidadeSelecionada.custo * tempo;
                    if(VerificarSeTemCodigoMobilidade(codigo, Constants.FileDirectoryMobilidade)){
                        if((mobilidadeSelecionada.autonomia > distancia)
                            && (mobilidadeSelecionada.autonomia > conta)){
                                            confirmacao = true;
                            }
                    }
                }
            }
        } catch(Exception ex){
            Console.WriteLine(ex.Message);
        }

        return confirmacao;
    }

    public static bool VerificarSeTemCodigoMobilidade(int codigo, string filePath){
        string fileString;
        string[] stringToWords;
        bool confirmacao = false;

        try{
            StreamReader streamReader = new StreamReader(filePath);
            fileString = streamReader.ReadLine();
            do{
                if(fileString != null){
                    stringToWords = fileString.Split(' ');
                    if(stringToWords[0].ToLower() == "M_".ToLower() + codigo.ToString().ToLower()) confirmacao = true;
                    fileString = streamReader.ReadLine();
                }
            }while(fileString != null);
            streamReader.Close();
        } catch(Exception ex){
            Console.WriteLine("Error: "+ ex.Message);
        }
        return confirmacao;
    }

    public static void ImprimirMobilidade(){
        MobilidadeUrbana[] tiposMobilidade = new MobilidadeUrbana[20];
        tiposMobilidade = ReadFileMobilidade();
    
        Console.WriteLine("MOBILIDADE");
        DrawTableOfMobilidade(tiposMobilidade);
    }

    public static void InserirMobilidade(){
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

        InserirNoFicheiroMobilidade(Constants.FileDirectoryMobilidade, 
                                    tipoMobilidade, custoMobilidade,
                                    autonomiaMobilidade);

        Console.WriteLine("Mobilidade inserida com sucesso!");
        Helpers.PremirQualquerTeclaParaContinuar();
    }

    public static void DrawTableOfMobilidade(MobilidadeUrbana[] tiposMobilidade){
        Console.WriteLine("_________________________________________________________________");
        Console.WriteLine("|Código\t|Tipo\t\t\t|Custo\t\t|Autonomia\t|");
        foreach(MobilidadeUrbana mobilidade in tiposMobilidade){
            if(mobilidade != null) {
                if(mobilidade.tipo.Length < 7) Console.WriteLine("|" + mobilidade.codigo + "\t|" + mobilidade.tipo + "\t\t\t|" 
                                                    + mobilidade.custo + "\t\t|" + mobilidade.autonomia + "\t\t|");
                else Console.WriteLine("|" + mobilidade.codigo + "\t|" + mobilidade.tipo + "\t\t|" 
                                                    + mobilidade.custo + "\t\t|" + mobilidade.autonomia + "\t\t|");
            }
        }
        Console.WriteLine("|_______________________________________________________________|");
        Helpers.PremirQualquerTeclaParaContinuar();
    }
    
    public static void RemoverMobilidade(){
        int codigo;

        while(true){
            Console.Write("Qual dos códigos mobilidades deseja apagar? M_");
            codigo = int.Parse(Console.ReadLine());
            if(codigo > 0 && RemoverNoFicheiroMobilidade(Constants.FileDirectoryMobilidade, codigo)){
                Console.WriteLine("Mobilidade removida com sucesso.");
                Console.ReadLine();
                break;
            } else Console.WriteLine("Inseriu o código errado.");
        }
    }
}