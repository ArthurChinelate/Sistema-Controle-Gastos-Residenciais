// src/components/Dashboard.tsx
import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { TotalsResponse } from '../types';

const Dashboard: React.FC = () => {
  const [totals, setTotals] = useState<TotalsResponse | null>(null);

  // Função para buscar os dados na API
  const fetchTotals = async () => {
    try {
      const response = await axios.get<TotalsResponse>('https://localhost:5001/api/totals');
      setTotals(response.data);
    } catch (error) {
      console.error("Erro ao buscar totais", error);
    }
  };

  // Executa assim que o componente é montado
  useEffect(() => {
    fetchTotals();
  }, []);

  if (!totals) return <p>Carregando totais...</p>;

  return (
    <div>
      <h2>Consulta de Totais por Pessoa</h2>
      <table border={1} cellPadding={10} style={{ width: '100%', marginBottom: '20px' }}>
        <thead>
          <tr>
            <th>Nome</th>
            <th>Total Receitas</th>
            <th>Total Despesas</th>
            <th>Saldo</th>
          </tr>
        </thead>
        <tbody>
          {totals.people.map(person => (
            <tr key={person.id}>
              <td>{person.name}</td>
              <td style={{ color: 'green' }}>R$ {person.totalReceitas.toFixed(2)}</td>
              <td style={{ color: 'red' }}>R$ {person.totalDespesas.toFixed(2)}</td>
              <td style={{ fontWeight: 'bold' }}>R$ {person.saldo.toFixed(2)}</td>
            </tr>
          ))}
        </tbody>
      </table>

      <h3>Total Geral</h3>
      <div style={{ padding: '15px', backgroundColor: '#f0f0f0', borderRadius: '5px' }}>
        <p><strong>Receitas:</strong> R$ {totals.generalTotal.totalReceitas.toFixed(2)}</p>
        <p><strong>Despesas:</strong> R$ {totals.generalTotal.totalDespesas.toFixed(2)}</p>
        <p><strong>Saldo Líquido:</strong> R$ {totals.generalTotal.saldo.toFixed(2)}</p>
      </div>
    </div>
  );
};

export default Dashboard;