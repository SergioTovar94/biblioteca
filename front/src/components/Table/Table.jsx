export default function Table({ data }) {
  return (
    <table border="1" cellPadding="8">
      <thead>
        <tr>
          <th>ID</th>
          <th>Nombre</th>
          <th>Apellido</th>
          <th>Acciones</th>
        </tr>
      </thead>

      <tbody>
        {data.map((author) => (
          <tr key={author.id}>
            <td>{author.id}</td>
            <td>{author.name}</td>
            <td>{author.lastName}</td>

            <td>
              <button>Editar</button>
              <button>Eliminar</button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}