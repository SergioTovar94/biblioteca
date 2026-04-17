import { useEffect, useState } from "react";
import api from "../services/api";

export function useAuthors(page = 1, pageSize = 10) {
    const [loans, setLoans] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    const fetchLoans = async () => {
        try {
            setLoading(true);

            const response = await api.get("/Loan/List", {
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