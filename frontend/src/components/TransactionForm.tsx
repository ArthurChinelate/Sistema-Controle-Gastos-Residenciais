// src/components/TransactionForm.tsx
import React, { useState } from 'react';
import axios from 'axios';
import { Person } from '../types';

interface Props {
  people: Person[];
  onTransactionAdded: () => void;
}

const TransactionForm: React.FC<Props> = ({ people, onTransactionAdded }) => {
  const [description, setDescription] = useState('');
  const [value, setValue] = useState<number | ''>('');
  const [type, setType] = useState<number>(2); // Padrão: 2 (Despesa)
  const [personId, setPersonId] = useState<number | ''>('');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await axios.post('https://localhost:5001/api/transactions', {
        description,
        value: Number(value),
        type,
        personId: Number(personId)
      });
      alert('Transação cadastrada com sucesso!');
      onTransactionAdded(); // Atualiza a tela após salvar
    } catch (error: any) {
      // Exibe a mensagem de regra de negócio do backend (ex: Menor de idade)
      if (error.response && error.response.data) {
        alert(`Erro: ${error.response.data}`);
      }
    }
  };

  return (
    <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: '10px', maxWidth: '300px' }}>
      <h3>Nova Transação</h3>
      
      <select value={personId} onChange={(e) => setPersonId(Number(e.target.value))} required>
        <option value="">Selecione a Pessoa</option>
        {people.map(p => (
          <option key={p.id} value={p.id}>{p.name} ({p.age} anos)</option>
        ))}
      </select>

      <input 
        type="text" placeholder="Descrição" required
        value={description} onChange={(e) => setDescription(e.target.value)} 
      />
      
      <input 
        type="number" step="0.01" placeholder="Valor" required
        value={value} onChange={(e) => setValue(Number(e.target.value))} 
      />

      <select value={type} onChange={(e) => setType(Number(e.target.value))} required>
        <option value={1}>Receita</option>
        <option value={2}>Despesa</option>
      </select>

      <button type="submit">Salvar Transação</button>
    </form>
  );
};

export default TransactionForm;