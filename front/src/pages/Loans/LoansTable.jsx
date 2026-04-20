export default function Table({ data }) {
  return (
    <table className="min-w-full divide-y divide-gray-200">
      <thead className="bg-gray-50">
        <tr>
          <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase">ID</th>
          <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase">Id Libro</th>
          <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase">Tïtulo</th>
          <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase">Fecha de préstamo</th>
          <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase">Fecha límite devolución</th>
          <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase">Prestado a</th>
          <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase">Devuelto</th>
          <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase">Acciones</th>
        </tr>
      </thead>

      <tbody className="divide-y divide-gray-200">
        {data.map((loan) => (
          <tr key={loan.id} className="hover:bg-gray-50">
            <td className="px-4 py-4 whitespace-nowrap text-sm">{loan.id}</td>
            <td className="px-4 py-4 whitespace-nowrap text-sm">{loan.bookId}</td>
            <td className="px-4 py-4 whitespace-nowrap text-sm">{loan.title}</td>
            <td className="px-4 py-4 whitespace-nowrap text-sm">{loan.loanDate}</td>
            <td className="px-4 py-4 whitespace-nowrap text-sm">{loan.dueDate}</td>
            <td className="px-4 py-4 whitespace-nowrap text-sm">{loan.borrowerName}</td>
            <td className="px-4 py-4 whitespace-nowrap text-sm">{loan.returned ? "Sí" : "No"}</td>
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