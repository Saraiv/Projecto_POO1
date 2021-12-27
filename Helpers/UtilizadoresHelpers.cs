using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;

class UtilizadorHelpers{
    private static Utilizador[] ReadFileUtilizador(){
        Utilizador[] todosUtilizadores = new Utilizador[20];
        string fileString;
        string[] stringToWords;
        List<string> list = new List<string>();
        int i = 0;
        try{
            StreamReader streamReader = new StreamReader(Constants.FileDirectoryUtilizadores);
            fileString = streamReader.ReadLine();
            do{
                if(fileString != null){
                    stringToWords = fileString.Split(' ');

                    for(int j = 3; j < stringToWords.Length; j++){
                        if(stringToWords[2] != null) stringToWords[2] += " " + stringToWords[j];
                    }

                    todosUtilizadores[i] = new Utilizador(stringToWords[0], 
                                                int.Parse(stringToWords[1]), 
                                                stringToWords[2]);
                    fileString = streamReader.ReadLine();
                    i++;
                }
            }while(fileString != null);
            streamReader.Close();
        } catch(Exception ex){
            Console.WriteLine("Error: " + ex.Message);
        }
        
        return todosUtilizadores;
    }

    private static void DrawTableOfUtilizadores(Utilizador[] todosUtilizadores){
        Console.WriteLine("_________________________________________________");
        Console.Write("|Nome\t\t|NIF\t\t|Pedidos\t|\n");
        foreach(Utilizador utilizador in todosUtilizadores){
            if(utilizador != null) {
                if(utilizador.nome.Length > 6) Console.WriteLine("|" + utilizador.nome 
                                                            + "\t|" + utilizador.nif
                                                            + "\t|" + utilizador.pedidos + " \t\t|");
                else Console.WriteLine("|" + utilizador.nome 
                                   + "\t\t|" + utilizador.nif
                                   + "\t|" + utilizador.pedidos + " \t\t|");
            }
        }
        Console.WriteLine("|_______________________________________________|");  
        Helpers.PremirQualquerTeclaParaContinuar();
    }

    public static void ImprimirUtilizadores(){
        Utilizador[] todosUtilizadores = new Utilizador[20];
        todosUtilizadores = ReadFileUtilizador();
        
        Console.WriteLine("UTILIZADORES");
        DrawTableOfUtilizadores(todosUtilizadores);
    }

    public static bool ConfirmacaoSeHaUtilizador(int nif){
        bool confirmacao = false;
        Utilizador[] todosUtilizadores = ReadFileUtilizador();

        foreach(Utilizador utilizadorSelecionado in todosUtilizadores){
            if(utilizadorSelecionado != null){
                if(utilizadorSelecionado.nif == nif){
                    confirmacao = true;
                    break;
                } else continue;
            }
        }

        return confirmacao;
    }
}