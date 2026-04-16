import { useEffect, useState } from "react";
import api from "../services/api";

export function useBooks() {
    const [books, setBooks] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);


    const fetchBooks = async () => {
        try {
            setLoading(true);
            setError(null);

            const response = await api.get("/Books/List");

            setBooks(response.data);
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchBooks();
    }, []);

    return {
        books,
        loading,
        error,
        refetch: fetchBooks,
    };
}