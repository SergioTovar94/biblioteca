import { useEffect, useState } from "react";
import api from "../services/api";

export function useAuthors(page = 1, pageSize = 10) {
    const [authors, setAuthors] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const [totalPages, setTotalPages] = useState(1);


    const fetchAuthors = async () => {
        try {
            setLoading(true);

            const response = await api.get("/Author/List", {
                params: {
                    page,
                    pageSize,
                },
            });

            setAuthors(response.data.items);
            setTotalPages(response.data.totalPages);
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchAuthors();
    }, [page, pageSize]);

    return {
        authors,
        totalPages,
        loading,
        error,
        refetch: fetchAuthors,
    };
}