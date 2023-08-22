<!-- # TODO's

## Arquitetura ##
- Descrever estilos e padrões de arquiteturas escolhidos (camadas com REST... etc)
- Descrever um Modelo Entidade Relacional do banco (pelo menos do Core talvez?)

## Frameworks, pacotes terceiros, ORM's, etc ##

## Segurança ##

- Descrever o fluxo geral implementado (seguindo o OAuth2 no backend):
    - (talvez uns diagramas UML de sequência?)
    - Descrever a rotação da chave e o Key Vault (citar a necessidade de um serviço de instância única)
    - Descrever a assinatura do JWT com chave assimétrica
    - Descrever o JWKS
    - Descrever a validação do JWT com a chave pública
    - Descrever a autenticação por cookie/sessão no Web (Front)

- Descrever alguns possíveis ataques e como a aplicação está segura contra isso:
    - SQL Injection => (ORM's, parametrização por procedures, etc..)
    - Brute Force => (Lockout, Hash de senhas pelo Identity)
    - Cross Site Scripting (XSS) => (Boas validações nas controllers, HTTP Only Cookie, etc)
    - Cross Site Request Forgery (CSRF) => (Validação de Anti Forgery Token, CORS (habilitado por padrão pelo ASP .NET Core))
    - Man in the Middle => (HTTPS, HTTPS Redirection, HSTS)

## CI/CD, como é feito o deploy, etc ##
    - Descrever o que criamos de recursos através do ARM Template:
        - Container Registry *obrigatório (para guardar as imagens das app's )
        - Container Instance (rodar as imagens das app's em si)
        - Blob Storage (imagens das notícias)
        - Key Vault (chave privada de criptografia)
        - SQL Database (notícias, usuários, etc)
    - Github Actions
    - Database scripts ou Migrations
    - Automatização
    - Estratégia de deploy e rollback (faremos por deployment slots usando app services ou containers usando ACI?)
    - etc...

-->

<h1 align="center">Tech News</h1>
<h4 align="center">Um blog de notícias para a galera tech.</h4>

<p align="center">
  <a href="">
    <img src="https://img.shields.io/badge/version-1.0.0-blue"
         alt="version">
  </a>
  <a href="">
    <img src="https://img.shields.io/badge/license-MIT-green"
         alt="license">
  </a>
</p>

<p align="center">
  <a href="">
    <img src=".github\images\website-demo.png" alt="website-demo">
  </a>
</p>


## Sumário

- [Sobre](#sobre)
- [Tecnologias](#tecnologias)
- [Suporte ao Browser](#suporte-ao-browser)
- [Arquitetura](#arquitetura)
    - [Web App](#web-app)
    - [Core API](#core-api)
    - [Auth API (Authorization Server)](#auth-api-authorization-server)
- [Segurança](#segurança)
- [CI / CD](#ci-cd)
- [Executando a aplicação](#executando-a-aplicação)
    - [Docker](#docker)


## Sobre
Este projeto foi criado para atender os requisitos do projeto Tech Challenge da [ Faculdade de Tecnologia - FIAP](https://postech.fiap.com.br/?gclid=Cj0KCQjwnf-kBhCnARIsAFlg49228y9z3y6lf_mWZEekgcxZRZBDavxtRT-zAUNs33TZOJtXpGVMNlAaAue5EALw_wcB).<br>
O sistema do TechNews consiste em três aplicações: 
- Uma aplicação Web App MVC que exibe as notícias do blog.
- Uma API de gerenciamento das notícias (requer autenticação OAuth2).
- Uma API dedicada ao gerenciamento, autenticação e autorização dos usuários.

## Tecnologias

| Web App | API's | ORM | Database
| --- | --- | --- | --- |
| [![bootstrap-version](https://img.shields.io/badge/Bootstrap-5.0.2-purple)](https://getbootstrap.com/)<br>[![fontawesome-version](https://img.shields.io/badge/Font_Awesome-6.4.0-yellow)](https://fontawesome.com/)<br>[![aspnetcore-version](https://img.shields.io/badge/ASP.NET_Core_MVC-7.0-blue)](https://learn.microsoft.com/pt-br/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-7.0)| [![aspnetcore-version](https://img.shields.io/badge/ASP.NET_Core-7.0-blue)](https://learn.microsoft.com/pt-br/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-7.0) | [![dapper-version](https://img.shields.io/badge/EF_Core-7.0-red)](https://learn.microsoft.com/en-us/ef/core/) | ![database](https://img.shields.io/badge/SQL_Server-gray)

## Suporte ao Browser

| <img src="https://user-images.githubusercontent.com/1215767/34348387-a2e64588-ea4d-11e7-8267-a43365103afe.png" alt="Chrome" width="16px" height="16px" /> Chrome | <img src="https://user-images.githubusercontent.com/1215767/34348590-250b3ca2-ea4f-11e7-9efb-da953359321f.png" alt="IE" width="16px" height="16px" /> Internet Explorer | <img src="https://user-images.githubusercontent.com/1215767/34348380-93e77ae8-ea4d-11e7-8696-9a989ddbbbf5.png" alt="Edge" width="16px" height="16px" /> Edge | <img src="https://user-images.githubusercontent.com/1215767/34348394-a981f892-ea4d-11e7-9156-d128d58386b9.png" alt="Safari" width="16px" height="16px" /> Safari | <img src="https://user-images.githubusercontent.com/1215767/34348383-9e7ed492-ea4d-11e7-910c-03b39d52f496.png" alt="Firefox" width="16px" height="16px" /> Firefox |
| :---------: | :---------: | :---------: | :---------: | :---------: |
| Yes | 11+ | Yes | Yes | Yes |


## Arquitetura
Esta é uma visão geral da arquitetura do TechNews.

<p align="center">
  <a href="">
    <img src=".github\images\architecture-overview.png" alt="overview-architecture">
  </a>
</p>

### Web App

A concepção da aplicação foi fundamentada no padrão arquitetural MVC (Model View Controller), sendo implementada por meio do ASP.NET Core.
No âmbito do negócio, sua responsabilidade é requisitar os recursos restritos disponibilizados pela API de notícias e, posteriormente, apresentando esses elementos aos usuários.
Quanto à segurança, a aplicação assume a responsabilidade de transmitir os dados referentes à criação de usuários ou as credenciais de acesso ao Authorization Server. Mais detalhes em [Segurança](#segurança)


Caso as credenciais estiverem corretas, a aplicação irá receber um JWT assinado (JWS - Json Web Signature) e irá autenticar o usuário via cookie. Então, para cada requisição feita à API de notícias o mesmo JWT será enviado pelo header.

<p align="center">
  <a href="">
    <img src=".github\images\webapp-architecture.png" alt="webapp-architecture">
  </a>
</p>

### Core API

Work in Progress

### Auth API (Authorization Server)

Foi adotado o estilo arquitetural REST (Representational State Transfer) com camadas, utilizando ASP.NET Core.
A camada de <b>Filtros</b> lidam com exceções e tratamento de Model State inválidos.
A camada de <b>Controllers</b> direciona o fluxo das requisições. É responsável por expor os parâmetros públicos da chave assimétrica, realizar chamadas ao Identity para autenticação/autorização do usuário e criar o JWT utilizando as classes de Serviços.
A camada de <b>Data</b> se integra com classes do Identity (User e Role) e com o Entity Framework para mapeamento de dados.
A camada de <b>Services</b> possui serviços com responsabilidades diversas como: gerenciar (buscar ou persistir) a chave privada no Azure Key Vault, assinar um token digitalmente com criptografia RSA e a criação da chave assimétrica através de criptografia RSA.
O <b>Background Service</b> que vemos abaixo é uma parte dos serviços. Ele constitui uma solução simples para a rotação da chave privada que gera os tokens. O ideal é possuir uma solução mais robusta, consistindo em uma aplicação que gerencia a rotação da chave para todas as instâncias de aplicações que a utilizam, de preferência sendo uma única instância, para evitar concorrência na geração da chave.

<p align="center">
  <a href="">
    <img src=".github\images\architecture-auth.png" alt="api-architecture">
  </a>
</p>

## Segurança

Work in Progress

## CI / CD

Work in Progress

## Executando a aplicação
É possível executar a aplicação realizando a configuração manualmente, ou utilizando Docker (recomendado).

### Docker
Para rodar localmente, é possível utilizar o Docker.  
Abaixo o passo a passo para executar a aplicação localmente:
- Realizar o clone do projeto na pasta desejada:
    ```bash
        git clone https://github.com/pistoladas-group/tech-challenge-02.git
    ```
- Configurar certificados para habilitar conexão via https:
    ```bash
        dotnet dev-certs https -ep "$env:USERPROFILE\.aspnet\https\technews.pfx"  -p "OVmTv9lykb0)>m=wWcQaJ"
        dotnet dev-certs https --trust
    ```
- Utilizar o comando abaixo para subir a aplicação utilizando docker-compose:
    ```bash
        docker-compose -f docker-compose.debug.yml up --build
    ```