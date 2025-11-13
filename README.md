# Projeto FullStack - Library

![Status](https://img.shields.io/badge/Status-Finalizado-green6?style=flat-square)
![C#](https://img.shields.io/badge/C%23-512BD4?style=flat-square&logo=csharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![Angular](https://img.shields.io/badge/Angular-DD0031?style=flat-square&logo=angular&logoColor=white)
![TypeScript](https://img.shields.io/badge/TypeScript-3178C6?style=flat-square&logo=typescript&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-336791?style=flat-square&logo=postgresql&logoColor=white)
![PrimeNG](https://img.shields.io/badge/PrimeNG-2266B5?style=flat-square&logo=primeng&logoColor=white)

Projeto full-stack de CRUD para uma biblioteca, desenvolvido como avaliação técnica.

---

## Sobre o Projeto

O objetivo do desafio foi desenvolver um projeto básico de CRUD (Create, Read, Update, Delete) para uma biblioteca, avaliando o entendimento e a aplicação dos conceitos de backend, frontend, banco de dados e raciocínio lógico.

O projeto consiste em uma API RESTful em .NET para gerenciar livros e categorias, e uma interface de usuário (UI) em Angular para consumir essa API.

## Funcionalidades

- **Backend:**
  - CRUD completo de Livros.
  - CRUD completo de Categorias.
  - Relacionamento Muitos-para-Muitos (N-N) entre Livros e Categorias.
  - Arquitetura de Serviços (Service Layer) para lógica de negócio.
  - Mapeamento de DTOs com AutoMapper.
  - Tratamento de exceções centralizado com `GlobalExceptionHandler`.
  - Armazenamento seguro de senhas com .NET User Secrets.
- **Frontend:**
  - Interface reativa para listar, criar, editar e excluir livros.
  - Filtragem de livros por título.
  - Formulários reativos (Reactive Forms) para validação.
  - Componentes de UI modernos com PrimeNG e o tema Aura.
  - Modais de confirmação e notificações (Toast).

---

## Tecnologias Utilizadas

| Backend            | Frontend               | Banco de Dados        |
| :----------------- | :--------------------- | :-------------------- |
| .NET 8             | Angular                | PostgreSQL            |
| C#                 | TypeScript             | Entity Framework Core |
| EF Core Migrations | PrimeNG (Tema Aura)    |                       |
| AutoMapper         | Angular Reactive Forms |                       |
| API RESTful        | HttpClient             |                       |

---

## Regra de Negócio

O sistema possui uma validação de regra de negócio centralizada no `Service Layer` do backend:

`Livro não deve ser cadastrado com mais de 3 categorias.`

Se a regra for violada, a API retorna um erro 400 Bad Request, que é capturada pelo `GlobalExceptionHandler`:

```json
{
  "title": "Bad Request",
  "status": 400,
  "detail": "Um livro não pode ter mais de 3 categorias.",
  "instance": "/api/v1/books"
}
```

---

## Rodando o Projeto

Pré-requisitos

- .NET 8 SDK

- Angular CLI (v18+)

- Node.js (v20+)

- Um servidor PostgreSQL rodando.

---

### 1. Backend (.NET)

1. Clone o repositório.

2. Navegue até a pasta da solução (/Library).

3. Configure os Segredos: O projeto usa .NET User Secrets para proteger a senha do banco.

```bash
# Navegue até a pasta do projeto
cd BackendLibrary

# Inicializa o User Secrets
dotnet user-secrets init

# Configure sua connection string
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=biblioteca_db;Username=SEU_USER;Password=SUA_SENHA;"
```

4. Aplique as Migrações: Este comando criará as tabelas no seu banco PostgreSQL.

```bash
dotnet ef database update
```

5. Rode o backend:

```bash
dotnet run
```

### 2. Frontend (Angular)

1. Em um novo terminal, navegue até a pasta do frontend (/FrontendLibrary).

2. Instale as dependências:

```bash
npm install
```

3. Rode o frontend:

```bash
npm start
```

---

## API Endpoints

O backend expõe os seguintes endpoints RESTful:

### Livros

| Método   | Rota                           | Descrição                    |
| :------- | :----------------------------- | :--------------------------- |
| `GET`    | `/api/v1/books`                | Busca todos os livros.       |
| `GET`    | `/api/v1/books?title={titulo}` | Filtra livros por título.    |
| `GET`    | `/api/v1/books/{id}`           | Busca um livro por ID.       |
| `POST`   | `/api/v1/books`                | Cria um novo livro.          |
| `PUT`    | `/api/v1/books/{id}`           | Atualiza um livro existente. |
| `DELETE` | `/api/v1/books/{id}`           | Deleta um livro.             |

---

### Categorias

| Método   | Rota                      | Descrição                   |
| :------- | :------------------------ | :-------------------------- |
| `GET`    | `/api/v1/categories`      | Busca todas as categorias.  |
| `GET`    | `/api/v1/categories/{id}` | Busca uma categoria por ID. |
| `POST`   | `/api/v1/categories`      | Cria uma nova categoria.    |
| `PUT`    | `/api/v1/categories/{id}` | Atualiza uma categoria.     |
| `DELETE` | `/api/v1/categories/{id}` | Deleta uma categoria.       |
