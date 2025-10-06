# Embrace API – Sistema de Apoio em Situações de Emergência Climática 🌎

---

## 👤 Integrantes

**Nome do Grupo:** InnovexGroup  
**Integrantes:**
- Enzo Marsola (RM556310)
- Cauan Cruz (RM558238)
- Igor Barrocal (RM555217)

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

Siga o passo a passo detalhado em [`scripts/deploy_commands.txt`](scripts/deploy_commands.txt):

```sh
# Exemplo resumido (veja detalhes no script):
az group create --name embrace-rg --location brazilSouth
az sql server create --name embracesqlserver --resource-group embrace-rg --location brazilSouth --admin-user embraceadmin --admin-password "Embrace#2025"
az sql db create --resource-group embrace-rg --server embracesqlserver --name embrace-db --service-objective S0
az sql server firewall-rule create --resource-group embrace-rg --server embracesqlserver --name AllowAzureServices --start-ip-address 0.0.0.0 --end-ip-address 0.0.0.0
az sql server firewall-rule create --resource-group embrace-rg --server embracesqlserver --name AllowLocal --start-ip-address <SEU_IP> --end-ip-address <SEU_IP>
az appservice plan create --name embrace-plan --resource-group embrace-rg --location brazilSouth --sku B1
az webapp create --resource-group embrace-rg --plan embrace-plan --name embrace-app --runtime "dotnet:8"
az monitor app-insights component create --app embrace-insights --location brazilSouth --resource-group embrace-rg --application-type web
az webapp config appsettings set --resource-group embrace-rg --name embrace-app --settings "APPINSIGHTS_INSTRUMENTATIONKEY=$(az monitor app-insights component show --app embrace-insights --resource-group embrace-rg --query 'instrumentationKey' -o tsv)"
az webapp config connection-string set --resource-group embrace-rg --name embrace-app --connection-string-type SQLAzure --settings SqlServer="<String_Conexao_Completa>"
dotnet publish -c Release -o ./publish
Compress-Archive -Path ./publish/* -DestinationPath ./app.zip
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
