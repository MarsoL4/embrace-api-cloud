# Embrace API – Sistema de Apoio em Situações de Emergência Climática 🌎

👨‍💻 **Integrantes**

Time Embrace – GS 2025-1:

- **Enzo Giuseppe Marsola** – RM: 556310, Turma: 2TDSPK  
- **Rafael de Souza Pinto** – RM: 555130, Turma: 2TDSPY  
- **Luiz Paulo F. Fernandes** – RM: 555497, Turma: 2TDSPF

---

## 💡 Descrição da Solução

A Embrace API é uma solução desenvolvida em .NET 8 para gerenciamento de ações solidárias em situações de emergência climática, facilitando a conexão de ONGs, voluntários e comunidades afetadas. A API oferece endpoints completos para cadastro, consulta, atualização e remoção (CRUD) das principais entidades do sistema, integrando-se a um banco de dados SQL Server PaaS e monitoramento via Application Insights na Azure.

---

## 🏆 Benefícios

- Centralização da gestão de ações solidárias, doações e voluntários.
- Persistência de dados em SQL Server na nuvem, com estrutura relacional e integridade.
- Monitoramento automatizado via Application Insights.
- Deploy automatizado via Azure CLI.
- Documentação e exemplos completos via Swagger UI.

---

## 🗄️ Banco de Dados em Nuvem

- **Tecnologia:** Azure SQL Database (PaaS)
- **Relacionamento:** Master-Detail (ONG → Ação Solidária → Doação; Voluntário, Ponto de Alimento)
- **Script DDL:** [`scripts/ddl.sql`](scripts/ddl.sql)

---

## 🛠️ Conteúdo do Repositório

- [Código-fonte da API (.NET)](Embrace.API)
- [Scripts de banco (DDL)](scripts/ddl.sql)
- [Scripts de deploy na Azure CLI](scripts/deploy_commands.txt)
- [Arquivo de configuração (`appsettings.json`)](Embrace.API/appsettings.json)
- [Exemplos de payload para Swagger e testes (`Examples`)](Embrace.API/Examples/)

---

## ⚙️ How-To: Deploy Automatizado na Azure (CLI)

### **Requisitos**

- Conta Azure, Azure CLI instalado e autenticado (`az login`)
- .NET SDK 8.0+

### **1. Clone o repositório**

```bash
git clone https://github.com/MarsoL4/embrace-api-cloud.git
cd embrace-api-cloud
```

### **2. Execute os comandos do CLI para provisionar recursos**

### **Requisitos**

- Conta Azure, Azure CLI instalado e autenticado (`az login`)
- .NET SDK 8.0+

### 1. Clone o repositório

```bash
git clone https://github.com/MarsoL4/embrace-api-cloud.git
cd embrace-api-cloud
```

### 2. Crie os recursos na Azure via CLI (passo a passo)

1. **Crie o grupo de recursos Azure**  
   Este comando cria um agrupador para todos os recursos do projeto.
   ```bash
   az group create --name embrace-rg --location brazilSouth
   ```

2. **Crie o servidor SQL**  
   Cria o servidor do banco de dados SQL na Azure, onde o banco será hospedado.
   ```bash
   az sql server create --name embracesqlserver --resource-group embrace-rg --location brazilSouth --admin-user embraceadmin --admin-password "Embrace#2025"
   ```

3. **Crie o banco de dados SQL**  
   Cria o banco de dados dentro do servidor SQL criado no passo anterior.
   ```bash
   az sql db create --resource-group embrace-rg --server embracesqlserver --name embrace-db --service-objective S0
   ```

4. **Obtenha a string de conexão do banco**  
   Exibe a string de conexão necessária para configurar a aplicação.
   ```bash
   az sql db show-connection-string --server embracesqlserver --name embrace-db --client ado.net
   ```
   > **Atenção:** Na string recebida, será necessário adicionar o usuário (`User ID`) e senha (`Password`) do banco de dados nos espaços indicados.

5. **Libere acesso do App Service ao SQL**  
   Permite que serviços da Azure conectem-se ao banco de dados.
   ```bash
   az sql server firewall-rule create --resource-group embrace-rg --server embracesqlserver --name AllowAzureServices --start-ip-address 0.0.0.0 --end-ip-address 0.0.0.0
   ```

6. **Libere acesso do seu IP ao SQL**  
   Permite que você acesse o banco de dados diretamente pelo seu IP.
   ```bash
   az sql server firewall-rule create --resource-group embrace-rg --server embracesqlserver --name AllowLocal --start-ip-address <SEU_IP> --end-ip-address <SEU_IP>
   ```
   > Substitua `<SEU_IP>` pelo seu IP real.

7. **Crie o plano do App Service**  
   Cria o plano de hospedagem para o serviço de aplicação.
   ```bash
   az appservice plan create --name embrace-plan --resource-group embrace-rg --location brazilSouth --sku B1
   ```

8. **Crie o App Service (.NET 8)**  
   Cria o serviço de aplicação onde a API será publicada.
   ```bash
   az webapp create --resource-group embrace-rg --plan embrace-plan --name embrace-app --runtime "dotnet:8"
   ```

9. **Crie o recurso do Application Insights**  
   Cria o recurso para monitoramento da aplicação.
   ```bash
   az monitor app-insights component create --app embrace-insights --location brazilSouth --resource-group embrace-rg --application-type web
   ```

10. **Vincule o Application Insights ao App Service**  
    ```bash
    az webapp config appsettings set --resource-group embrace-rg --name embrace-app --settings "APPINSIGHTS_INSTRUMENTATIONKEY=$(az monitor app-insights component show --app embrace-insights --resource-group embrace-rg --query 'instrumentationKey' -o tsv)"
    ```

11. **Configure a string de conexão no App Service**  
    Adiciona a string de conexão do banco (com usuário e senha) nas configurações do App Service.
    ```bash
    az webapp config connection-string set --resource-group embrace-rg --name embrace-app --connection-string-type SQLAzure --settings SqlServer="<String_Conexao_Completa>"
    ```

12. **Compile e publique o projeto**  
    Compila o projeto para pasta de publicação.
    ```bash
    dotnet publish -c Release -o ./publish
    ```

13. **Compacte os arquivos publicados**  
    Gera um arquivo ZIP para envio ao App Service.
    ```bash
    Compress-Archive -Path ./publish/* -DestinationPath ./app.zip
    ```

14. **Faça o deploy do ZIP para o App Service**  
    Publica a API na Azure.
    ```bash
    az webapp deployment source config-zip --resource-group embrace-rg --name embrace-app --src ./app.zip
    ```

> **Atenção:**  
> - Substitua `<SEU_IP>` pelo seu IP real.  
> - Insira a string de conexão completa (com usuário e senha) no lugar de `<String_Conexao_Completa>`.


### **3. Acesse o Swagger UI**

Após o deploy, acesse o Swagger via:
```
https://embrace-app.azurewebsites.net/swagger/index.html
```

---

## 🧑‍💻 Testando a API via Swagger

- Utilize o botão "Try it out" para testar todos os endpoints (ONG, Ação Solidária, Doação, Voluntário, Ponto de Alimento).
- Os exemplos de requisição já estarão preenchidos para facilitar os testes.
- É possível realizar operações CRUD completas, validando persistência no banco SQL Server Azure.

---

## 📄 Script DDL das Tabelas

- O arquivo [`scripts/ddl.sql`](scripts/ddl.sql) contém toda a estrutura das tabelas, colunas, PKs, FKs e índices do banco.
- Use-o para criar ou verificar a estrutura do banco conforme exigido no projeto.

---

## 🎥 Vídeo Demonstrativo

- O vídeo da entrega mostra:
  - Criação dos recursos Azure via CLI
  - Deploy da aplicação
  - Execução de testes CRUD via Swagger
  - Verificação da persistência dos dados no banco SQL Server Azure
- **Link do vídeo:** [https://youtu.be/5euz19OZEVE]

---

## 🏗️ Arquitetura da Solução

- **Recursos:** App Service (.NET 8), Azure SQL Database, Application Insights
- **Fluxo:** Usuário → Embrace API (.NET 8) → SQL Server na nuvem → Application Insights para monitoramento
