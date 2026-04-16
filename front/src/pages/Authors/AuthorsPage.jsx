import { useState } from "react";
import { useAuthors } from "../../hooks/useAuthors";
import Table from "../../components/Table/Table";

export default function AuthorsPage() {
  const [page, setPage] = useState(1);

  const { authors, totalPages, loading, error } = useAuthors(page, 10);

  if (loading) return <p>Cargando autores...</p>;

  if (error) return <p>Error: {error}</p>;

  return (
    <div>
    <div className="flex justify-between items-center mb-6">
      <h1 className="text-2xl font-bold">Listado de Autores</h1>

      <button className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700">Crear Autor</button>
    </div>
      <Table data={authors} />

      <div style={{ marginTop: "10px" }}>
        <button
          onClick={() => setPage((prev) => Math.max(prev - 1, 1))}
          disabled={page === 1}
          className="
            px-4 py-2
            bg-gray-200
            text-gray-700
            rounded-lg
            font-medium
            transition
            hover:bg-gray-300
            disabled:opacity-50
            disabled:cursor-not-allowed
            "
        >
          Anterior
        </button>

        <span style={{ margin: "0 10px" }}>
          Página {page}
        </span>

        <button
          onClick={() => setPage((prev) => Math.min(prev + 1, totalPages))}
          disabled={page === totalPages}
          className="
            px-4 py-2
            bg-gray-200
            text-gray-700
            rounded-lg
            font-medium
            transition
            hover:bg-gray-300
            disabled:opacity-50
            disabled:cursor-not-allowed
            "
        >
          Siguiente
        </button>
      </div>
    </div>
    
  );
}