# SpMedicalGroup

## 1. Versão
Versão atual: 1.0.0

## 2. Propósito
#### Facilitar a busca de profissionais, realização de agendamentos, gerenciamento de consultas médicas e oferecer uma solução simplificada, transparente e efetiva.

## 2. Tecnologias utilizadas
![CSharp](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![Dotnet](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SQLServer](https://img.shields.io/badge/Microsoft_SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![AWS](https://img.shields.io/badge/Amazon_AWS-FF9900?style=for-the-badge&logo=amazonaws&logoColor=white)

## 3. Pré-requisitos
- .NET SDK 8.0 
- SQL Server
- Visual Studio 

## 4. Criação do banco de dados

#### Acesse o link abaixo e utilize comandos SQL no seu SGBD:

``` https://drive.google.com/drive/folders/14lHKQTPeHDbySkQ25PXw8ZFABi9lXSbo?usp=sharing ```

## 5. Crie um bucket na Amazon Web Services **

### Obs: Essa etapa é importante para realizar adição e alteração da foto de perfil dos usuários, mas não é essencial para o funcionamento básico da API.

#### a. Utilize as opções padrões de criação de bucket que a AWS sugere.
#### b. Após criar, adicione o seguinte script no campo de "Política de Bucket" em "Permissões":


``` JSON
{
    "Version": "2012-10-17",
    "Statement": [
        {
            "Sid": "PublicReadGetObject",
            "Effect": "Allow",
            "Principal": "*",
            "Action": "s3:GetObject",
            "Resource": "arn:aws:s3:::nome-do-seu-bucket/*" // Altere pelo nome do seu bucket
        }
    ]
}
```

#### c. Esse script serve para tornar os objetos do bucket acessíveis publicamente.

## 6. Preparando ambiente

#### a. Clone o repositório para sua máquina local.
#### b. Configure a string de conexão com o banco de dados criado anteriormente no arquivo `appsettings.json`.
#### c.** Na raiz da pasta do seu usuário, crie um diretório chamado '.aws' e insira um arquivo com nome 'credentials'. Exemplo: 

``` C://Usuários/SeuUsuárioAqui/.aws ```

#### Adicione o seguinte texto no arquivo *credentials* e insira suas chaves da AWS:

``` 
[default]
aws_access_key_id = SuaChaveDeAcesso
aws_secret_access_key = SuaSecretKey
```
#### d.** No arquivo S3Service (dentro da pasta /Services/aws) altere as seguintes linhas com as propriedades do bucket criado:

``` C#
private readonly string _bucketName = "seu-bucket"; 
private readonly string _region = "sua-regiao-aws"; 
```

## 7. Compile o projeto e inicie o programa



