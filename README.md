# Rota API

## Descrição

Bem-vindo à documentação da API Rota. Esta API permite gerenciar e consultar rotas entre diferentes cidades. A seguir, estão os detalhes das operações disponíveis.

## Tecnologias Utilizadas

- **.NET Core 6.0**
- **MySQL para o banco de dados**

## Funcionalidades

- **Gerenciamento de Rotas**: 
  - Cadastro de rota.
  - Edição de rota.
  - Exclusão de rota.
  - Obter todas as rotas disponíveis.
  - Consulta a melhor rota entre dois pontos.

## Requisitos para Rodar a Aplicação

- SDK do .NET 6.0: Certifique-se de ter o SDK do .NET 6.0 instalado. 
- MySQL: Instale o MySQL e configure um banco de dados para ser usado pela aplicação.

## Instalação

1. Clone o repositório:
    ```sh
    git clone https://github.com/MatheusCavalari/rota-app.git
    cd rota-app
    ```

2. Restaure os pacotes NuGet:
    ```sh
    dotnet restore
    ```
3. Compile e execute o projeto:
    ```sh
    dotnet build
    dotnet run
    ```

### Endpoints
1. **GET /v1/rota
- **URL**: `/v1/rota`
- **Método**: `GET`
- **Resposta de Sucesso**:
    - Código: `200 OK`
    - Corpo: Lista de rotas

2. **GET /v1/rota/1
- **URL**: `/v1/rota/1`
- **Método**: `GET`
- **Resposta de Sucesso**:
    - Código: `200 OK`
    - Corpo: Rota encontrada

3. **POST /v1/rota
- **URL**: `/v1/rota`
- **Método**: `POST`
- **Corpo da Requisição**:
  ```json
  {
  "origem": "GRU",
  "destino": "BRC",
  "valor": 10
  }
  ```
- **Resposta de Sucesso**:
    - Código: `201 CREATED`
    - Corpo: Rota criada

4. **PUT /v1/rota/1
- **URL**: `/v1/rota/1`
- **Método**: `PUT`
- **Corpo da Requisição**:
  ```json
  {
  "origem": "GRU",
  "destino": "BRC",
  "valor": 12
  }
  ```
- **Resposta de Sucesso**:
    - Código: `204 NO CONTENT`

5. **DELETE /v1/rota/1
- **URL**: `/v1/rota/1`
- **Método**: `DELETE`
- **Resposta de Sucesso**:
    - Código: `204 NO CONTENT`

6. **GET /v1/rota/consulta?origem=GRU&destino=BRC
- **URL**: `/v1/rota/consulta?origem=GRU&destino=BRC`
- **Método**: `GET`
- **Resposta de Sucesso**:
  ```json
  {
  "rota": "GRU - BRC ao custo de $10"
  }
  ```

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests.

## Autor

Matheus Cavalari Barbosa
