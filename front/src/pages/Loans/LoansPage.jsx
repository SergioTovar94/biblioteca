import { useState } from "react";
import { useBooks } from "../../hooks/useBooks";
import Table from "../Books/BooksTable";

export default function BooksPage() {

  const { books, loading, error } = useBooks();

  if (loading) return <p>Cargando libros...</p>;

  if (error) return <p>Error: {error}</p>;

  return (
    <div>
    <div className="flex justify-between items-center mb-6">
      <h1 className="text-2xl font-bold">Listado de Libros</h1>

      <button className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700">Crear Libro</button>
    </div>
      <Table data={books} />

      <div style={{ marginTop: "10px" }}>

      </div>
    </div>
    
  );
}