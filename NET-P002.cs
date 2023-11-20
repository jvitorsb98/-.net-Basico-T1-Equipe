using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

class Tarefa
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime DataVencimento { get; set; }
    public bool Concluida { get; set; }
}

class GerenciadorTarefas
{
    private static List<Tarefa> tarefas = new List<Tarefa>();

    static void Main(string[] args)
    {
        bool emExecucao = true;
        while (emExecucao)
        {
            ExibirMenu();

            if (int.TryParse(Console.ReadLine(), out int opcao))
            {
                emExecucao = ProcessarOpcao(opcao);
            }
            else
            {
                Console.WriteLine("Opção inválida! Por favor, digite um número válido.");
            }
        }
    }

    private static void ExibirMenu()
    {
        Console.WriteLine("Escolha uma opção:");
        Console.WriteLine("1 - Adicionar Tarefa");
        Console.WriteLine("2 - Exibir Tarefas");
        Console.WriteLine("3 - Marcar Tarefa como Concluída");
        Console.WriteLine("4 - Exibir Tarefas Pendentes");
        Console.WriteLine("5 - Exibir Tarefas Concluídas");
        Console.WriteLine("6 - Excluir Tarefa");
        Console.WriteLine("7 - Pesquisar Tarefa por Palavra-Chave");
        Console.WriteLine("8 - Exibir Estatísticas");
        Console.WriteLine("9 - Sair");
    }

    private static bool ProcessarOpcao(int opcao)
    {
        switch (opcao)
        {
            case 1:
                AdicionarTarefa();
                break;
            case 2:
                ExibirTarefas();
                break;
            case 3:
                MarcarComoConcluida();
                break;
            case 4:
                ExibirTarefasPendentes();
                break;
            case 5:
                ExibirTarefasConcluidas();
                break;
            case 6:
                ExcluirTarefa();
                break;
            case 7:
                PesquisarTarefasPorPalavraChave();
                break;
            case 8:
                ExibirEstatisticas();
                break;
            case 9:
                Console.WriteLine("Encerrando o Gerenciador de Tarefas. Adeus!");
                return false; // Sair do loop
            default:
                Console.WriteLine("Opção inválida! Por favor, digite um número válido.");
                break;
        }

        return true; // Continuar no loop
    }

    private static void AdicionarTarefa()
    {
        Console.WriteLine("Digite o título da tarefa:");
        string titulo = Console.ReadLine();

        Console.WriteLine("Digite a descrição da tarefa:");
        string descricao = Console.ReadLine();

        DateTime dataVencimento;
        do
        {
            Console.WriteLine("Digite a data de vencimento da tarefa (dd/MM/aaaa):");
        } while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataVencimento));

        Tarefa novaTarefa = new Tarefa
        {
            Titulo = titulo,
            Descricao = descricao,
            DataVencimento = dataVencimento,
            Concluida = false
        };

        tarefas.Add(novaTarefa);
        Console.WriteLine("Tarefa adicionada com sucesso!");
    }

    private static void ExibirTarefas()
    {
        Console.WriteLine("Lista de Tarefas:");
        foreach (var tarefa in tarefas)
        {
            Console.WriteLine($"Título: {tarefa.Titulo} - Vencimento: {tarefa.DataVencimento.ToShortDateString()} - Concluída: {(tarefa.Concluida ? "Sim" : "Não")}");
        }
    }

    private static void MarcarComoConcluida()
    {
        Console.WriteLine("Digite o índice da tarefa a ser marcada como concluída:");
        int indice;
        if (int.TryParse(Console.ReadLine(), out indice) && indice >= 0 && indice < tarefas.Count)
        {
            tarefas[indice].Concluida = true;
            Console.WriteLine("Tarefa marcada como concluída!");
        }
        else
        {
            Console.WriteLine("Índice inválido!");
        }
    }

    private static void ExibirTarefasPendentes()
    {
        var tarefasPendentes = tarefas.Where(t => !t.Concluida);
        Console.WriteLine("Tarefas Pendentes:");
        foreach (var tarefa in tarefasPendentes)
        {
            Console.WriteLine($"Título: {tarefa.Titulo} - Vencimento: {tarefa.DataVencimento.ToShortDateString()}");
        }
    }

    private static void ExibirTarefasConcluidas()
    {
        var tarefasConcluidas = tarefas.Where(t => t.Concluida);
        Console.WriteLine("Tarefas Concluídas:");
        foreach (var tarefa in tarefasConcluidas)
        {
            Console.WriteLine($"Título: {tarefa.Titulo} - Vencimento: {tarefa.DataVencimento.ToShortDateString()}");
        }
    }

    private static void ExcluirTarefa()
    {
        Console.WriteLine("Digite o índice da tarefa a ser excluída:");
        int indice;
        if (int.TryParse(Console.ReadLine(), out indice) && indice >= 0 && indice < tarefas.Count)
        {
            tarefas.RemoveAt(indice);
            Console.WriteLine("Tarefa excluída!");
        }
        else
        {
            Console.WriteLine("Índice inválido!");
        }
    }

    private static void PesquisarTarefasPorPalavraChave()
    {
        Console.WriteLine("Digite a palavra-chave para a pesquisa:");
        string palavraChave = Console.ReadLine();
        var resultadoPesquisa = tarefas.Where(t => t.Titulo.Contains(palavraChave) || t.Descricao.Contains(palavraChave));
        Console.WriteLine($"Resultado da Pesquisa por '{palavraChave}':");
        foreach (var tarefa in resultadoPesquisa)
        {
            Console.WriteLine($"Título: {tarefa.Titulo} - Vencimento: {tarefa.DataVencimento.ToShortDateString()} - Concluída: {(tarefa.Concluida ? "Sim" : "Não")}");
        }
    }

    private static void ExibirEstatisticas()
    {
        int tarefasConcluidas = tarefas.Count(t => t.Concluida);
        int tarefasPendentes = tarefas.Count(t => !t.Concluida);

        var tarefaMaisAntiga = tarefas.OrderBy(t => t.DataVencimento).FirstOrDefault();
        var tarefaMaisRecente = tarefas.OrderByDescending(t => t.DataVencimento).FirstOrDefault();

        Console.WriteLine($"Número de Tarefas Concluídas: {tarefasConcluidas}");
        Console.WriteLine($"Número de Tarefas Pendentes: {tarefasPendentes}");
        Console.WriteLine($"Tarefa Mais Antiga: {(tarefaMaisAntiga != null ? tarefaMaisAntiga.Titulo : "Nenhuma tarefa registrada")}");
        Console.WriteLine($"Tarefa Mais Recente: {(tarefaMaisRecente != null ? tarefaMaisRecente.Titulo : "Nenhuma tarefa registrada")}");
    }
}

