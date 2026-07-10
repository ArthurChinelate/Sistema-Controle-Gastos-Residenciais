// src/types/index.ts
export interface Person {
  id: number;
  name: string;
  age: number;
}

export interface Transaction {
  id: number;
  description: string;
  value: number;
  type: 1 | 2; // 1: Receita, 2: Despesa
  personId: number;
  person?: Person;
}

export interface PersonTotal {
  id: number;
  name: string;
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
}

export interface TotalsResponse {
  people: PersonTotal[];
  generalTotal: {
    totalReceitas: number;
    totalDespesas: number;
    saldo: number;
  };
}