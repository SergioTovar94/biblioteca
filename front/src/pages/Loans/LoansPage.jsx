import { useState } from "react";
import { useLoans } from "../../hooks/useLoans";
import Table from "./LoansTable"; "../Loans/LoansTable";

export default function LoanssPage() {

  const { loans, loading, error } = useLoans();

  if (loading) return <p>Cargando préstamos...</p>;

  if (error) return <p>Error: {error}</p>;

  return (
    <div>
    <div className="flex justify-between items-center mb-6">
      <h1 className="text-2xl font-bold">Listado de Préstamos</h1>

      <button className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700">Crear Préstamo</button>
    </div>
      <Table data={loans} />

      <div style={{ marginTop: "10px" }}>

      </div>
    </div>
    
  );
}