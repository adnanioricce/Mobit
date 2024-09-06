# Teste Mobit
O projeto aaixo se trata de uma aplicação desenvolvida para o teste da Mobit

# Estrutura do Projeto

- Backend: Web API em ASP.NET Core.
- Frontend: Aplicação Angular, na pasta ClientApp.
- Banco de Dados: PostgreSQL, utilizando Entity Framework Core para ORM.
- Docker: O projeto é configurado para ser executado em contêineres Docker, com um arquivo docker-compose.yml para orquestração.

# Requisitos

- .NET SDK (versão mínima recomendada: 8.0)
- Node.js e npm (para o frontend Angular)
- Docker e Docker Compose

# Instruções para Build e Execução
## Executando o projeto com docker-compose

### 1. Clonar o Repositório

Primeiro, clone o repositório do projeto para o seu ambiente local:

```bash
git clone https://github.com/adnanioricce/Mobit.git
cd Mobit
```

### 2. Configurar Variáveis de Ambiente

Certifique-se de que as variáveis de ambiente necessárias para a aplicação estão configuradas. Eu já deixei alguns valores padrão no docker-compose.yml se precisar, mas certifique-se por exemplo, e que o projeto não esteja usando uma porta já em uso, se esta na porta correta ou mesmo se você quer usar outra string de conexão.

```yml
environment:
    ConnectionStrings__DefaultConnection: "Host=db;Username=mobituser;Password=mobitpass;Database=mobitdb"
    ASPNETCORE_URLS: http://*:80
```

### 3. Construir e Rodar o Projeto com Docker

O projeto inclui um arquivo docker-compose.yml para facilitar a criação dos contêineres de aplicação e banco de dados. Para construir e rodar a aplicação, execute:

```bash
docker-compose up --build
```

Este comando irá:

- Construir e iniciar o contêiner do banco de dados PostgreSQL.
- Executar as migrações do Entity Framework Core automaticamente(arquivo init.sql na pasta Database).
- Construir e iniciar o contêiner da aplicação ASP.NET Core com Angular.

### 4. Acessar a Aplicação

Após a inicialização, a aplicação estará disponível em http://localhost:5080 no seu navegador e o banco na porta 5456.

### 5. Parar a Aplicação

Para parar a aplicação e os contêineres Docker, execute:

```bash
docker-compose down
```
## Executando o Projeto Localmente (Sem Docker)

### 1. Navegue até a pasta do projeto:

```bash
cd Mobit.Web/
```
### 2. Execute o build do projeto:

```bash
dotnet restore
```

### 3. Execute as migrações:

```bash
dotnet ef database update
```

### 4. Inicie a aplicação:

```bash
dotnet run
```

O frontend será iniciado em conjunto