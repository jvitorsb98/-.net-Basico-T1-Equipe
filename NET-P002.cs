using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

class Tarefa{
    
    public string titulo {get; set;}
    public string descricao {get; set;}
    public DateTime dataDeVencimento {get; set;}
    public bool concluida {get; set;}
    
    public Tarefa(string _titulo, string _descricao, DateTime _dataDeVencimento){
        titulo = _titulo;
        descricao = _descricao;
        dataDeVencimento = _dataDeVencimento;
        concluida = false;
    }
}

class GerenciadorTarefas {
  static void Main() {
      
      List<Tarefa> listaTarefas = new List<Tarefa>();
      int opcao;
      
    do
    {
        Console.WriteLine("1 - Criar tarefa");
        Console.WriteLine("2 - Listar todas as tarefas criadas");
        Console.WriteLine("3 - Marcar uma tarefa como concluída");
        Console.WriteLine("4 - Lista de tarefas pendentes");
        Console.WriteLine("5 - Lista de tarefas concluídas");
        Console.WriteLine("6 - Excluir uma tarefa");
        Console.WriteLine("7 - Pesquisar uma tarefa por palavra-chave");
        Console.WriteLine("8 - Estatísticas");
        Console.WriteLine("9 - Sair");
        
        opcao = int.Parse(Console.ReadLine());
        
        if(opcao == 1){
            
            CriarTarefa(listaTarefas);
            
        }else if(opcao == 2){
            
            ListarTarefas(listaTarefas);
            
        }else if(opcao == 3){
            
            ConcluirTarefa(listaTarefas);
            
        }else if(opcao == 4){
            
            TarefasPendentes(listaTarefas);
            
        }else if(opcao == 5){
            
            TarefasConcluidas(listaTarefas);
            
        }
        else if(opcao == 6){
            
            ExcluirTarefa(listaTarefas);
            
        }else if(opcao == 7){
            
            Pesquisar(listaTarefas);
            
        }else if(opcao == 8){
            
            Estatisticas(listaTarefas);
            
        }else if(opcao == 9){
            
            Console.WriteLine("Programa Encerrado!");
        }
        
    }while(opcao!=9);
  }
    static void CriarTarefa(List<Tarefa> listaTarefas){
        Console.WriteLine("Digite o titulo da tarefa:");
        string _titulo = Console.ReadLine();
        
        Console.WriteLine("Digite a descricao da tarefa:");
        string _descricao = Console.ReadLine();
        
        DateTime _dataDeVencimento;
        do
        {
            Console.WriteLine("Digite a data de vencimento (dd/MM/aaaa):");
        } while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _dataDeVencimento));
/*
        Console.WriteLine("Digite a data de vencimento:");
        DateTime _dataDeVencimento = DateTime.Parse(Console.ReadLine());*/
        
        Tarefa novaTarefa = new Tarefa(_titulo, _descricao, _dataDeVencimento);
        listaTarefas.Add(novaTarefa);
        
        Console.WriteLine("A tarefa foi criada!");
    }
    
    static void ListarTarefas(List<Tarefa> listaTarefas){
        Console.WriteLine("Abaixo estão todas as tarefas:");
        foreach(Tarefa tarefa in listaTarefas){
            Console.WriteLine($"Título: {tarefa.titulo}, Descrição: {tarefa.descricao}, Vencimento: {tarefa.dataDeVencimento}, Concluída: {tarefa.concluida}");
          }
    }
    
    static void ConcluirTarefa(List<Tarefa> listaTarefas){
        
        Console.WriteLine("Digite o título da tarefa que você deseja marcar como concluída:");
        string _titulo = Console.ReadLine();
        
        Tarefa aux_tarefa = null;
        
        foreach(Tarefa tarefa in listaTarefas){
            if(string.Equals(_titulo, tarefa.titulo, StringComparison.OrdinalIgnoreCase)){
                aux_tarefa = tarefa;
                break;
            }
        }
        
        if(aux_tarefa != null ){
            Console.WriteLine("Tarefa concluída com sucesso!");
            aux_tarefa.concluida = true;
            
        }else{
            Console.WriteLine("Título não encontrado!");
        }
    }
    
    static void TarefasPendentes(List<Tarefa> listaTarefas){
        
        Console.WriteLine("Abaixo estão todas as tarefas pendentes:");
        foreach(Tarefa tarefa in listaTarefas){
            if(tarefa.concluida){
                }else{
                    Console.WriteLine($"Título: {tarefa.titulo}, Descrição: {tarefa.descricao}, Vencimento: {tarefa.dataDeVencimento}, Concluída: {tarefa.concluida}");
                }
        }
    }
    
    static void TarefasConcluidas(List<Tarefa> listaTarefas){
        
        Console.WriteLine("Abaixo estão todas as tarefas concluidas:");
        
        foreach(Tarefa tarefa in listaTarefas){
            if(tarefa.concluida){
                Console.WriteLine($"Título: {tarefa.titulo}, Descrição: {tarefa.descricao}, Vencimento: {tarefa.dataDeVencimento}, Concluída: {tarefa.concluida}");
            }
        }
    }
    
    static void ExcluirTarefa(List<Tarefa> listaTarefas){
        
        Console.WriteLine("Digite o titulo da tarefa que voce deseja excluir:");
        string _titulo = Console.ReadLine();
        
        
        for(int i=listaTarefas.Count - 1; i>=0; i--){
            Tarefa tarefa = listaTarefas[i];
            if(string.Equals(_titulo, tarefa.titulo, StringComparison.OrdinalIgnoreCase)){
                listaTarefas.RemoveAt(i);
                Console.WriteLine("Tarefa removida com sucesso!");
                break;
            }else{
                Console.WriteLine("Titulo não encontrado!");
            }
        }
    }
    
    static void Pesquisar(List<Tarefa> listaTarefas){
        Console.WriteLine("Digite uma palavra-chave para encontrar tarefas correspondentes:");
        string palavraChave = Console.ReadLine();
        
        var tarefasCorrespondentes = listaTarefas.FindAll(t => t.titulo.Contains(palavraChave, StringComparison.OrdinalIgnoreCase)||t.descricao.Contains(palavraChave, StringComparison.OrdinalIgnoreCase));
        
        if (tarefasCorrespondentes.Count < 1) {
            Console.WriteLine("Não foram encontrardas correspondencias.");
        }else{
            Console.WriteLine("Abaixo estão todas as tarefas correspondentes aa palavra-chave digitada:");
            foreach(Tarefa tarefa in tarefasCorrespondentes){
                Console.WriteLine($"Título: {tarefa.titulo}, Descrição: {tarefa.descricao}, Vencimento: {tarefa.dataDeVencimento}, Concluída: {tarefa.concluida}");
            }
        }    
    }
  
    static void Estatisticas(List<Tarefa> listaTarefas){
        int tarefasConcluidas = listaTarefas.Count(tarefa => tarefa.concluida);
        int tarefasPendentes = listaTarefas.Count(tarefa => !tarefa.concluida);
        
        if(listaTarefas.Count > 0){
            var tarefaAntiga = listaTarefas.OrderBy(tarefa => tarefa.dataDeVencimento).First();
            var tarefaNova = listaTarefas.OrderByDescending(tarefa => tarefa.dataDeVencimento).First();
            
            Console.WriteLine($"Numero de tarefas concluidas: {tarefasConcluidas}");
            Console.WriteLine($"Numero de tarefas pendentes: {tarefasPendentes}");
            Console.WriteLine($"Tarefa com data de vencimento mais proxima: {tarefaAntiga.titulo}");
            Console.WriteLine($"Tarefa com data de vencimento mais distante: {tarefaNova.titulo}");
        }else{
            Console.WriteLine("Nao ha tarefas!");
        }
    }
}