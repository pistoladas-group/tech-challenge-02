# Tech News

Work In Progress

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
        dotnet dev-certs https -ep "$env:USERPROFILE\.aspnet\https\technews.pfx"  -p OVmTv9lykb0)>m=wWcQaJ
        dotnet dev-certs https --trust
    ```
- Utilizar o comando abaixo para subir a aplicação utilizando docker-compose:
    ```bash
        docker-compose -f docker-compose.debug.yml up --build
    ```

<!-- # TODO's
- Descrever o fluxo OAuth implementado:
    - (talvez uns diagramas UML de sequência?)
    - Descrever a rotação da chave e o Key Vault (citar a necessidade de um serviço de instância única)
    - Descrever a assinatura do JWT com chave assimétrica
    - Descrever o JWKS
    - Descrever a validação do JWT com a chave pública

- Descrever como é feito o deploy 
    - ARM Template
    - Github Actions
    - Database scripts ou Migrations
    - Automatização
    - Estratégia de deploy e rollback (faremos por deployment slots usando app services ou containers usando ACI?)
    - ACR & ACI
    - etc...

- Descrever estilos e padrões de arquiteturas escolhidos (camadas com REST... etc)
- Descrever um Modelo Entidade Relacional do banco (pelo menos do Core talvez?) -->