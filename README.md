# Desafio Blip - Por jpemendonca

## Sobre o Projeto

O **Desafio Blip** foi desenvolvido utilizando **.NET 9** e conta com uma API documentada por meio dos pacotes **OpenApi** e **Scalar**. O projeto está publicado e acessível pelo link:
[**Desafio Blip - Versão Publicada**](https://desafioblip-jpemendonca.azurewebsites.net/)
### Preview da tela inicial

![image](https://github.com/user-attachments/assets/5963f857-624c-4e46-98a1-577fcb0a7cdb)

## Funcionalidades da API `GetRepositories`
```
/api/GitHub/{username}/repositories
```
A API foi projetada para buscar um número mínimo de repositórios de um usuário no GitHub, utilizando como intermediária a API pública do GitHub.

### Parâmetros:
1. **username:** Identificação do usuário os quais os repositórios serão consultados.
2. **Querys opcionais:**
   - **language** Filtra repositórios por linguagem especificada.
   - **minCount** Define o limite mínimo de repositórios no resultado.

![image](https://github.com/user-attachments/assets/6eaa03d4-bf27-4303-9a7b-fc63f978a61a)

## Busca personalizada
Apesar da api ter sido criada para buscar os repositórios do usuário **takenet** pelo bot **Lora**, você também pode utilizar a api para outros usuários. 
No exemplo abaixo busquei pelo menos 5 repositórios na linguagem **C** do usuário **torvalds**, o Linus Torvalds

![image](https://github.com/user-attachments/assets/04550bcc-a1af-4379-9396-2a9f4ccaab66)

## Logs

Cada chamada da API é registrada em logs armazenados em uma tabela SQLite chamada **desafio.db**, localizada junto ao projeto. Os logs podem ser acessados por meio do endpoint:

```
GET /api/Log
```

![image](https://github.com/user-attachments/assets/92c620ff-13a7-406f-a43a-30b76c14ad47)

## Arquitetura e Boas Práticas

### Injeção de Dependência
O projeto utiliza o padrão de **Injeção de Dependência** para gerenciar os serviços de forma eficiente e desacoplada. Esse padrão facilita a manutenção, testes e a expansão da aplicação.

### Testes de Unidade
Foram adicionados testes para as services, utilizando o pacote Xunit da Microsoft

### Arquitetura em Camadas (Layered Architecture)
A aplicação foi desenvolvida seguindo a **arquitetura em camadas**, separando responsabilidades de forma clara:
- **Camada de Apresentação:** Responsável pela interação com o usuário e pela disponibilização dos endpoints.
- **Camada de Aplicação:** Contém as regras de negócio específicas e a orquestração de serviços.
- **Camada de Infraestrutura:** Gerencia o acesso a dados, incluindo a interação com o SQLite e APIs externas.

### SOLID e RESTful
As funções obedecem os príncipios do SOLID e as APIs às regras do padrão RESTful.

### Criação de Exceptions próprias
Foram criadas Exceptions para os casos de problemas na consulta, como UserNotFoundException.

## Tecnologias Utilizadas
- **.NET 9**
- **OpenApi** e **Scalar** (documentação de APIs)
- **SQLite** (Armazenamento de logs)
- **Dapper** (Micro ORM para trabalhar com o banco de dados)
- **Xunit** (Para testes de unidade)

### Pré-requisitos:
1. Instalar o **.NET 9**.
2. Clonar o repositório do projeto:
   ```bash
   git clone https://github.com/jpemendonca/ApiGithubDesafioBlip.git
   ```
3. Navegar até o diretório do projeto:
   ```bash
   cd ApiGithubDesafioBlip
   cd ApiGithubDesafioBlip
   ```

### Aviso importante
O **token** do github, para o acesso da api, localizado em appsettings.json pode ter expirado. Se tiver expirado, a consulta a api pública do github não irá funcionar. Gere um novo token em https://github.com/settings/tokens e troque no appsettings.json

### Executar a API:
1. Restaurar as dependências:
   ```bash
   dotnet restore
   ```
2. Iniciar a aplicação:
   ```bash
   dotnet run
   ```
3. Acessar a documentação da API:
   - [http://localhost:5182](http://localhost:5182)
