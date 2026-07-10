// Models/Enums.cs
// Define os tipos de transação permitidos no sistema.
public enum TransactionType
{
    Receita = 1,
    Despesa = 2
}

// Models/Person.cs
public class Person
{
    // Identificador único gerado automaticamente pelo EF Core.
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    
    // Navegação: Uma pessoa tem várias transações.
    // O Entity Framework usará isso para configurar o Delete em Cascata automaticamente.
    public List<Transaction> Transactions { get; set; } = new List<Transaction>();
}

// Models/Transaction.cs
public class Transaction
{
    // Identificador único gerado automaticamente.
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public TransactionType Type { get; set; }
    
    // Chave estrangeira obrigatória. 
    // Se a pessoa for deletada, as transações também serão (Cascade Delete).
    public int PersonId { get; set; }
    public Person? Person { get; set; }
}