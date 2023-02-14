# Tryitter

Tryitter é uma rede social baseada em texto (e imagens) com o objetivo de proporcionar
um ambiente em que pessoas estudantes da Trybe possam compartilhar suas experiências
e também acessar posts que possam contribuir para seu aprendizado.

Este projeto foi desenvolvido em dupla, com o objetivo de colocar em prática os
conhecimentos adquiridos durante o curso de Aceleração em C# da Trybe.

Feito por:
- [Brendon Lopes](http://github.com/brendon-lopes)
- [André Morsch](http://github.com/andremorsch)

## Principais tecnologias utilizadas

- .NET Core 6.0
- Entity Framework Core 6.0
- ASP.NET Core 6.0
- Fluent Validation
- SQL Server
- BCrypt
- XUnit
- Fluent Assertions
- Docker / Docker Compose

## Como rodar o projeto
1. Clone este repositório
    ```bash
    git clone git@github.com:Brendon-Lopes/Tryitter.git
    ```

2. Entre na pasta do projeto
    ```bash
    cd Tryitter
    ```

3. Rode o Docker Compose
    ```bash
    docker-compose up -d
    ```
   - Pode demorar alguns minutos para a aplicação rodar.


4. A URL da documentação da API é a seguinte:

   - http://localhost:5000/swagger/index.html

## Como rodar os testes

1. Entre na pasta de testes
    ```bash
    cd Server/BackEndTryitter/TestTryitter
    ```

2. Instale as dependências
    ```bash
    dotnet restore
    ```

3. Rode os testes
    ```bash
    dotnet test
    ```
