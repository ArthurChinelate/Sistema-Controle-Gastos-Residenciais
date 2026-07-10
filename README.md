### Sistema de Controle de Gastos Residenciais
## Objetivo
# Este projeto implementa um sistema de controle de gastos residenciais. O sistema conta com cadastros de transações, cadastro de pessoas e consulta de totais.

Tecnologias Utilizadas
A construção do sistema emprega o Back-end em .NET com C# e o Front-end em React com TypeScript. Para o banco de dados e persistência, é utilizado o SQLite integrando o Entity Framework Core. Os dados devem persistir após fechar a aplicação.

## Funcionalidades

O sistema possui o Cadastro de Pessoas, que permite a criação, listagem e deleção. O cadastro contém Identificador (único e gerado automaticamente), Nome e Idade. O Cadastro de Transações oferece funcionalidades básicas de criação e listagem. Este cadastro contém Identificador (gerado automaticamente), Descrição, Valor, Tipo (despesa ou receita) e a Pessoa associada, cujo identificador precisa existir no banco. A Consulta de Totais lista todas as pessoas cadastradas, exibindo o total de receitas, despesas e o saldo (receita – despesa) de cada uma. Ao final da listagem, é exibido o total geral de todas as pessoas, incluindo o total de receitas, despesas e o saldo líquido.

Regras de Negócio e Lógica Implementada
A lógica desenvolvida foi desenhada para aderir rigorosamente às regras de negócio, e está documentada através de comentários no próprio código.

Sobre a Restrição para Menores de Idade, caso a pessoa informada seja menor de 18 anos, apenas despesas podem ser cadastradas. Essa regra foi alocada no Back-end por segurança, não permitindo a injeção via chamadas de API avulsas.

A Exclusão em Cascata (Cascade Delete) define que em casos que se delete uma pessoa, todas as transações dessa pessoa são apagadas. Isso foi configurado via Entity Framework (OnDelete(DeleteBehavior.Cascade)), assegurando que o banco exclua as transações automaticamente para manter a integridade dos dados, economizando código no Controller.

O sistema realiza Consultas Otimizadas, onde o controlador de Totais calcula tudo através de projeções SQL (.Select()) direto no banco de dados. Isso impede o carregamento desnecessário de todos os registros na memória RAM do servidor para realizar a soma.

A Persistência Local é garantida utilizando o .SaveChangesAsync(), de forma que tudo persiste localmente (em disco no SQLite) entre as execuções da aplicação.

## Como Executar o Projeto

Para rodar a aplicação localmente e validar que os dados persistem após fechar, utilize os comandos exatos. Primeiro, acesse a pasta do back-end no seu terminal e inicie a API executando o comando dotnet run. Em seguida, abra um novo terminal, acesse a pasta do front-end e inicie a interface de usuário utilizando o comando npm run dev.
