export default function Table({ data }) {
  return (
    <table className="min-w-full divide-y divide-gray-200">
      <thead className="bg-gray-50">
        <tr>
          <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase">ID</th>
          <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase">Nombre</th>
          <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase">Apellido</th>
          <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase">Acciones</th>
        </tr>
      </thead>

      <tbody className="divide-y divide-gray-200">
        {data.map((author) => (
          <tr key={author.id} className="hover:bg-gray-50">
            <td className="px-4 py-4 whitespace-nowrap text-sm">{author.id}</td>
            <td className="px-4 py-4 whitespace-nowrap text-sm">{author.name}</td>
            <td className="px-4 py-4 whitespace-nowrap text-sm">{author.lastName}</td>

            <td>
              <button className="text-blue-600 hover:text-blue-800">Editar</button>
              <button className="text-red-600 hover:text-blue-800">Eliminar</button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}