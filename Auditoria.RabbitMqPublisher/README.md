# RabbitMQ Publisher Console App (C#)

Este é um programa console simples em C# para publicar mensagens em uma fila específica do RabbitMQ, compatível com a biblioteca `RabbitMQ.Client` v6.x.

## Pré-requisitos

- **SDK do .NET 8**: [Download .NET](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Instância do RabbitMQ**: Acessível a partir de onde você executará este programa.

## Configuração

- Abra o arquivo `Program.cs`.
- Ajuste as constantes `RabbitMqHost`, `QueueName`, `UserName` (se necessário) e `Password` (se necessário) para corresponder à sua configuração do RabbitMQ e à fila que o consumidor está ouvindo (padrão: `my_test_queue`).

## Como Compilar e Executar

1.  **Restaurar Dependências**: Navegue até o diretório `/RabbitMqPublisher` no terminal e execute:
    ```bash
    dotnet restore
    ```
2.  **Construir o Projeto**: Ainda no diretório do publisher, execute:
    ```bash
    dotnet build
    ```
3.  **Executar**: Execute o programa a partir do diretório de build (geralmente `bin/Debug/net8.0`) ou usando `dotnet run`, passando a mensagem como argumento entre aspas:
    ```bash
    # Exemplo usando dotnet run a partir do diretório do projeto
    dotnet run "Sua mensagem de teste aqui"
    
    # Exemplo executando o .exe diretamente (o caminho pode variar)
    # ./bin/Debug/net8.0/RabbitMqPublisher "Outra mensagem"
    ```

O programa se conectará ao RabbitMQ, declarará a fila (para garantir que ela exista) e publicará a mensagem fornecida.
