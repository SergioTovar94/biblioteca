export default function Table({ data }) {
  return (
    <table className="min-w-full divide-y divide-gray-200">
      <thead className="bg-gray-50">
        <tr>
          <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase">ID</th>
          <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase">Título</th>
          <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase">Páginas</th>
          <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase">Fecha publicación</th>
          <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase">Género</th>
          <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase">Autor</th>
          <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase">Imagen</th>
          <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase">Acciones</th>
        </tr>
      </thead>

      <tbody className="divide-y divide-gray-200">
        {data.map((book) => (
          <tr key={book.id} className="hover:bg-gray-50">
            <td className="px-4 py-4 whitespace-nowrap text-sm">{book.id}</td>
            <td className="px-4 py-4 whitespace-nowrap text-sm">{book.title}</td>
            <td className="px-4 py-4 whitespace-nowrap text-sm">{book.numberOfPages}</td>
            <td className="px-4 py-4 whitespace-nowrap text-sm">{book.publishedDate}</td>
            <td className="px-4 py-4 whitespace-nowrap text-sm">{book.genre}</td>
            <td className="px-4 py-4 whitespace-nowrap text-sm">{book.authorName}</td>
            <td className="px-4 py-4 whitespace-nowrap text-sm">
              {book.coverImageUrl ? (
                <img
                  src={`${import.meta.env.VITE_API_URL}${book.coverImageUrl}`}
                  alt={`Portada de ${book.title}`}
                  className="w-12 h-16 object-cover rounded shadow"
                />
              ) : (
                <span className="text-gray-400">Sin imagen</span>
              )}
            </td>
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