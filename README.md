# skills-in-action-asp-net-authentication
### Base de código da API REST de pessoas e livros em ASP.NET e .NET 10, com HATEOAS, paginação, arquivos, e-mail e Swagger, usada como ponto de partida para adicionar autenticação com JWT com o Claude Code.

![Forks](https://img.shields.io/github/forks/leandrocgsi/skills-in-action-asp-net-authentication)
![Stars](https://img.shields.io/github/stars/leandrocgsi/skills-in-action-asp-net-authentication)
[![Donate with PayPal](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.com/donate/?hosted_button_id=ZJ4NQJXEKQ63A)

[![Formação ASP.NET 2026: do Zero ao Deploy na Azure e GCP com .NET 10, Docker e Kubernetes](https://raw.githubusercontent.com/leandrocgsi/blog-images/refs/heads/main/formacoes_github/01-rest-asp-net.png)](https://pub.erudio.com.br/asp-net-2026-do-0-a-azure-e-gcp-com-asp-net-10-docker-e-kubernetes?utm_source=github&utm_medium=organic&utm_campaign=readme&utm_content=sia-aspnet-authentication)

Este repositório é derivado do código do curso [Formação ASP.NET 2026: do Zero ao Deploy na Azure e GCP com .NET 10, Docker e Kubernetes](https://pub.erudio.com.br/asp-net-2026-do-0-a-azure-e-gcp-com-asp-net-10-docker-e-kubernetes?utm_source=github&utm_medium=organic&utm_campaign=readme&utm_content=sia-aspnet-authentication), mas não é o código do curso. Ele foi atualizado para as versões mais recentes e estáveis do .NET e dos pacotes e serve de base para o conteúdo da série *skills in action*.

# Cursos Relacionados

[![Image](https://raw.githubusercontent.com/leandrocgsi/blog-images/refs/heads/main/formacoes_github/33-dotnet-ai.png "Inteligência Artificial com .NET AI e .NET 10: Aplicações Inteligentes com ChatGPT, OpenAI, DeepSeek e Ollama")](https://pub.erudio.com.br/inteligencia-artificial-com-dotnet-ai-e-dotnet-10-apps-inteligentes-com-chatgpt-openai-deepseek-e-ollama?utm_source=github&utm_medium=organic&utm_campaign=readme&utm_content=sia-aspnet-authentication)

[![Image](https://raw.githubusercontent.com/leandrocgsi/blog-images/refs/heads/main/formacoes_github/15-microservices-asp-net.png "Formação Microsserviços 2026: do 0 ao Deploy na Google Cloud com ASP.NET, .NET 10, Kubernetes e Docker")](https://pub.erudio.com.br/meus-cursos?utm_source=github&utm_medium=organic&utm_campaign=readme&utm_content=sia-aspnet-authentication)

[![Image](https://raw.githubusercontent.com/leandrocgsi/blog-images/refs/heads/main/formacoes_github/10-docker-to-aws.png "Formação Docker e Kubernetes 2026: do Zero ao Deploy Profissional na AWS, Azure e GCP com GitHub Actions")](https://pub.erudio.com.br/docker-e-kubernetes-2026-do-zero-ao-deploy-profissional-na-aws-azure-e-gcp-com-github-actions?utm_source=github&utm_medium=organic&utm_campaign=readme&utm_content=sia-aspnet-authentication)

[![Image](https://raw.githubusercontent.com/leandrocgsi/blog-images/refs/heads/main/formacoes_github/09-docker.png "Formação Docker e Kubernetes 2026: do Zero à Inteligência Artificial com MCP Catalog e Docker Model Runner")](https://pub.erudio.com.br/meus-cursos?utm_source=github&utm_medium=organic&utm_campaign=readme&utm_content=sia-aspnet-authentication)

[![Image](https://raw.githubusercontent.com/leandrocgsi/blog-images/refs/heads/main/formacoes_github/13-career.png "Carreira em TI do Zero ao Exterior: Currículo, Entrevistas, Negociação e Crescimento Profissional")](https://pub.erudio.com.br/carreira-em-ti-do-zero-ao-exterior-curriculo-entrevistas-negociacao-e-crescimento-profissional?utm_source=github&utm_medium=organic&utm_campaign=readme&utm_content=sia-aspnet-authentication)

# Sobre este repositório

Este repositório é o ponto de partida de um vídeo da série *skills in action*, em que o **Claude Code** e **skills** são usados para adicionar **autenticação com JWT** a uma API REST já existente. O que no curso leva horas, aqui é feito em minutos. A base é uma API em **ASP.NET e .NET 10** completa, mas com todos os endpoints abertos.

## O que tem aqui hoje (ponto de partida)

* CRUD de pessoas e livros, com versionamento por rota (`v1` e `v2`)
* HATEOAS e paginação (`/api/person/v1/{sortDirection}/{pageSize}/{page}`)
* Negociação de conteúdo em JSON e XML
* Importação e exportação de pessoas em CSV e XLSX
* Upload e download de arquivos
* Envio de e-mail, com e sem anexo
* CORS configurável
* Documentação com Swagger UI e Scalar
* EF Core, SQL Server e migrations com Evolve
* Testes unitários e de integração (Testcontainers) com xUnit v3

## Resultado final dos vídeos

Ao final da gravação o código deve ficar equivalente ao módulo `27_AuthenticationWithJWT` do curso, já nas versões mais recentes das tecnologias:

* Autenticação JWT Bearer, configurada em `AuthConfig`, com a seção `TokenConfigurations` no `appsettings.json`
* Entidade `User`, `UserRepository` e tabela `Users` criada por uma nova migration do Evolve, com um usuário de exemplo
* Geração de tokens (`TokenGenerator`) e hash de senha (`Sha256PasswordHasher`)
* Serviços de login e de usuário
* `AuthController` em `api/auth` com `signin`, `refresh`, `revoke` e `create`
* Endpoints de pessoas, livros, e-mail e arquivos protegidos com `[Authorize("Bearer")]`

## Como usar o código

### Pré-requisitos

* .NET SDK 10 (versão GA, 10.0.100 ou mais nova)
* SQL Server rodando em `localhost,1433` com usuário `sa` e senha `@Admin123` (ajuste em `RestWithASPNET10Erudio/appsettings.json` se for diferente)
* Docker em execução, necessário para os testes de integração com Testcontainers

### Estrutura: before e after

* `01-authentication-before`: o ponto de partida, deixado intacto para você comparar
* `02-authentication-after`: começa idêntica à pasta before e é nela que a autenticação é adicionada durante o vídeo

Cada pasta é uma solução .NET completa. Entre em uma delas (`cd 02-authentication-after`) antes de executar os comandos abaixo.

### Passo a passo

1. Crie o banco de dados: `CREATE DATABASE skills_in_action_asp_net_authentication;`
2. Execute a aplicação: `dotnet run --project RestWithASPNET10Erudio`. As migrations do Evolve criam e populam as tabelas na primeira execução (elas só rodam em `Development`, que é o ambiente dos perfis `http` e `https`)
3. Abra o Swagger UI em `http://localhost:5034/swagger-ui/index.html` ou o Scalar em `http://localhost:5034/scalar`
4. Rode os testes com `dotnet test`. Os testes de integração sobem um SQL Server com Testcontainers e **também exigem o SQL Server em `localhost,1433`** do passo 1, porque a aplicação roda em `Development` dentro do teste
5. Para rodar só os testes unitários: `dotnet test --filter-namespace "*UnitTests*"`
6. Abra a pasta `02-authentication-after` no Claude Code e siga o vídeo para adicionar a autenticação

# Outros Cursos Erudio

[![Image](https://raw.githubusercontent.com/leandrocgsi/blog-images/refs/heads/main/formacoes_github/01-hm-claude-code-java.png?raw=true "Formação Claude Code para DEVs Java e Spring Boot: Produtividade 10x, Agentes de IA, MCPs e Aplicações do Mundo Real")](https://pub.erudio.com.br/formacao-dev10x-claude-code-para-devs-java-e-spring-boot?utm_source=github&utm_medium=organic&utm_campaign=readme&utm_content=sia-aspnet-authentication)

[![Image](https://raw.githubusercontent.com/leandrocgsi/blog-images/refs/heads/main/formacoes_github/07-rest-java.png "Formação Spring Boot 2026: do Zero ao Deploy na AWS e GCP com Java, Docker e Kubernetes")](https://pub.erudio.com.br/formacao-spring-boot-2026-do-zero-ao-continuous-deployment-na-aws-e-gcp-com-java-docker-e-kubernetes?utm_source=github&utm_medium=organic&utm_campaign=readme&utm_content=sia-aspnet-authentication)

[![Image](https://raw.githubusercontent.com/leandrocgsi/blog-images/refs/heads/main/formacoes_github/31-spring-ai-java.png "Formação Spring AI 2026: Inteligência Artificial com Java, Spring Boot, ChatGPT, DeepSeek, Claude e MCP")](https://pub.erudio.com.br/formacao-spring-ai-2026-inteligencia-artificial-com-java-spring-boot-chatgpt-deepseek-claude-e-mcp?utm_source=github&utm_medium=organic&utm_campaign=readme&utm_content=sia-aspnet-authentication)

[![Image](https://raw.githubusercontent.com/leandrocgsi/blog-images/refs/heads/main/formacoes_github/14-microservices-java.png "Formação Microsserviços 2026: do Zero ao Deploy na Google Cloud com Spring Boot, Kubernetes e Docker")](https://pub.erudio.com.br/formacao-microsservicos-2026-do-zero-ao-deploy-na-google-cloud-com-spring-boot-kubernetes-e-docker?utm_source=github&utm_medium=organic&utm_campaign=readme&utm_content=sia-aspnet-authentication)

[![Image](https://raw.githubusercontent.com/leandrocgsi/blog-images/refs/heads/main/formacoes_github/24-tests-java.png "Formação Java Testing 2026: Testes Automatizados com JUnit, Mockito, Spring Boot, TDD e TestContainers")](https://pub.erudio.com.br/formacao-java-testing-2026-testes-automatizados-com-junit-mockito-spring-boot-tdd-e-testcontainers?utm_source=github&utm_medium=organic&utm_campaign=readme&utm_content=sia-aspnet-authentication)

[![Image](https://raw.githubusercontent.com/leandrocgsi/blog-images/refs/heads/main/formacoes_github/29-cdpl-java-gcp.png "Formação Continuous Deployment: do Zero ao Deploy com Java, Spring Boot, Docker, Kubernetes, Google Cloud (GCP) e GitHub Actions")](https://pub.erudio.com.br/formacao-continuous-deployment-java-kubernetes-gcp-github-actions?utm_source=github&utm_medium=organic&utm_campaign=readme&utm_content=sia-aspnet-authentication)

[![Image](https://raw.githubusercontent.com/leandrocgsi/blog-images/refs/heads/main/formacoes_github/27-cicd-java-aws.png "Formação Continuous Integration e Delivery: do Zero ao Deploy com Java, Spring Boot, Docker, Amazon AWS e GitHub Actions")](https://pub.erudio.com.br/formacao-continuous-integration-delivery-java-aws-github-actions?utm_source=github&utm_medium=organic&utm_campaign=readme&utm_content=sia-aspnet-authentication)

[![Image](https://raw.githubusercontent.com/leandrocgsi/blog-images/refs/heads/main/formacoes_github/28-cicd-java-azure.png "Formação Continuous Integration e Delivery: do Zero ao Deploy com Java, Spring Boot, Docker, Microsoft Azure e GitHub Actions")](https://pub.erudio.com.br/formacao-continuous-integration-delivery-java-azure-github-actions?utm_source=github&utm_medium=organic&utm_campaign=readme&utm_content=sia-aspnet-authentication)

[![Image](https://raw.githubusercontent.com/leandrocgsi/blog-images/refs/heads/main/formacoes_github/32-code-ai-kspring.png "Formação Spring AI 2026: Inteligência Artificial com Kotlin, Spring Boot, ChatGPT, DeepSeek, Claude e MCP")](https://pub.erudio.com.br/formacao-spring-ai-2026-inteligencia-artificial-com-kotlin-spring-boot-chatgpt-deepseek-claude-e-mcp?utm_source=github&utm_medium=organic&utm_campaign=readme&utm_content=sia-aspnet-authentication)

[![Image](https://raw.githubusercontent.com/leandrocgsi/blog-images/refs/heads/main/formacoes_github/22-ms-kotlin.png "Formação Microsserviços com Spring Cloud, Spring Boot, Kotlin, Docker e Zookeeper")](https://pub.erudio.com.br/formacao-microsservicos-com-spring-cloud-spring-boot-kotlin-docker-e-zookeeper?utm_source=github&utm_medium=organic&utm_campaign=readme&utm_content=sia-aspnet-authentication)

[![Image](https://raw.githubusercontent.com/leandrocgsi/blog-images/refs/heads/main/formacoes_github/18-rest-spring-kotlin.png "Formação Spring Boot com Kotlin: REST APIs Profissionais do Zero ao Deploy na AWS com Docker e Kubernetes")](https://pub.erudio.com.br/formacao-spring-boot-com-kotlin-rest-apis-profissionais-do-zero-ao-deploy-na-aws-com-docker-e-kubernetes?utm_source=github&utm_medium=organic&utm_campaign=readme&utm_content=sia-aspnet-authentication)

[![Image](https://raw.githubusercontent.com/leandrocgsi/blog-images/refs/heads/main/formacoes_github/20-kotlin.png "Kotlin para DEV's Java: Domine Lambdas, Generics, Reflections, Programação Funcional e Spring Boot")](https://pub.erudio.com.br/kotlin-para-desenvolvedores-java-domine-sintaxe-lambdas-generics-reflections-programacao-funcional-e-spring-boot?utm_source=github&utm_medium=organic&utm_campaign=readme&utm_content=sia-aspnet-authentication)

[![Image](https://raw.githubusercontent.com/leandrocgsi/blog-images/refs/heads/main/formacoes_github/30-jasper.png "Crie Relatórios Profissionais com JasperReports, Java, Spring Boot e JasperSoft Studio")](https://pub.erudio.com.br/crie-relatorios-profissionais-com-jasperreports-java-spring-boot-e-jaspersoft-studio?utm_source=github&utm_medium=organic&utm_campaign=readme&utm_content=sia-aspnet-authentication)

# Vídeos Gratuitos: Configuração do Ambiente de DEV

## [Como Configurar Ambiente de DEV ASP.NET no Windows: .NET 10, Visual Studio e SQL Server Guia Completo](https://www.youtube.com/watch?v=dqwARAaweu8)

[![Como Configurar Ambiente de DEV ASP.NET no Windows: .NET 10, Visual Studio e SQL Server Guia Completo](https://img.youtube.com/vi/dqwARAaweu8/maxresdefault.jpg)](https://www.youtube.com/watch?v=dqwARAaweu8)

## [Como Configurar Ambiente de DEV ASP.NET no Linux: .NET 10, Visual Studio e SQL Server Guia Completo](https://www.youtube.com/watch?v=QQlraJMG5pU)

[![Como Configurar Ambiente de DEV ASP.NET no Linux: .NET 10, Visual Studio e SQL Server Guia Completo](https://img.youtube.com/vi/QQlraJMG5pU/maxresdefault.jpg)](https://www.youtube.com/watch?v=QQlraJMG5pU)

# 📚 Para saber mais

### Fundamentos Teóricos
* [Web Services](https://www.erudio.com.br/blog/web-services/)
* [RESTful Web Services](https://www.erudio.com.br/blog/restful-web-services/)
* [HTTP Status Codes em Serviços REST](https://www.erudio.com.br/blog/http-status-codes-em-servicos-rest/)
* [Entendendo HATEOAS](https://www.erudio.com.br/blog/en/)

### Fundamentos Práticos
* [Criando uma simples Web API RESTful em .NET Core 2.0](https://www.erudio.com.br/blog/criando-uma-simples-web-api-restful-em-net-core-2-0/)
* [Criando um endpoint REST com ASP.NET Core](https://www.erudio.com.br/blog/criando-um-endpoint-rest-com-asp-net-core/)

### Conheça Mais Detalhes
* [Evolve](https://evolve-db.netlify.com/)

# 🌐 HTTP Status Codes adotados pelas principais APIs

Baseado [neste repositório](https://gist.github.com/vkostyukov/32c84c0c01789425c29a#file-statuses-md)

| API | Status Codes |
|-----|---------------|
| Twitter | 200, 304, 400, 401, 403, 404, 406, 410, 420, 422, 429, 500, 502, 503, 504 |
| Stripe | 200, 400, 401, 402, 404, 429, 500, 502, 503, 504 |
| Github | 200, 400, 422, 301, 302, 304, 307, 401, 403 |
| Pagerduty | 200, 201, 204, 400, 401, 403, 404, 408, 500 |
| NewRelic Plugins | 200, 400, 403, 404, 405, 413, 500, 502, 503 |
| Etsy | 200, 201, 400, 403, 404, 500, 503 |
| Dropbox | 200, 400, 401, 403, 404, 405, 429, 503, 507 |
| Spotify | 200, 201, 204, 304, 400, 401, 403, 404, 429, 500, 502, 503 |
| Google Cloud | 200, 301, 304, 307, 308, 400, 401, 403, 404, 405, 409, 411, 412, 416, 429, 500, 501, 503 |
| Telegram | 200, 303, 400, 401, 403, 404, 420, 500 |
| Uber | 200, 201, 400, 401, 403, 404, 406, 409, 422, 429, 500 |
