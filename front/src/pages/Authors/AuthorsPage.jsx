import { useState } from "react";
import { useAuthors } from "../../hooks/useAuthors";
import Table from "../../components/Table/Table";

export default function AuthorsPage() {
  const [page, setPage] = useState(1);

  const { authors, loading, error } = useAuthors(page, 10);

  if (loading) return <p>Cargando autores...</p>;

  if (error) return <p>Error: {error}</p>;

  return (
    <div>
      <h1>Listado de Autores</h1>

      <button>Crear Autor</button>

      <Table data={authors} />

      <div style={{ marginTop: "10px" }}>
        <button onClick={() => setPage(page - 1)}>
          Anterior
        </button>

        <span style={{ margin: "0 10px" }}>
          Página {page}
        </span>

        <button onClick={() => setPage(page + 1)}>
          Siguiente
        </button>
      </div>
    </div>
  );
}